using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Drawing.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TemplateUI.Logic;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : MetroWindow
    {
        string Product;
        public bool EntryAddPressed { get; set; }
        public PreferencesWindow()
        {
            InitializeComponent();
            EntryAddPressed = true;
            SetTheme();
            FontFamilyComboBox.Text = IsValidFont(Properties.Settings.Default.FontFamily) ? Properties.Settings.Default.FontFamily : "Segoe UI";
            foreach (var item in Properties.Settings.Default.FontFamilyList)
                FontFamilyComboBox.Items.Add(item);
           

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

            foreach (UIElement el in AccentColorPanel.Children)
            {
                RadioButton rb = el as RadioButton;
                string value = rb.Name.Replace("Radio", "");
                if (Properties.Settings.Default.Theme == value)
                {
                    rb.IsChecked = true;
                }

                FontSizeSelectorBox.Value = Properties.Settings.Default.FontSize;
                StyleExtraElements();
            }
        }

        private void PreferencesSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            SetTheme();
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            SetTheme();
        }
        private async void RestoreDefaults_Click(object sender, RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "This will reset all saved settings and restore to default", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result == MessageDialogResult.Negative)
            {
                Properties.Settings.Default.Reset();
                SetTheme();
            }
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
            Properties.Settings.Default.FontSize = (int)(FontSizeSelectorBox.Value);
            StyleExtraElements();
            Properties.Settings.Default.Save();
        }

        private void StyleExtraElements()
        {
            FontFamilyComboBox.FontFamily = new FontFamily(Properties.Settings.Default.FontFamily);
            FontFamilyComboBox.FontSize = Properties.Settings.Default.FontSize;
            DataEntryBox.FontFamily = new FontFamily(Properties.Settings.Default.FontFamily);
            DataEntryBox.FontSize = Properties.Settings.Default.FontSize;
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
            if (DarkModeToggleSwitch.IsOn)
            {
                Properties.Settings.Default.IsDarkMode = true;
            }
            else
            {
                Properties.Settings.Default.IsDarkMode = false;
            }
        }

        private void ThemeColorRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Properties.Settings.Default.Theme = rb.Name.Replace("Radio", "");
        }

        private async void AddProductEntry_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Product) || string.IsNullOrEmpty(DataEntryBox.Text))
            {
                await this.ShowMessageAsync("Try again, Sport", "One or more fields are empty");
            }
            else
            {
                EntryAddPressed = true;
                ProductReader.AddEntry(Product, DataEntryBox.Text);
                DataEntryBox.Clear();
            }
        }

        private void ProductRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Product = rb.Name.Replace("Radio", "");
        }

        private void DataEntryBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                AddProductEntry_Click(sender, e);
            }
        }
    }
}
