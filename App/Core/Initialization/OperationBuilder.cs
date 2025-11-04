using App.Core.Context;
using App.Data.OperationData;

namespace App.Core.Initialization;

public class OperationBuilder(IOperationRepository repository)
{
    private readonly IOperationRepository _repository = repository;

    private Guid _bankAccountId;
    private Guid _categoryId;
    private decimal _amount;
    private DateTime _date = DateTime.Now;
    private string? _description;

    public OperationBuilder SetBankAccount(Guid bankAccountId)
    {
        _bankAccountId = bankAccountId;
        return this;
    }

    public OperationBuilder SetCategory(Guid categoryId)
    {
        _categoryId = categoryId;
        return this;
    }

    public OperationBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public OperationBuilder SetDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public OperationBuilder SetDescription(string? description)
    {
        _description = description;
        return this;
    }

    public Operation Build()
    {
        if (_bankAccountId == Guid.Empty)
            throw new InvalidOperationException("BankAccountId must be set");
        if (_categoryId == Guid.Empty)
            throw new InvalidOperationException("CategoryId must be set");
        if (_amount == 0)
            throw new InvalidOperationException("Amount must be set");

        var link = new OperationLink
        (
            BankAccountId: _bankAccountId,
            CategoryId: _categoryId
        );

        var ctx = new OperationContext
        (
            Amount: _amount,
            Date: _date,
            Description: _description
        );

        Operation operation = new(Guid.NewGuid(), link, ctx);

        _repository.Add(operation);

        return operation;
    }
}