using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

        GenericInfo Ginfo;
        ProductInfo Pinfo;
        ProductReader Preader;
        LogWriter Lwriter;
        public MainWindow()
        {
            InitializeComponent();

            Ginfo = new GenericInfo();
            Pinfo = new ProductInfo();
            Lwriter = new LogWriter();
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
            Ginfo.ContactName = ContactNameBox.Text != string.Empty ? ContactNameBox.Text : null;
            Ginfo.PhoneNumber = PhoneNumberBox.Text != string.Empty ? PhoneNumberBox.Text : null;
            Ginfo.EmailAddress = EmailAddressBox.Text != string.Empty ? EmailAddressBox.Text : null;
            Ginfo.CaseNumber = CaseNumberBox.Text != string.Empty ? CaseNumberBox.Text : null;
            Ginfo.IssueSummary = IssueSummaryBox.Text != string.Empty ? IssueSummaryBox.Text : null;
            Ginfo.IssueDetails = IssueDetailsBox.Text != string.Empty ? IssueDetailsBox.Text : null;
            Ginfo.TroubleShootingSteps = TroubleshootStepsBox.Text != string.Empty ? TroubleshootStepsBox.Text : null;
            Ginfo.ResolutionDetails = ResolutionBox.Text != string.Empty ? ResolutionBox.Text : null;

            Pinfo.Imaging = ImagingVersionBox.SelectedItem?.ToString(); 
            Pinfo.PMS = PMSVersionBox.SelectedItem?.ToString();
            Pinfo.Bridge = BridgeVersionBox.SelectedItem?.ToString();
            Pinfo.DatabasePath = DatabasePathBox.Text != string.Empty ? DatabasePathBox.Text : null;
            Pinfo.DeviceType = DeviceTypeBox.SelectedItem?.ToString();
            Pinfo.SerialNumber = SerialNumberBox.Text != string.Empty ? SerialNumberBox.Text : null;
            Pinfo.Driver = DriverVersionBox.SelectedItem?.ToString();

            string templateString = Ginfo.ToString() + Pinfo.ToString();
            Lwriter.AddLogEntry(templateString);
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

    }
}
