using System.Windows;
using TemplateLogic;
using MahApps.Metro.Controls;
using TextCopy;

namespace TemplateUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        GenericInfo GInfo;
        public MainWindow()
        {
            InitializeComponent();
            GInfo = new GenericInfo();
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

            ClipboardService.SetText(GInfo.ToString());

        }
    }
}
