using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateUserAcount(SignUpUserModel signUpUserModel)
        {
            return await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = signUpUserModel.FirstName,
                LastName = signUpUserModel.LastName,
                DateOfBirth = signUpUserModel.DateOfBirth,
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email,
            }, signUpUserModel.Password);
        }

    }
}
