using MahApps.Metro.Controls;
using System.Windows;
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
        LogWriter lw;
        public MainWindow()
        {
            InitializeComponent();
            GInfo = new GenericInfo();
            lw = new LogWriter();
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            GInfo.ContactName = ContactNameBox.Text;
            GInfo.PhoneNumber = PhoneNumberBox.Text;
            GInfo.EmailAddress = EmailAddressBox.Text;
            GInfo.CaseNumber = CaseNumberBox.Text;
            GInfo.Problem = ProblemBox.Text;
            GInfo.ProblemDetails = ProblemDetailsBox.Text;
            GInfo.TroubleShootingSteps = TroubleshootStepsBox.Text;
            GInfo.ResolutionDetails = ResolutionBox.Text;

            lw.AddLogEntry(GInfo.ToString());

            ClipboardService.SetText(GInfo.ToString());

        }
    }
}
