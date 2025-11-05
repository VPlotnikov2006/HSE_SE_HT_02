using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataProvider;
using App.DataSaving;
using App.UserActions;

namespace App;

/// <summary>
/// Base application class
/// </summary>
public class Application
(
    IOperationRepository _operations,
    ICategoryRepository _categories,
    IBankAccountRepository _accounts,
    BankAccountFactory _accountFactory,
    CategoryFactory _categoryFactory,
    OperationBuilder _operationBuilder,
    Dictionary<string, Serializer> _serializers,
    Dictionary<string, UserAction> _actions,
    IDataProvider _dataProvider

)
{
    public readonly IOperationRepository operations = _operations;
    public readonly ICategoryRepository categories = _categories;
    public readonly IBankAccountRepository accounts = _accounts;


    public readonly BankAccountFactory accountFactory = _accountFactory;
    public readonly CategoryFactory categoryFactory = _categoryFactory;
    public readonly OperationBuilder operationBuilder = _operationBuilder;

    public readonly Dictionary<string, Serializer> serializers = _serializers;
    public readonly Dictionary<string, UserAction> actions = _actions;

    public readonly IDataProvider dataProvider = _dataProvider;

    private readonly Group<string> actions_group = new("Choose action", _actions.Keys.Select(x => x.Split('/').ToList()));

    public void AskUserAction()
    {
        actions.GetValueOrDefault(string.Join('/', [.. dataProvider.SelectValue(actions_group).Skip(1)]))?.Invoke(this);
    }
}
