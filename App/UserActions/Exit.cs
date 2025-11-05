namespace App.UserActions;

/// <summary>
/// Exit action
/// </summary>
public class Exit : UserAction
{
    /// <inheritdoc/>
    public override void Invoke(Application application)
    {
        Environment.Exit(0);
    }
}
