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
            result += ContactName != null ? $"Contact Name:  {ContactName}\n" : null;
            result += PhoneNumber != null ? $"Phone Number:  {PhoneNumber}\n" : null;
            result += EmailAddress != null ? $"Email:  {EmailAddress}\n" : null;
            result += CaseNumber != null ? $"Case #:  {CaseNumber}\n\n" : null;
            result += IssueSummary != null ? $"Problem Description:\n{IssueSummary}\n\n" : null;
            result += IssueDetails != null ? $"Problem Details:\n{IssueDetails}\n\n" : null;
            result += TroubleShootingSteps != null ? $"Troubleshooting Steps:\n{TroubleShootingSteps}\n\n" : null;
            result += ResolutionDetails != null ? $"Resolution Details:\n{ResolutionDetails}\n\n" : null;

            return result;
        }
    }
}
