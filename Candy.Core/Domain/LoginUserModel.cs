namespace Candy.Core.Domain
{
    public class LoginUserModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Remember { get; set; }

        public string Captcha { get; set; }

        public string ReturnUrl { get; set; }
    }
}