using FluentValidation;
using SenderService.Models;

namespace SenderService.Validators
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(x => x.LastName)
				.NotEmpty()
				.WithMessage("Поле фамилия не заполнено");

			RuleFor(x => x.FirstName)
				.NotEmpty()
				.WithMessage("Поле имя не заполнено");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Поле телефон не заполнено");

			RuleFor(x => x.EMail)
				.EmailAddress()
				.WithMessage("Не корректный EMail");
		}
	}
}
