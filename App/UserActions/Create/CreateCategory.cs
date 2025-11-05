using App.Core;
using App.Core.Context;
using App.DataProvider;

namespace App.UserActions.Create;

/// <summary>
/// Create category action
/// </summary>
public class CreateCategory : UserAction
{
    /// <inheritdoc/>
    public override void Invoke(Application app)
    {

        string name = app.dataProvider.GetValue<string>("Enter category name", GetDataOptions.Repeat)!;

        OperationType type = app.dataProvider.SelectValue(
            Enum.GetValues<OperationType>().ToList(),
            "Choose category type"
        );

        var ctx = new CategoryContext
        (
            Name: name,
            Type: type
        );

        var category = app.categoryFactory.Create(ctx);
    }
}