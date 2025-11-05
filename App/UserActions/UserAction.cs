namespace App.UserActions;

public abstract class UserAction(Application application)
{
    protected readonly Application _application = application;
    public abstract void Invoke();
}
