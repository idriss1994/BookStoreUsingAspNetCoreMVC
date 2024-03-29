﻿using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel)
        {
            var user = new ApplicationUser
            {
                FirstName = signUpUserModel.FirstName,
                LastName = signUpUserModel.LastName,
                DateOfBirth = signUpUserModel.DateOfBirth,
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email,
            };
            var result = await _userManager.CreateAsync(user, signUpUserModel.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }

            return result;
        }
        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrWhiteSpace(token))
            {
                await SendConfirmationEmail(user, token);
            }
        }
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrWhiteSpace(token))
            {
                await SendResetPasswordEmail(user, token);
            }
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            string userId = _userService.GetLoggedInUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword,
                changePasswordModel.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.ConfirmEmailAsync(user, token);
            }
            return IdentityResult.Failed(new IdentityError { Description = "Something went wrong" });
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordModel.UserId);
            
            if (user != null)
            {
                return await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError { Description = "Something went wrong" });
        }
        async Task SendConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string emailConfirmation = _configuration.GetSection("Application:EmailConfirmation").Value;
            string emailConfirmationUrl = string.Format(appDomain + emailConfirmation, user.Id, token);
            var userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}", $"{user.LastName} {user.FirstName}"),
                    new KeyValuePair<string, string>("{{link}}", emailConfirmationUrl)
                }
            };
            await _emailService.SendEmailForEmailConfirmation(userEmailOptions);
        }

        async Task SendResetPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string forgotPassword = _configuration.GetSection("Application:ForgotPassword").Value;
            string forgotPasswordUrl = string.Format(appDomain + forgotPassword, user.Id, token);
            var userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}", $"{user.LastName} {user.FirstName}"),
                    new KeyValuePair<string, string>("{{link}}", forgotPasswordUrl)
                }
            };
            await _emailService.SendEmailForForgotPassword(userEmailOptions);
        }
    }
}
