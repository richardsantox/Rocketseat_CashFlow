using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseWriteOnlyRespository
{
    Task AddAsync(Expense expense);
}
