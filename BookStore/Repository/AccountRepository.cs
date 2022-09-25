using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateUserAcount(SignUpUserModel signUpUserModel)
        {
            return await _userManager.CreateAsync(new IdentityUser
            {
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email,
            }, signUpUserModel.Password);
        }

    }
}
