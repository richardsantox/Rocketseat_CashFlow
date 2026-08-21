using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace ValidatorsTests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success() 
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("    ")]
    [InlineData("")]
    public void Error_Title_Empty(string title)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesBuilder.Build();
        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.TitleRequired);
    }

    [Fact]
    public void Error_Date_Future()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.ExpensesCannotForTheFuture);
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesBuilder.Build();
        request.PaymentType = (PaymentType)999;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.PaymentTypeInvalid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesBuilder.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.AmountMustBeGreaterThanZero);
    }
}

