﻿using System.Threading.Tasks;

namespace DotNetNote.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
