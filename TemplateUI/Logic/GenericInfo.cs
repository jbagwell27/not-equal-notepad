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
            result += !string.IsNullOrEmpty(CaseNumber ) ? $"Case #:  {CaseNumber}\n\n" : null;
            result += !string.IsNullOrEmpty(IssueSummary) ? $"Problem Description:\n{IssueSummary}\n\n" : null;
            result += !string.IsNullOrEmpty(IssueDetails) ? $"Problem Details:\n{IssueDetails}\n\n" : null;
            result += !string.IsNullOrEmpty(TroubleShootingSteps) ? $"Troubleshooting Steps:\n{TroubleShootingSteps}\n\n" : null;
            result += !string.IsNullOrEmpty(ResolutionDetails) ? $"Resolution Details:\n{ResolutionDetails}\n\n" : null;

            return result;
        }
    }
}
