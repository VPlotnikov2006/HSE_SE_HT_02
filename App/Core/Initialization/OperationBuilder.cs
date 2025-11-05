using App.Core.Context;
using App.Data.OperationData;

namespace App.Core.Initialization;

/// <summary>
/// Operation builder
/// </summary>
/// <param name="repository">Repository to store new operation</param>
public class OperationBuilder(IOperationRepository repository)
{
    /// <summary>
    /// Repository to store new operation
    /// </summary>
    private readonly IOperationRepository _repository = repository;


    /// <summary>
    /// Reference to operation account
    /// </summary>
    private Guid _bankAccountId;

    /// <summary>
    /// Reference to operation category
    /// </summary>
    private Guid _categoryId;

    /// <summary>
    /// Operation amount
    /// </summary>
    private decimal _amount;

    /// <summary>
    /// Operation date & time
    /// </summary>
    private DateTime _date = DateTime.Now;

    /// <summary>
    /// Operation description
    /// </summary>
    private string? _description;


    /// <summary>
    /// Method to set account reference
    /// </summary>
    /// <param name="bankAccountId">Reference to operation account</param>
    /// <returns>Current builder</returns>
    public OperationBuilder SetBankAccount(Guid bankAccountId)
    {
        _bankAccountId = bankAccountId;
        return this;
    }

    /// <summary>
    /// Method to set category reference
    /// </summary>
    /// <param name="categoryId">Reference to operation category</param>
    /// <returns>Current builder</returns>
    public OperationBuilder SetCategory(Guid categoryId)
    {
        _categoryId = categoryId;
        return this;
    }

    /// <summary>
    /// Method to set operation amount
    /// </summary>
    /// <param name="amount">Operation amount</param>
    /// <returns>Current builder</returns>
    public OperationBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    /// <summary>
    /// Method to set operation date
    /// </summary>
    /// <param name="date">Operation date</param>
    /// <returns>Current builder</returns>
    public OperationBuilder SetDate(DateTime date)
    {
        _date = date;
        return this;
    }

    
    /// <summary>
    /// Method to set operation description
    /// </summary>
    /// <param name="description">Operation description</param>
    /// <returns>Current builder</returns>
    public OperationBuilder SetDescription(string? description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    /// Final builder
    /// </summary>
    /// <returns>New operation stored in <see cref="_repository"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
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