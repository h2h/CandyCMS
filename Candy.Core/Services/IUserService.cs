using Candy.Core.Domain;

namespace Candy.Core.Services
{
    public partial interface IUserService
    {
        void Create(User model);

        void Register(RegisterUserModel model);

        User GetByUserName(string username);

        User GetByEmail(string email);

        bool Validate(LoginUserModel model);

        IPagedList<User> GetAllUser(int pageIndex = 0, int pageSize = 2147483647);
    }
}