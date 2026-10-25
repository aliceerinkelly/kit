using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Kit.Controls;

/// <summary>What a pasted snippet produced: a control, some text, or errors.</summary>
public sealed record SnippetResult(UIElement? Preview, string? Text, IReadOnlyList<string> Messages)
{
    public bool Ok => Messages.Count == 0;
}

/// <summary>
/// Compiles pasted C# and hands back the control it builds. KIT is a design tool, so
/// this is deliberately shallow: one class, one Build() entry point, no files on disk,
/// no assembly saved anywhere. The code runs in this process, exactly like a code-behind
/// file would, which is why it can use PillButton and WPF directly.
/// </summary>
public static class SnippetRunner
{
    private static List<MetadataReference>? _references;

    /// <summary>The smallest thing that works, shown in the Sample flyout.</summary>
    public const string OnePill = """
        return new PillButton
        {
            Content = "Save",
            Size = PillSize.Large
        };
        """;

    /// <summary>Pasted into the box when KIT starts, so the contract is visible.</summary>
    public const string Sample = """
        var row = new StackPanel { Orientation = Orientation.Horizontal };

        row.Children.Add(new PillButton
        {
            Content = "Save",
            Size = PillSize.Medium,
            Margin = new Thickness(0, 0, 8, 0)
        });

        row.Children.Add(new PillButton { Content = "Cancel", Size = PillSize.Small });

        return row;
        """;

    private const string Header = """
        // KIT runs this as the body of Snippet.Build().
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Windows;
        using System.Windows.Controls;
        using System.Windows.Input;
        using System.Windows.Media;
        using Kit.Controls;

        public static class Snippet
        {
            public static object Build()
            {

        """;

    private const string Footer = """
            }
        }
        """;

    public static SnippetResult Run(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new SnippetResult(null, null, ["Nothing to run yet — paste some code."]);
        }

        var wholeFile = code.Contains("class ", StringComparison.Ordinal);
        var source = wholeFile ? code : Header + Normalize(code) + "\n" + Footer;
        var headerLines = wholeFile ? 0 : Header.Count(c => c == '\n');

        var tree = CSharpSyntaxTree.ParseText(
            source,
            new CSharpParseOptions(LanguageVersion.Latest),
            path: "snippet.cs");

