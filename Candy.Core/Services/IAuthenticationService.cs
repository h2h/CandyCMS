using Candy.Core.Domain;

namespace Candy.Core.Services
{
    public partial interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);
        void SignOut();
        User GetAuthenticatedUser();
    }
}
