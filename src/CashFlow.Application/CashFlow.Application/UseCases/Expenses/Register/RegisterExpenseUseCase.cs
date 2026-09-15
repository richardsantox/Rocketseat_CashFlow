using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpensesJson request)
        {
            Validate(request);

            var entity = new Domain.Entities.Expense
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Amount = request.Amount,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType
            };

            await _repository.AddAsync(entity);
            await _unitOfWork.Commit();

            return new ResponseRegisterExpenseJson();
        }

        private void Validate(RequestRegisterExpensesJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
