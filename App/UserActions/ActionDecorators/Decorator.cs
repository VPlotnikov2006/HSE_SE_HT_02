namespace App.UserActions.ActionDecorators;

public class ActionDecorator: UserAction
{
    public UserAction? Action { get;  set; }

    public override void Invoke(Application application)
    {
        Action?.Invoke(application);
    } 
}
