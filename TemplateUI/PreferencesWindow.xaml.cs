using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Controls;

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
            SetTheme();
            FontFamilyComboBox.Text = IsValidFont(Properties.Settings.Default.FontFamily) ? Properties.Settings.Default.FontFamily : "Segoe UI";
            foreach (var item in Properties.Settings.Default.FontFamilyList)
                FontFamilyComboBox.Items.Add(item);
            NumericUpDownBox.Value = Properties.Settings.Default.FontSize;
        }
        private void SetTheme()
        {
            if (Properties.Settings.Default.IsDarkMode)
            {
                DarkModeToggleSwitch.IsOn = true;
                ThemeManager.Current.ChangeTheme(this, $"Dark.{Properties.Settings.Default.Theme}");
            }
            else
            {
                DarkModeToggleSwitch.IsOn = false;
                ThemeManager.Current.ChangeTheme(this, $"Light.{Properties.Settings.Default.Theme}");
            }
            switch (Properties.Settings.Default.Theme)
            {
                case "Red":
                    RedRadio.IsChecked = true;
                        break;
                case "Orange":
                    OrangeRadio.IsChecked = true;
                    break;
                case "Yellow":
                    YellowRadio.IsChecked = true;
                    break;
                case "Green":
                    GreenRadio.IsChecked = true;
                    break;
                case "Blue":
                    BlueRadio.IsChecked = true;
                    break;
                case "Purple":
                    PurpleRadio.IsChecked = true;
                    break;
                case "Pink":
                    PinkRadio.IsChecked = true;
                    break;
                case "Magenta":
                    MagentaRadio.IsChecked = true;
                    break;
                case "Cyan":
                    CyanRadio.IsChecked = true;
                    break;
                default:
                    break;
            }

        }

        private async void PreferencesSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            if (DarkModeSwitchEventTrigger)
            {
                await this.ShowMessageAsync("Dark Mode Changed", "You will need to restart to see the theme changes");
            }

            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
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

        private void ThemeColorRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Properties.Settings.Default.Theme = rb.Content.ToString();
        }
    }
}
