using ControlzEx.Theming;
using MahApps.Metro.Controls;
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

        GenericInfo GInfo;
        ProductReader Pr;
        LogWriter Lw;
        public MainWindow()
        {
            InitializeComponent();
            
            GInfo = new GenericInfo();
            Lw = new LogWriter();
            Pr = new ProductReader();

            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            foreach (var item in Pr.ImagingVersions)
                ImagingVersionBox.Items.Add(item);
            foreach (var item in Pr.PMSVersions)
                PMSVersionBox.Items.Add(item);
            foreach (var item in Pr.BridgeVersions)
                BridgeVersionBox.Items.Add(item);
            foreach (var item in Pr.DeviceTypes)
                DeviceTypeBox.Items.Add(item);
            foreach (var item in Pr.DriverVersions)
                DriverVersionBox.Items.Add(item);
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            GInfo.ContactName = ContactNameBox.Text;
            GInfo.PhoneNumber = PhoneNumberBox.Text;
            GInfo.EmailAddress = EmailAddressBox.Text;
            GInfo.CaseNumber = CaseNumberBox.Text;
            GInfo.IssueSummary = IssueSummaryBox.Text;
            GInfo.IssueDetails = IssueDetailsBox.Text;
            GInfo.TroubleShootingSteps = TroubleshootStepsBox.Text;
            GInfo.ResolutionDetails = ResolutionBox.Text;

            Lw.AddLogEntry(GInfo.ToString());

            ClipboardService.SetText(GInfo.ToString());

            MessageBox.Show($"width {this.Width} | height {this.Height}");

        }
    }
}
