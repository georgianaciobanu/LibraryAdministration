﻿using System.ComponentModel.DataAnnotations;

namespace SimbioMed.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}