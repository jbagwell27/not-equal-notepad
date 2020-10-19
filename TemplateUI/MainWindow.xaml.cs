using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using TemplateUI.Logic;
using TextCopy;
using ControlzEx.Theming;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private GenericInfo Ginfo;
        private ProductInfo Pinfo;
        private ProductReader Preader;
        private string OSVersion;
        private string WindowsEdition;
        private string OSArchitecture;

        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.IsDarkMode)
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            else
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            Ginfo = new GenericInfo();
            Pinfo = new ProductInfo();
            Preader = new ProductReader();

            //Initial setup process.
            FillComboBoxes();
            TemplatePreviewTab.IsEnabled = false;
            ContactNameBox.Focus();


        }

        private void FillComboBoxes()
        {
            foreach (var item in Preader.ImagingVersions)
                ImagingVersionBox.Items.Add(item);
            foreach (var item in Preader.PMSVersions)
                PMSVersionBox.Items.Add(item);
            foreach (var item in Preader.BridgeVersions)
                BridgeVersionBox.Items.Add(item);
            foreach (var item in Preader.DeviceTypes)
                DeviceTypeBox.Items.Add(item);
            foreach (var item in Preader.DriverVersions)
                DriverVersionBox.Items.Add(item);
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Ginfo.ContactName = !string.IsNullOrEmpty(ContactNameBox.Text) ? ContactNameBox.Text : null;
            Ginfo.PhoneNumber = !string.IsNullOrEmpty(PhoneNumberBox.Text) ? PhoneNumberBox.Text : null;
            Ginfo.EmailAddress = !string.IsNullOrEmpty(EmailAddressBox.Text) ? EmailAddressBox.Text : null;
            Ginfo.CaseNumber = !string.IsNullOrEmpty(CaseNumberBox.Text) ? CaseNumberBox.Text : null;
            Ginfo.IssueSummary = !string.IsNullOrEmpty(IssueSummaryBox.Text) ? IssueSummaryBox.Text : null;
            Ginfo.IssueDetails = !string.IsNullOrEmpty(IssueDetailsBox.Text) ? IssueDetailsBox.Text : null;
            Ginfo.TroubleShootingSteps = !string.IsNullOrEmpty(TroubleshootStepsBox.Text) ? TroubleshootStepsBox.Text : null;
            Ginfo.ResolutionDetails = !string.IsNullOrEmpty(ResolutionBox.Text) ? ResolutionBox.Text : null;

            Pinfo.Imaging = ImagingVersionBox.SelectedItem?.ToString();
            Pinfo.PMS = PMSVersionBox.SelectedItem?.ToString();
            Pinfo.Bridge = BridgeVersionBox.SelectedItem?.ToString();
            Pinfo.DatabasePath = !string.IsNullOrEmpty(DatabasePathBox.Text) ? DatabasePathBox.Text : null;
            Pinfo.DeviceType = DeviceTypeBox.SelectedItem?.ToString();
            Pinfo.SerialNumber = !string.IsNullOrEmpty(SerialNumberBox.Text) ? SerialNumberBox.Text : null;
            Pinfo.Driver = DriverVersionBox.SelectedItem?.ToString();

            string computerList = "";
            if (!RemoteSessionListBox.Items.IsEmpty)
            {
                computerList = "Remote Sessions:\n";
                foreach (Computer cp in RemoteSessionListBox.Items)
                {
                    computerList += $"{cp}\n";
                }
            }
            string templateString = $"{Ginfo}{Pinfo}\n{computerList}";

            LogWriter.AddLogEntry(templateString);
            TemplatePreviewTab.IsEnabled = true;
            TemplatePreviewBox.Text = templateString;

            ClipboardService.SetText(templateString);

        }

        private async void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "You will be clearing everything", MessageDialogStyle.AffirmativeAndNegative, settings);

            if (result == MessageDialogResult.Affirmative)
            {


                foreach (UIElement el in GenericInfoGrid.Children)
                {
                    if (el.GetType() == typeof(TextBox))
                        ((TextBox)(el)).Clear();
                }
                foreach (UIElement el in DeviceInfoGrid.Children)
                {
                    if (el.GetType() == typeof(TextBox))
                        ((TextBox)(el)).Clear();
                    if (el.GetType() == typeof(ComboBox))
                        ((ComboBox)(el)).SelectedIndex = -1;
                }
                foreach (UIElement el in SoftwareInfoGrid.Children)
                {
                    if (el.GetType() == typeof(TextBox))
                        ((TextBox)(el)).Clear();
                    if (el.GetType() == typeof(ComboBox))
                        ((ComboBox)(el)).SelectedIndex = -1;
                }
                Ginfo.Clear();
                Pinfo.Clear();
                ClearRemoteSessionScreen();
                RemoteSessionListBox.Items.Clear();
                TemplatePreviewBox.Clear();
                TemplatePreviewTab.IsEnabled = false;
                GenericInfoTab.IsSelected = true;
                ContactNameBox.Focus();
            }

        }

        private void ClearRemoteSessionScreen()
        {
            ComputerNameBox.Clear();
            foreach (UIElement el in OSPanel.Children)
            {
                if (el.GetType() == typeof(RadioButton))
                    ((RadioButton)(el)).IsChecked = false;
            }
            foreach (UIElement el in EditionPanel.Children)
            {
                if (el.GetType() == typeof(RadioButton))
                    ((RadioButton)(el)).IsChecked = false;
            }
            foreach (UIElement el in ArchitecturePanel.Children)
            {
                if (el.GetType() == typeof(RadioButton))
                    ((RadioButton)(el)).IsChecked = false;
            }

        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            CancellationToken token;
            TaskScheduler uiSched = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(DialogsBeforeExit, token, TaskCreationOptions.None, uiSched);
        }
        private async void DialogsBeforeExit()
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            MessageDialogResult result = await this.ShowMessageAsync("Are you sure want to exit?", "No information will be saved.", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result == MessageDialogResult.Negative)
            {
                return;
            }
            else
            {
                int count = 0;
                foreach (Window win in Application.Current.Windows)
                {
                    if (win.Visibility == Visibility.Visible)
                    {
                        count++;
                    }
                }
                if (count > 2)
                {
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Application.Current.Shutdown();
                }

            }
        }

        private void OSRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton ck = sender as RadioButton;

            if (ck.Content.ToString().Contains("Server"))
            {
                EditionPanel.IsEnabled = false;
            }
            else
            {
                EditionPanel.IsEnabled = true;
            }
            this.OSVersion = ck.Content.ToString();
        }
        private void EditionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton ck = sender as RadioButton;
            WindowsEdition = ck.Content.ToString();
        }
        private void ArchRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton ck = sender as RadioButton;
            OSArchitecture = ck.Content.ToString();
        }


        private async void AddComputerButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ComputerNameBox.Text) || string.IsNullOrEmpty(OSVersion) || string.IsNullOrEmpty(OSArchitecture) ||
                (string.IsNullOrEmpty(WindowsEdition) && EditionPanel.IsEnabled))
            {
                await this.ShowMessageAsync("Some fields are empty.", "Please fill out all available fields.");
            }
            else
            {
                RemoteSessionListBox.Items.Add(new Computer(ComputerNameBox.Text, OSVersion, WindowsEdition, OSArchitecture));
                ClearRemoteSessionScreen();
            }
        }

        private void CopyDescription_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            if (string.IsNullOrEmpty(IssueDetailsBox.Text))
                result = $"{IssueSummaryBox.Text}\n";
            else
                result = $"{IssueDetailsBox.Text}\n";
            if (!RemoteSessionListBox.Items.IsEmpty)
                result += "Sessions:\n";
            foreach (Computer cp in RemoteSessionListBox.Items)
            {
                result += $"{cp}\n";
            }
            ClipboardService.SetText(result);
        }

        private void CopyTroubleshootSteps_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            foreach (string s in TroubleshootStepsBox.Text.Split('\n'))
            {
                result += $"\u2022 {s}";
            }
            ClipboardService.SetText(result);
        }

        private async void ClearComputerList_Click(object sender, RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "You will be clearing everything", MessageDialogStyle.AffirmativeAndNegative, settings);

            if (result == MessageDialogResult.Affirmative)
            {
                RemoteSessionListBox.Items.Clear();
            }
        }

        private void CopyComputerName_Click(object sender, RoutedEventArgs e)
        {
            if (RemoteSessionListBox.SelectedItem != null)
            {
                ClipboardService.SetText(((Computer)(RemoteSessionListBox.SelectedItem)).Name);
            }
        }

        private void RemoveSingleComputer_Click(object sender, RoutedEventArgs e)
        {
            if (RemoteSessionListBox.SelectedItem != null)
            {
                RemoteSessionListBox.Items.RemoveAt(RemoteSessionListBox.SelectedIndex);
            }


        }

        private void NewWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newWindow.Show();
        }

        private void DuplicateWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newWindow.ContactNameBox.Text = this.ContactNameBox.Text;
            newWindow.PhoneNumberBox.Text = this.PhoneNumberBox.Text;
            newWindow.EmailAddressBox.Text = this.EmailAddressBox.Text;
            newWindow.ImagingVersionBox.SelectedIndex = this.ImagingVersionBox.SelectedIndex;
            newWindow.PMSVersionBox.SelectedIndex = this.PMSVersionBox.SelectedIndex;
            newWindow.BridgeVersionBox.SelectedIndex = this.BridgeVersionBox.SelectedIndex;
            newWindow.DatabasePathBox.Text = this.DatabasePathBox.Text;

            newWindow.Show();
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void TodayLogItem_Click(object sender, RoutedEventArgs e)
        {
            if (LogWriter.LaunchTodaysLogFile() == 0)
            {
                await this.ShowMessageAsync("Log file not found", "Please verify the log is in the 'CaseHistory' Folder");
            }
        }

        private async void PreviousDayLogItem_Click(object sender, RoutedEventArgs e)
        {
            if (LogWriter.LaunchPreviousDayLogFile() == 0)
            {
                await this.ShowMessageAsync("Log file not found", "Please verify the log is in the 'CaseHistory' Folder");
            }
        }

        private async void OpenLogFolder_Click(object sender, RoutedEventArgs e)
        {
            if (LogWriter.OpenLogFolder() == 0)
            {
                await this.ShowMessageAsync("'CaseHistory' folder not found", $"Please verify the location of the 'CaseHistory' Folder.\n" +
                    $"It should be in:\n'{System.IO.Directory.GetCurrentDirectory()}'");
            }
        }

        private void Win10Pro64_Click(object sender, RoutedEventArgs e)
        {
            Win10Radio.IsChecked = true;
            ProRadio.IsChecked = true;
            x64Radio.IsChecked = true;
        }

        private void SelectAllTriple_Click(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (e.ClickCount ==3)
            {
                tb.SelectAll();
            }
        }
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aw.ShowDialog();
        }

        private void PreferencesItem_Click(object sender, RoutedEventArgs e)
        {
            PreferencesWindow pw = new PreferencesWindow();
            pw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            pw.ShowDialog();
        }

        private void EasterEggImage_Click(object sender, MouseButtonEventArgs e)
        {
            new EasterEggWindow().ShowDialog();
        }
    }
}
