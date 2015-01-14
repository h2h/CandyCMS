using System;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;

using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Services
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        protected virtual string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }
        protected virtual string EncodePassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA256");

            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(dst);
            return BitConverter.ToString(dst).Replace("-", "");
        }

        public void Create(User model)
        {
            this._userRepository.Insert(model);
        }
        public User GetByUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return this._userRepository.Table.Where(u => u.UserName.Equals(username)).FirstOrDefault();
        }
        public User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return this._userRepository.Table.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
        public void Register(RegisterUserModel model)
        {
            var user = new User();
            user.PasswordSalt = GenerateSalt();
            user.Password = EncodePassword(model.Password, user.PasswordSalt);
            user.Email = model.Email;
            user.NiceName = model.UserName;
            user.UserName = model.UserName;

            Create(user);
        }
        public bool Validate(LoginUserModel model)
        {
            var user = new User();

            if (model.UserName.Contains("@"))
                user = GetByEmail(model.UserName);
            else
                user = GetByUserName(model.UserName);

            if (user == null)
                return false;

            if (user.Password.Equals(EncodePassword(model.Password, user.PasswordSalt), StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
