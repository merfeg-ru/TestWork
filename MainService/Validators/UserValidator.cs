using MainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainService.Validators
{
    internal static class UserValidator
    {
        private static string INIT_ERROR_MESSAGE = "Поле ";
        private static string NAME_ERROR_MESSAGE = " имя ";
        private static string LASTNAME_ERROR_MESSAGE = " фамилия ";
        private static string PHONE_ERROR_MESSAGE = " телефон ";
        private static string EMAIL_ERROR_MESSAGE = " EMail ";
        private static string END_ERROR_MESSAGE = " не заполнено.";

        internal static (bool IsValid, string ErrorMessage) IsValid(this User user)
        {
            string errorMessage = INIT_ERROR_MESSAGE;
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                errorMessage += NAME_ERROR_MESSAGE; 
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                errorMessage += LASTNAME_ERROR_MESSAGE;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                errorMessage += PHONE_ERROR_MESSAGE;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(user.EMail))
            {
                errorMessage += EMAIL_ERROR_MESSAGE;
                isValid = false;
            }

            errorMessage += END_ERROR_MESSAGE;

            return (isValid, errorMessage);
        }
    }
}
