using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace LAP1
{
    public class Validation
    {
        public static bool IsFieldNull(string fieldText)
        {
            if (!string.IsNullOrEmpty(fieldText))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEmailValid(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {

                var phoneNum = new PhoneAttribute();
                if (phoneNum.IsValid(phoneNumber))
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }
    }
}
