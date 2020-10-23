using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
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
        public bool ItemAdded { get; set; }
        public bool ItemsReset { get; set; }
        public bool ItemRemoved { get; set; }
        private string DataRadioButton { get; set; }
        public PreferencesWindow()
        {
            CurrentSettings = new TemplateUISettings()
            {
                Color = Properties.Settings.Default.Color,
                IsDarkMode = Properties.Settings.Default.IsDarkMode,
                FontFamily = Properties.Settings.Default.FontFamily,
                FontSize = Properties.Settings.Default.FontSize
            };
            NewSettings = new TemplateUISettings();
            InitializeComponent();
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
            //SetThemeElements(NewSettings);
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
                //Affirmative is set to no because button theming

                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "This will reset all saved settings and restore to default", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result == MessageDialogResult.Negative)
            {
                Properties.Settings.Default.Reset();
                SetThemeElements(new TemplateUISettings()
                {
                    Color = Properties.Settings.Default.Color,
                    IsDarkMode = Properties.Settings.Default.IsDarkMode,
                    FontFamily = Properties.Settings.Default.FontFamily,
                    FontSize = Properties.Settings.Default.FontSize
                });
                ProductReader.Rebuild();
                ItemsReset = true;
                this.PreferencesSave_Click(sender, e);
            }
        }
        private TemplateUISettings GetTemporarySettings()
        {
            TemplateUISettings tempSettings = new TemplateUISettings()
            {
                FontFamily = FontFamilyComboBox.Text,
                FontSize = (int)FontSizeSelectorBox.Value,
                IsDarkMode = DarkModeToggleSwitch.IsOn

            };

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
                string s = DataEntryBox.Text;
                ProductReader.AddEntry(Product, s);
                DataEntryBox.Clear();
                RebuildDataList();
                DataList.SelectedIndex = DataList.Items.IndexOf(s);
                DataList.ScrollIntoView(DataList.SelectedItem);
                ItemAdded = true;
            }
        }

        private void DataEntryBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                AddProductEntry_Click(sender, e);
            }
        }

        private void ProductRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            DataRadioButton = rb.Name.Replace("Radio", "");
            DataList.Items.Clear();
            foreach (var item in ProductReader.GetProductsList(DataRadioButton))
            {
                DataList.Items.Add(item);
            }
        }
        private void RebuildDataList()
        {
            DataList.Items.Clear();
            foreach (var item in ProductReader.GetProductsList(DataRadioButton))
            {
                DataList.Items.Add(item);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (DataList.SelectedItem != null)
            {
                DataList.Items.Remove(DataList.SelectedItem);
            }
            List<string> products = new List<string>();
            foreach (string s in DataList.Items)
            {
                products.Add(s);
            }
            ProductReader.SetProductsList(products.ToArray(), DataRadioButton);
            ItemRemoved = true;
        }
    }

    public class TemplateUISettings
    {
        public string Color { get; set; }
        public bool IsDarkMode { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
    }
}
