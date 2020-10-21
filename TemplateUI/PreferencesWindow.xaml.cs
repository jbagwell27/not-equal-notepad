using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Drawing.Text;
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
        public TemplateUISettings CurrentSettings;
        public TemplateUISettings NewSettings { get; set; }
        public bool canceled = false;
        public bool EntryAddPressed { get; set; }
        public PreferencesWindow()
        {
            CurrentSettings = new TemplateUISettings(Properties.Settings.Default.Color, Properties.Settings.Default.IsDarkMode,
                Properties.Settings.Default.FontSize, Properties.Settings.Default.FontFamily);
            NewSettings = new TemplateUISettings();
            InitializeComponent();
            EntryAddPressed = false;
            SetThemeElements(CurrentSettings);
            FontFamilyComboBox.Text = IsValidFont(CurrentSettings.FontFamily) ? CurrentSettings.FontFamily : "Segoe UI";
            foreach (var item in Properties.Settings.Default.FontFamilyList)
                FontFamilyComboBox.Items.Add(item);


        }

        private void SetThemeElements(TemplateUISettings setting)
        {
            if (setting.IsDarkMode)
            {
                DarkModeToggleSwitch.IsOn = true;
                ThemeManager.Current.ChangeTheme(this, $"Dark.{setting.Color}");
            }
            else
            {
                DarkModeToggleSwitch.IsOn = false;
                ThemeManager.Current.ChangeTheme(this, $"Light.{setting.Color}");
            }

            foreach (UIElement el in AccentColorPanel.Children)
            {
                RadioButton rb = el as RadioButton;
                string value = rb.Name.Replace("Radio", "");
                if (setting.Color == value)
                {
                    rb.IsChecked = true;
                }

            }
            FontFamilyComboBox.Text = setting.FontFamily;
            FontSizeSelectorBox.Value = setting.FontSize;

            FontFamilyComboBox.FontFamily = new FontFamily(setting.FontFamily);
            FontFamilyComboBox.FontSize = setting.FontSize;

            DataEntryBox.FontFamily = new FontFamily(setting.FontFamily);
            DataEntryBox.FontSize = setting.FontSize;
        }
        private void PreferencesSave_Click(object sender, RoutedEventArgs e)
        {
            NewSettings = GetTemporarySettings();
            SetThemeElements(NewSettings);
            SaveSettings(NewSettings);
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SetThemeElements(CurrentSettings);
            NewSettings = GetTemporarySettings();
            SaveSettings(NewSettings);
            this.Close();
        }
        
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SetThemeElements(GetTemporarySettings());
            SaveSettings(GetTemporarySettings());
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
                SetThemeElements(new TemplateUISettings(Properties.Settings.Default.Color, Properties.Settings.Default.IsDarkMode,
                    Properties.Settings.Default.FontSize, Properties.Settings.Default.FontFamily));
            }
        }
        private TemplateUISettings GetTemporarySettings()
        {
            TemplateUISettings tempSettings = new TemplateUISettings();
            tempSettings.FontFamily = FontFamilyComboBox.Text;
            tempSettings.FontSize = (int)FontSizeSelectorBox.Value;
            tempSettings.IsDarkMode = DarkModeToggleSwitch.IsOn;

            foreach (UIElement el in AccentColorPanel.Children)
            {
                RadioButton rb = el as RadioButton;
                if (rb.IsChecked.Value)
                {
                    tempSettings.Color = rb.Name.Replace("Radio", "");
                }
            }
            return tempSettings;
        }
        private void SaveSettings(TemplateUISettings settings)
        {
            //saves font family
            Properties.Settings.Default.FontFamily = settings.FontFamily;

            //checks if it is real, and adds it to the family list
            if (IsValidFont(settings.FontFamily)
                && !Properties.Settings.Default.FontFamilyList.Contains(settings.FontFamily))
            {
                Properties.Settings.Default.FontFamilyList.Add(settings.FontFamily);
            }

            Properties.Settings.Default.FontSize = settings.FontSize;
            Properties.Settings.Default.Color = settings.Color;
            Properties.Settings.Default.IsDarkMode = settings.IsDarkMode;
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

        private async void AddProductEntry_Click(object sender, RoutedEventArgs e)
        {
            string Product = null;
            foreach (UIElement item in DataEntriesRadioPanel.Children)
            {
                RadioButton rb = item as RadioButton;
                if (rb.IsChecked.Value)
                {
                    Product = rb.Name.Replace("Radio", "");
                }
            }

            if (string.IsNullOrEmpty(Product) || string.IsNullOrEmpty(DataEntryBox.Text))
            {
                await this.ShowMessageAsync("Try again, Sport", "One or more fields are empty");
            }
            else
            {
                ProductReader.AddEntry(Product, DataEntryBox.Text);
                DataEntryBox.Clear();
                EntryAddPressed = true;
            }
        }

        private void DataEntryBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                AddProductEntry_Click(sender, e);
            }
        }

        private void PreferenceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetThemeElements(CurrentSettings);
            NewSettings = GetTemporarySettings();
            SaveSettings(NewSettings);
        }
    }

    public class TemplateUISettings
    {
        public string Color { get; set; }
        public bool IsDarkMode { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
        public TemplateUISettings(string color, bool isdark, int fontsize, string fontfamily)
        {
            Color = color;
            IsDarkMode = isdark;
            FontSize = fontsize;
            FontFamily = fontfamily;
        }
        public TemplateUISettings()
        {

        }
        public bool hasNullValues()
        {
            return Color == null || FontSize == 0 || FontFamily == null;
        }
    }
}