        var compilation = CSharpCompilation.Create(
            "Kit.Snippet",
            [tree],
            References(),
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Debug));

        using var stream = new MemoryStream();
        var emitted = compilation.Emit(stream);

        if (!emitted.Success)
        {
            var errors = new List<string>();

            if (!wholeFile && !Balanced(Normalize(code)))
            {
                errors.Add(
                    "This looks like part of a file, not a snippet: the curly braces do not match. " +
                    "Paste the whole thing, or paste something that returns a control.");
            }

            var diagnostics = emitted.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => Describe(d, headerLines))
                .ToList();

            errors.AddRange(diagnostics.Take(5));

            if (diagnostics.Count > 5)
            {
                errors.Add($"...and {diagnostics.Count - 5} more.");
            }

            return new SnippetResult(null, null, errors);
        }

        stream.Position = 0;
        var assembly = Assembly.Load(stream.ToArray());
        var entry = FindBuild(assembly);

        if (entry is null)
        {
            // A whole control class pasted in: build the first one that can stand alone.
            var control = FindControl(assembly);
            if (control is null)
            {
                return new SnippetResult(null, null, [
                    "No Build() method and no control class to show. Return a control, or paste a class with a parameterless constructor."]);
            }

            try
            {
                return new SnippetResult(Activator.CreateInstance(control) as UIElement, null, []);
            }
            catch (Exception ex)
            {
                return new SnippetResult(null, null, [Describe(ex)]);
            }
        }

        object? value;
        try
        {
            value = entry.Invoke(null, null);
        }
        catch (TargetInvocationException ex)
        {
            return new SnippetResult(null, null, [Describe(ex.InnerException ?? ex)]);
        }
        catch (Exception ex)
        {
            return new SnippetResult(null, null, [Describe(ex)]);
        }

        return value switch
        {
            null => new SnippetResult(null, null, ["Build() returned null."]),
            UIElement element => new SnippetResult(element, null, []),
            _ => new SnippetResult(null, value.ToString(), [])
        };
    }

    /// <summary>
    /// People paste a snippet, not a method. If there is no return yet, make the obvious
    /// one: a bare `new PillButton {...}` gets returned, and a `var save = new ...`
    /// returns the last thing it built, so the preview shows the thing they just wrote.
    /// </summary>
    private static string Normalize(string code)
    {
        var body = code.TrimEnd();

        if (body.Length == 0 || body.Contains("return", StringComparison.Ordinal))
        {
            return body;
        }

        if (body.TrimStart().StartsWith("new ", StringComparison.Ordinal)
            && body.EndsWith(";", StringComparison.Ordinal)
            && body.Count(c => c == ';') == 1)
        {
            return "return " + body[..^1].TrimEnd() + ";";
        }

        var assignments = Regex.Matches(body, @"\b(\w+)\s*=\s*new\b");
        if (assignments.Count > 0)
        {
            var name = assignments[assignments.Count - 1].Groups[1].Value;
            return body + Environment.NewLine + "return " + name + ";";
        }

        return body + Environment.NewLine + "return null;";
    }

    /// <summary>Braces inside strings and comments are ignored; this is a shape check.</summary>
    private static bool Balanced(string code)
    {
        var depth = 0;
        var inString = false;
        var inChar = false;
        var escaped = false;

        foreach (var c in code)
        {
            if (escaped)
            {
                escaped = false;
                continue;
            }

            if (c == '\\' && (inString || inChar))
            {
                escaped = true;
                continue;
            }

            if (!inChar && c == '"')
            {
                inString = !inString;
                continue;
            }

            if (!inString && c == '\'')
            {
                inChar = !inChar;
                continue;
            }

            if (inString || inChar)
            {
                continue;
            }

            if (c == '{')
            {
                depth++;
            }
            else if (c == '}')
            {
                depth--;
                if (depth < 0)
                {
                    return false;
                }
            }
        }

        return depth == 0;
    }

    private static MethodInfo? FindBuild(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            var method = type.GetMethod(
                "Build",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
                Type.EmptyTypes);

            if (method is not null)
            {
                return method;
            }
        }

        return null;
    }

    private static Type? FindControl(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (typeof(UIElement).IsAssignableFrom(type)
                && !type.IsAbstract
                && type.GetConstructor(Type.EmptyTypes) is not null)
            {
                return type;
            }
        }

        return null;
    }

    private static string Describe(Diagnostic diagnostic, int headerLines)
    {
        var line = diagnostic.Location.GetLineSpan().StartLinePosition.Line + 1 - headerLines;
        var column = diagnostic.Location.GetLineSpan().StartLinePosition.Character + 1;

        if (headerLines == 0 || line < 1)
        {
            return $"{diagnostic.Id}: {diagnostic.GetMessage()}";
        }

        return $"{diagnostic.Id} (line {line}, col {column}): {diagnostic.GetMessage()}";
    }

    private static string Describe(Exception ex) => $"{ex.GetType().Name}: {ex.Message}";

    /// <summary>
    /// Everything WPF and KIT can see, plus the shared framework, so a snippet can use
    /// Brushes, Thickness and PillSize without extra using blocks of its own.
    /// </summary>
    private static List<MetadataReference> References()
    {
        if (_references is not null)
        {
            return _references;
        }

        var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") is string trusted)
        {
            foreach (var path in trusted.Split(Path.PathSeparator))
            {
                paths.Add(path);
            }
        }

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
            {
                paths.Add(assembly.Location);
            }
        }

        _references = paths
            .Where(File.Exists)
            .Select(path => (MetadataReference)MetadataReference.CreateFromFile(path))
            .ToList();

        return _references;
    }
}
