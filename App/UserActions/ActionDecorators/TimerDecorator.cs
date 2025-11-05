using System.Diagnostics;

namespace App.UserActions.ActionDecorators;

public class TimerDecorator: ActionDecorator
{
    private const string Path = @"../../../../.log";

    public override void Invoke(Application application)
    {
        var stopwatch = Stopwatch.StartNew();

        Action?.Invoke(application);

        stopwatch.Stop();
        var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;

        File.AppendAllText(
            Path,
            $"{DateTimeOffset.Now:yyyy/MM/dd HH:mm:ss.fffzzz} | Executed action; time taken: {elapsedMs:F2} ms\n"
        );    
    }
}
