using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterExpensesBuilder
    {
        public static RequestRegisterExpensesJson Build()
        {
            return new Faker<RequestRegisterExpensesJson>()
                .RuleFor(r => r.Title, f => f.Commerce.ProductName())
                .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
                .RuleFor(r => r.Date, f => f.Date.Past())
                .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>())
                .RuleFor(r => r.Amount, faker => faker.Random.Decimal(1, 1000));
        }
    }
}
