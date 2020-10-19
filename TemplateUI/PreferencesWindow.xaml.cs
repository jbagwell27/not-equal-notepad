using MahApps.Metro.Controls;
using System.Windows;
using System.Drawing.Text;
using MahApps.Metro.Controls.Dialogs;
using ControlzEx.Theming;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : MetroWindow
    {
        private bool DarkModeSwitchEventTrigger;
        public PreferencesWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.IsDarkMode)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
                DarkModeToggleSwitch.IsOn = true;
            }

            else
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
                DarkModeToggleSwitch.IsOn = false;
            }
            FontFamilyComboBox.Text = IsValidFont(Properties.Settings.Default.FontFamily) ? Properties.Settings.Default.FontFamily : "Segoe UI";
            foreach (var item in Properties.Settings.Default.FontFamilyList)
                FontFamilyComboBox.Items.Add(item);
            NumericUpDownBox.Value = Properties.Settings.Default.FontSize;
        }

        private async void PreferencesSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            if (DarkModeSwitchEventTrigger)
            {
                await this.ShowMessageAsync("Dark Mode Changed","You will need to restart to see the theme changes");
            }
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveSettings()
        {
            if (!string.IsNullOrEmpty(FontFamilyComboBox.Text))
            {
                Properties.Settings.Default.FontFamily = FontFamilyComboBox.Text;
                if (IsValidFont(FontFamilyComboBox.Text)
                    && !Properties.Settings.Default.FontFamilyList.Contains(FontFamilyComboBox.Text))
                {
                    Properties.Settings.Default.FontFamilyList.Add(FontFamilyComboBox.Text);
                }
            }
            Properties.Settings.Default.FontSize = (int)(NumericUpDownBox.Value);
            

            Properties.Settings.Default.Save();
        }

        private bool IsValidFont(string fontFamilyName)
        {
            foreach (var item in new InstalledFontCollection().Families)
            {
                if (item.Name == fontFamilyName)
                {
                    return true;
                }
            }
            return false;
        }

        private void DarkMode_Toggled(object sender, RoutedEventArgs e)
        {
            DarkModeSwitchEventTrigger = true;
            if (DarkModeToggleSwitch.IsOn)
            {
                Properties.Settings.Default.IsDarkMode = true;
            }
            else
            {
                Properties.Settings.Default.IsDarkMode = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}
