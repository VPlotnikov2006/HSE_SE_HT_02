namespace App.UserActions;

public class Exit : UserAction
{
    public override void Invoke(Application application)
    {
        Environment.Exit(0);
    }
}
