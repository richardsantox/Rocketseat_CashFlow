using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpenseWriteOnlyRespository
{
    Task AddAsync(Expense expense);
    /// <summary>
    /// This function returns TRUE if the deletion was successful otherwise returns FALSE
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
