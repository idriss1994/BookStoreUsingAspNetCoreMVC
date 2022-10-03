﻿using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOption);
    }
}