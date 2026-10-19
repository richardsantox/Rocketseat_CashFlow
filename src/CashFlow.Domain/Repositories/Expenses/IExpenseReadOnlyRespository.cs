using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseReadOnlyRespository
{
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
