using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpenseReadOnlyRespository _repository;
        private readonly IMapper _mapper;

        public GetAllExpensesUseCase(IExpenseReadOnlyRespository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseGetAllExpensesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseGetAllExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }
    }
}