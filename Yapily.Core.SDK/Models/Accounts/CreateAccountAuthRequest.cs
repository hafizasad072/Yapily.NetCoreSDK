namespace Yapily.Core.SDK.Models.Accounts
{
    public class CreateAccountAuthRequest
    {
        public string UserUuid { get; set; }

        public string InstitutionId { get; set; }

        public string? ApplicationUserId { get; set; }

        public string Callback { get; set; }

        public bool OneTimeToken { get; set; } = false;
    }
}
