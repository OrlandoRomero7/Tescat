﻿using Tescat.Models;

namespace Tescat.Services.Emails
{
    public interface IEmailService
    {
        public Task<Email> GetEmails(int userId);
    }
}