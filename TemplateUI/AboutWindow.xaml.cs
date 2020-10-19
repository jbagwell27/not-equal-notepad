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
            if (Properties.Settings.Default.IsDarkMode)
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            else
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
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
