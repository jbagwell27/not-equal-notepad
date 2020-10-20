using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Diagnostics;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            SetTheme();
        }
        private void SetTheme()
        {
            if (Properties.Settings.Default.IsDarkMode)
                ThemeManager.Current.ChangeTheme(this, $"Dark.{Properties.Settings.Default.Theme}");
            else
                ThemeManager.Current.ChangeTheme(this, $"Light.{Properties.Settings.Default.Theme}");
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = e.Uri.ToString(),
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
