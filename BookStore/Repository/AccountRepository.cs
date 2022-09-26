using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel)
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

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
