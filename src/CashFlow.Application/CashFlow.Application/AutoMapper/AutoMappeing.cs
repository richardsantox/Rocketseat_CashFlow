using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMappeing : Profile
    {
        public AutoMappeing()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestRegisterExpensesJson, Expense>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpenseJson>();
        }
    }
}
