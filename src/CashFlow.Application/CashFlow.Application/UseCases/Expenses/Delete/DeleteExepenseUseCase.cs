using System;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExepenseUseCase : IDeleteExepenseUseCase
{
    private readonly IExpenseWriteOnlyRespository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExepenseUseCase(
        IExpenseWriteOnlyRespository repository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result == false)
            throw new NotFoundException(ResourceErrorMessages.ExpenseNotFound);

        await _unitOfWork.Commit();
    }
}
