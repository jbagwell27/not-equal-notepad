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
            if (ContactName != null)
                result += $"Contact Name:  {ContactName}\n";
            if (PhoneNumber != null)
                result += $"Phone Number:  {PhoneNumber}\n";
            if (EmailAddress != null)
                result += $"Email:  {EmailAddress}\n";
            if (CaseNumber != null)
                result += $"Case #:  {CaseNumber}\n\n";
            if (IssueSummary != null)
                result += $"Problem Description:\n{IssueSummary}\n\n";
            if (IssueDetails != null)
                result += $"Problem Details:\n{IssueDetails}\n\n";
            if (TroubleShootingSteps != null)
                result += $"Troubleshooting Steps:\n{TroubleShootingSteps}\n\n";
            if (ResolutionDetails != null)
                result += $"Resolution Details:\n{ResolutionDetails}\n\n";

            return result;
        }
    }
}
