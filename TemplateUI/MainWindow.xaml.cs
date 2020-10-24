using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TemplateUI.Logic;
using TextCopy;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private GenericInfo Ginfo;
        private ProductInfo Pinfo;
        private TemplateUISettings _UISettings;
        public TemplateUISettings UISettings
        {
            get { return _UISettings; }
            set { _UISettings = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Ginfo = new GenericInfo();
            Pinfo = new ProductInfo();
            _UISettings = new TemplateUISettings()
            {
                Color = Properties.Settings.Default.Color,
                IsDarkMode = Properties.Settings.Default.IsDarkMode,
                FontFamily = Properties.Settings.Default.FontFamily,
                FontSize = Properties.Settings.Default.FontSize
            };
            LogWriter.CreateTodaysLog();

            KeyBinding OpenCmdKeyBinding = new KeyBinding(
                ApplicationCommands.New,
                Key.R,
                ModifierKeys.Control);

            this.InputBindings.Add(OpenCmdKeyBinding);


            //Initial setup process.
            FillComboBoxes();

            SetTheme(new TemplateUISettings()
            {
                Color = Properties.Settings.Default.Color,
                IsDarkMode = Properties.Settings.Default.IsDarkMode,
                FontFamily = Properties.Settings.Default.FontFamily,
                FontSize = Properties.Settings.Default.FontSize

            });
            DataContext = UISettings;
            ContactNameBox.Focus();
        }

        private void SetTheme(TemplateUISettings settings)
        {
            UISettings = settings;
            DataContext = UISettings;
            if (settings.IsDarkMode)

                ThemeManager.Current.ChangeTheme(this, $"Dark.{settings.Color}");
            else
                ThemeManager.Current.ChangeTheme(this, $"Light.{settings.Color}");

            foreach (UIElement el in GenericInfoGrid.Children)
            {
                if (el.GetType() == typeof(TextBox))
                {
                    ((TextBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((TextBox)(el)).FontSize = settings.FontSize;
                }
            }
            foreach (UIElement el in DeviceInfoGrid.Children)
            {
                if (el.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((ComboBox)(el)).FontSize = settings.FontSize;
                }
                if (el.GetType() == typeof(TextBox))
                {
                    ((TextBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((TextBox)(el)).FontSize = settings.FontSize;
                }
            }
            foreach (UIElement el in SoftwareInfoGrid.Children)
            {
                if (el.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((ComboBox)(el)).FontSize = settings.FontSize;
                }
                if (el.GetType() == typeof(TextBox))
                {
                    ((TextBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((TextBox)(el)).FontSize = settings.FontSize;
                }
            }
            foreach (UIElement el in RemoteSessionsGrid.Children)
            {
                if (el.GetType() == typeof(TextBox))
                {
                    ((TextBox)(el)).FontFamily = new FontFamily(settings.FontFamily);
                    ((TextBox)(el)).FontSize = settings.FontSize;
                }
            }
            TemplatePreviewBox.FontFamily = new FontFamily(settings.FontFamily);
            TemplatePreviewBox.FontSize = settings.FontSize;
            BlankField.FontFamily = new FontFamily(settings.FontFamily);
            BlankField.FontSize = settings.FontSize;
        }

        private void FillComboBoxes()
        {
            foreach (var item in ProductReader.GetImaging())
                ImagingBox.Items.Add(item);
            foreach (var item in ProductReader.GetPMS())
                PMSBox.Items.Add(item);
            foreach (var item in ProductReader.GetBridges())
                BridgesBox.Items.Add(item);
            foreach (var item in ProductReader.GetDevices())
                DevicesBox.Items.Add(item);
            foreach (var item in ProductReader.GetDrivers())
                DriversBox.Items.Add(item);
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

            Pinfo.Imaging = ImagingBox.SelectedItem?.ToString();
            Pinfo.PMS = PMSBox.SelectedItem?.ToString();
            Pinfo.Bridge = BridgesBox.SelectedItem?.ToString();
            Pinfo.DatabasePath = !string.IsNullOrEmpty(DatabasePathBox.Text) ? DatabasePathBox.Text : null;
            Pinfo.DeviceType = DevicesBox.SelectedItem?.ToString();
            Pinfo.SerialNumber = !string.IsNullOrEmpty(SerialNumberBox.Text) ? SerialNumberBox.Text : null;
            Pinfo.Driver = DriversBox.SelectedItem?.ToString();

            string computerList = "";
            if (!RemoteSessionListBox.Items.IsEmpty)
            {
                computerList = "Remote Sessions:\n";
                foreach (Computer cp in RemoteSessionListBox.Items)
                    computerList += $"{cp}\n";
            }
            string templateString = $"{Ginfo}\n" +
                "---------- Environment----------\n\n" +
                $"{Pinfo}\n{computerList}";

            LogWriter.AddLogEntry(templateString);
            TemplatePreviewBox.Text = templateString;

            ClipboardService.SetText(templateString);

        }

        private async void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            ContactNameBox.Focus();
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "You will be clearing everything", MessageDialogStyle.AffirmativeAndNegative, settings);

            if (result == MessageDialogResult.Negative)
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
                BlankField.Clear();


            }

        }
        private void CopyIssue_Click(object sender, RoutedEventArgs e)
        {
            string summary = IssueSummaryBox.Text;
            string details = IssueDetailsBox.Text;
            ClipboardService.SetText($"{summary}\n{details}");
        }

        private void CopyDescription_Click(object sender, RoutedEventArgs e)
        {
            string result;

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
            EditionPanel.IsEnabled = true;

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
            //I switched up the affirmative and negative buttons as a cheap way to change button colors
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
            };
            MessageDialogResult result = await this.ShowMessageAsync("Are you sure want to exit?", "No information will be saved.", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result == MessageDialogResult.Negative)
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

            if (ck.Name.Contains("Server"))
            {
                EditionPanel.IsEnabled = false;
                foreach (UIElement el in EditionPanel.Children)
                {
                    RadioButton rb = el as RadioButton;
                    rb.IsChecked = false;
                }
            }
            else
            {
                EditionPanel.IsEnabled = true;
            }
        }
        private void EditionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void ArchRadioButton_Checked(object sender, RoutedEventArgs e)
        {
        }


        private async void AddComputerButton_Click(object sender, RoutedEventArgs e)
        {
            string OSVersion = null;
            string OSEdition = null;
            string OSArchitecture = null;

            foreach (UIElement item in OSPanel.Children)
            {
                RadioButton rb = item as RadioButton;
                if (rb.IsChecked.Value)
                {
                    OSVersion = rb.Name.Replace("Radio", "").Replace("Server", "Server 20").Replace("Win", "");
                }
            }
            foreach (UIElement item in EditionPanel.Children)
            {
                RadioButton rb = item as RadioButton;
                if (rb.IsChecked.Value)
                {
                    OSEdition = rb.Name.Replace("Radio", "");
                }
            }
            foreach (UIElement item in ArchitecturePanel.Children)
            {
                RadioButton rb = item as RadioButton;
                if (rb.IsChecked.Value)
                {
                    OSArchitecture = rb.Name.Replace("Radio", "");
                }
            }

            if (string.IsNullOrEmpty(ComputerNameBox.Text) || string.IsNullOrEmpty(OSVersion) || string.IsNullOrEmpty(OSArchitecture) ||
                (string.IsNullOrEmpty(OSEdition) && EditionPanel.IsEnabled))
            {
                await this.ShowMessageAsync("Some fields are empty.", "Please fill out all available fields.");
            }
            else
            {
                RemoteSessionListBox.Items.Add(new Computer(ComputerNameBox.Text, OSVersion, OSEdition, OSArchitecture));
                ClearRemoteSessionScreen();
            }
        }



        private async void ClearComputerList_Click(object sender, RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
            };
            var result = await this.ShowMessageAsync("Are you sure?", "You will be clearing everything", MessageDialogStyle.AffirmativeAndNegative, settings);

            if (result == MessageDialogResult.Negative)
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
            var newWindow = new MainWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            newWindow.Show();
        }

        private void DuplicateWindow_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            newWindow.ContactNameBox.Text = this.ContactNameBox.Text;
            newWindow.PhoneNumberBox.Text = this.PhoneNumberBox.Text;
            newWindow.EmailAddressBox.Text = this.EmailAddressBox.Text;
            newWindow.ImagingBox.SelectedIndex = this.ImagingBox.SelectedIndex;
            newWindow.PMSBox.SelectedIndex = this.PMSBox.SelectedIndex;
            newWindow.BridgesBox.SelectedIndex = this.BridgesBox.SelectedIndex;
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
            if (e.ClickCount == 3)
            {
                tb.SelectAll();
            }
        }
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aw = new AboutWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            aw.ShowDialog();
        }

        private void PreferencesItem_Click(object sender, RoutedEventArgs e)
        {
            PreferencesWindow pw = new PreferencesWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            pw.ShowDialog();
            SetTheme(pw.NewSettings);

            if (pw.ItemAdded || pw.ItemsReset || pw.ItemRemoved)
            {
                UpdateDropDowns();
            }
        }

        private void UpdateDropDowns()
        {
            DevicesBox.Items.Clear();
            DriversBox.Items.Clear();
            ImagingBox.Items.Clear();
            PMSBox.Items.Clear();
            BridgesBox.Items.Clear();

            FillComboBoxes();
        }

        private void EasterEggImage_Click(object sender, MouseButtonEventArgs e)
        {
            new EasterEggWindow().ShowDialog();
        }

        private void SingleLineTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (Keyboard.PrimaryDevice.IsKeyDown(Key.Tab))
            {
                tb.SelectAll();
            }

        }

        private void RefreshProductList_Click(object sender, RoutedEventArgs e)
        {
            UpdateDropDowns();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                NewWindow_Click(sender, e);
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D)
            {
                DuplicateWindow_Click(sender, e);
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
            {
                PreferencesItem_Click(sender, e);
            }
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
