using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;
using System.Xml;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator
        : AbstractValidator<RequestRegisterExpensesJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.TitleRequired);

            RuleFor(expense => expense.Amount)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.AmountMustBeGreaterThanZero);

            RuleFor(expense => expense.Date)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.ExpensesCannotForTheFuture);

            RuleFor(expense => expense.PaymentType)
                .IsInEnum()
                .WithMessage(ResourceErrorMessages.PaymentTypeInvalid);

        }
    }
}
