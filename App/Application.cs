using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataProvider;
using App.DataSaving;
using App.Notification;
using App.UserActions;

namespace App;

public class Application
(
    IOperationRepository _operations,
    ICategoryRepository _categories,
    IBankAccountRepository _accounts,
    BankAccountFactory _accountFactory,
    CategoryFactory _categoryFactory,
    OperationBuilder _operationBuilder,
    Dictionary<string, Serializer> _serializers,
    Dictionary<IEnumerable<string>, UserAction> _actions,
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
    public readonly Dictionary<IEnumerable<string>, UserAction> actions = _actions;

    public readonly IDataProvider dataProvider = _dataProvider;

    public void AskUserAction()
    {
        actions.GetValueOrDefault(dataProvider.SelectValue(new Group<string>("Choose action", actions.Keys)))?.Invoke();
    }
}
