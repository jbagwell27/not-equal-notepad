namespace TemplateUI.Logic
{
    class GenericInfo
    {
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CaseNumber { get; set; }
        public string IssueSummary { get; set; }
        public string IssueDetails { get; set; }
        public string TroubleShootingSteps { get; set; }
        public string ResolutionDetails { get; set; }

        public GenericInfo()
        {
            Clear();
        }
        public void Clear()
        {
            ContactName = null;
            PhoneNumber = null;
            EmailAddress = null;
            CaseNumber = null;
            IssueSummary = null;
            IssueDetails = null;
            TroubleShootingSteps = null;
            ResolutionDetails = null;
        }

        public override string ToString()
        {
            string result = "";
            result += !string.IsNullOrEmpty(ContactName) ? $"Contact Name:  {ContactName}\n" : null;
            result += !string.IsNullOrEmpty(PhoneNumber) ? $"Phone Number:  {PhoneNumber}\n" : null;
            result += !string.IsNullOrEmpty(EmailAddress) ? $"Email:  {EmailAddress}\n" : null;
            result += !string.IsNullOrEmpty(CaseNumber) ? $"Case #:  {CaseNumber}\n\n" : null;
            result += !string.IsNullOrEmpty(IssueSummary) ? $"Issue Summary:  {IssueSummary}\n\n" : null;
            result += !string.IsNullOrEmpty(IssueDetails) ? $"Issue Details:\n{IssueDetails}\n\n" : null;

            if (!string.IsNullOrEmpty(TroubleShootingSteps))
            {
                string[] tempSteps = TroubleShootingSteps.Split('\n');
                TroubleShootingSteps = "";
                foreach (string s in tempSteps)
                {
                    TroubleShootingSteps += $"\u2022 {s}\n";
                }

            }
            result += !string.IsNullOrEmpty(TroubleShootingSteps) ? $"Troubleshooting Steps:\n{TroubleShootingSteps}\n" : null;
            result += !string.IsNullOrEmpty(ResolutionDetails) ? $"Resolution Details:\n{ResolutionDetails}\n" : null;

            return result;
        }
    }
}
