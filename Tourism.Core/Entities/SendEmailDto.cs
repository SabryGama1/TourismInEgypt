namespace Tourism.Core.Entities
{
    public class SendEmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
        public string ResetUrl { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
