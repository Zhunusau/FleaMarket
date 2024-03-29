﻿using System;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages
{
    public class VerificationMessage : Message
    {
        public VerificationMessage(string toEmailAddress, string title, string subject) 
            : base(toEmailAddress, title, subject) { }

        public override string GetBody(IMessageContext context)
        {
            var verificationMessageContext = context as VerificationMessageContext;
            if (verificationMessageContext == null)
            {
                throw new NullReferenceException(nameof(verificationMessageContext));
            }
            return "<br/><br/>We are excited to tell you that your FleaMarket account is" +
                   " successfully created. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + verificationMessageContext.VerificationLink + "'>" + verificationMessageContext.VerificationLink + "</a> ";
        }
    }
}
