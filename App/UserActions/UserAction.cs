namespace App.UserActions;

/// <summary>
/// Base class for all user actions
/// </summary>
public abstract class UserAction()
{
    /// <summary>
    /// Invoke action method
    /// </summary>
    /// <param name="application">Application, storing all data</param>
    public abstract void Invoke(Application application);
}
