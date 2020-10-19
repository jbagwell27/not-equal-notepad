using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Windows;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for EasterEggWindow.xaml
    /// </summary>
    public partial class EasterEggWindow : MetroWindow
    {
        public EasterEggWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.IsDarkMode)
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            else
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            this.WindowStartupLocation = WindowStartupLocation.Manual;
        }
    }
}
