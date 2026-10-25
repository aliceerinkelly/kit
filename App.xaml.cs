using System.Windows;
using Kit.Theming;

namespace Kit;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ThemeManager.Apply(KitThemes.Accents[0], dark: true);
        base.OnStartup(e);
    }
}
