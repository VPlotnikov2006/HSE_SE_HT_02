using App.DataProvider;
using App.Notification;

namespace App.Core.Initialization;

public class BankAccountFactory<TNotifier>(IDataProvider provider): EntityFactory<BankAccount<TNotifier>>(provider)
 where TNotifier : INotifier
{
    public override BankAccount<TNotifier> Create()
    {
        return new(Guid.NewGuid())
        {
            Name = _provider.GetValue<string>("Enter account name", GetDataOptions.Repeat)!,
            Balance = _provider.GetLimitedValue<decimal>(0, null, "Enter initial balance (leave empty if zero)")
        };
    }
}
