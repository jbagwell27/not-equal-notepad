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
            SetTheme();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
        }

        private void SetTheme()
        {
            if (Properties.Settings.Default.IsDarkMode)
                ThemeManager.Current.ChangeTheme(this, $"Dark.{Properties.Settings.Default.Color}");
            else
                ThemeManager.Current.ChangeTheme(this, $"Light.{Properties.Settings.Default.Color}");
        }
    }
}
