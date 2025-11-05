namespace App.UserActions.ActionDecorators;

/// <summary>
/// Base decorator for <see cref="UserAction"/>
/// </summary>
public class ActionDecorator : UserAction
{
    public UserAction? Action { get; set; }

    public override void Invoke(Application application)
    {
        Action?.Invoke(application);
    }
}
