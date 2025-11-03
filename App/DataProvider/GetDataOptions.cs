namespace App.DataProvider;

/// <summary>
/// Styles how to request data from user
/// </summary>
public enum GetDataOptions
{
    Repeat, // on error - repeat
    Default // on error - return default for this type
}