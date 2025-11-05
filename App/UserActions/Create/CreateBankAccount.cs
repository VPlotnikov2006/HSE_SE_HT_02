namespace App.UserActions.Create;

public class CreateBankAccount : UserAction
{
    public override void Invoke(Application application)
    {
        string name = application.dataProvider.GetValue<string>("Enter account name", DataProvider.GetDataOptions.Repeat)!;
        decimal initialBalance = application.dataProvider.GetLimitedValue<decimal>(
            0,
            null,
            "Enter initial balance",
            DataProvider.GetDataOptions.Default
        );
        application.accountFactory.Create(new(name, initialBalance));
    }
}
