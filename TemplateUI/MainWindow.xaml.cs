using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private ProductReader Preader;
        private List<Computer> RemoteSessions;
        private string OSVersion;
        private string WindowsEdition;
        private string OSArchitecture;

        public MainWindow()
        {
            InitializeComponent();

            Ginfo = new GenericInfo();
            Pinfo = new ProductInfo();
            Preader = new ProductReader();
            RemoteSessions = new List<Computer>();

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

            string templateString = Ginfo.ToString() + Pinfo.ToString();
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
            }

            Ginfo.Clear();
            Pinfo.Clear();
            TemplatePreviewBox.Clear();
            TemplatePreviewTab.IsEnabled = false;
            GenericInfoTab.IsSelected = true;
            ContactNameBox.Focus();
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
                Application.Current.Shutdown();
            }
        }

        private void OSRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton ck = sender as RadioButton;

            if (ck.Content.ToString().Contains("Server"))
            {
                WindowsEditionPanel.IsEnabled = false;
            }
            else
            {
                WindowsEditionPanel.IsEnabled = true;
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
            if (string.IsNullOrEmpty(ComputerNameBox.Text) || string.IsNullOrEmpty(OSVersion) || 
                string.IsNullOrEmpty(WindowsEdition) || string.IsNullOrEmpty(OSArchitecture))
            {
                await this.ShowMessageAsync("Some fields are empty.", "Please fill out all available fields.");
            }
            Computer cp = new Computer();
            cp.Name = ComputerNameBox.Text;
            cp.OSVersion = OSVersion;
            cp.Edition = WindowsEdition;
            cp.Architecture = OSArchitecture;


        }


    }
}
