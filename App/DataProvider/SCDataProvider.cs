using Spectre.Console;

namespace App.DataProvider;

/// <summary>
/// IDataProvider, that requests data through Console, decorated by Spectre.Console
/// </summary>
public class SCDataProvider : IDataProvider
{
    /// <inheritdoc/>
    public T? GetValue<T>(
        string? prompt = null,
        GetDataOptions options = GetDataOptions.Default
    ) where T : IParsable<T>
    {
        prompt ??= $"Enter value of type [yellow]{typeof(T).Name}[/]";

        switch (options)
        {
            case GetDataOptions.Repeat:
                {
                    var input = AnsiConsole.Prompt(
                        new TextPrompt<string>($"[green]{prompt}:[/]")
                            .Validate(s =>
                                T.TryParse(s, null, out _)
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error($"[red]Invalid {typeof(T).Name}. Please try again.[/]"))
                    );

                    return T.TryParse(input, null, out var value) ? value : default;
                }

            case GetDataOptions.Default:
                {
                    var input = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            $"[green]{prompt}[/] (press [grey]<Enter>[/] for default = [yellow]{default(T)}[/][/])")
                            .AllowEmpty()
                    );

                    if (string.IsNullOrWhiteSpace(input))
                        return default;

                    if (!T.TryParse(input, null, out var value))
                    {
                        AnsiConsole.MarkupLine($"[red]Invalid {typeof(T).Name}, using default value.[/]");
                        return default;
                    }

                    return value;
                }

            default:
                throw new ArgumentOutOfRangeException(nameof(options));
        }
    }

    /// <inheritdoc/>
    public T GetLimitedValue<T>(
        T? min,
        T? max,
        string? prompt = null,
        GetDataOptions options = GetDataOptions.Default
    ) where T : struct, IParsable<T>, IComparable<T>
    {
        string range = Markup.Escape($"[{min?.ToString() ?? "-∞"}; {max?.ToString() ?? "+∞"}]");
        prompt ??= $"Enter value of type [yellow]{typeof(T).Name}[/] in range {range}";

        switch (options)
        {
            case GetDataOptions.Repeat:
                {
                    var input = AnsiConsole.Prompt(
                        new TextPrompt<string>($"[green]{prompt}:[/]")
                            .Validate(s =>
                            {
                                if (!T.TryParse(s, null, out var v))
                                    return ValidationResult.Error($"[red]Invalid {typeof(T).Name} format[/]");

                                if (min is not null && v.CompareTo(min.Value) < 0)
                                    return ValidationResult.Error($"[red]Value must be ≥ [yellow]{min}[/][/]");

                                if (max is not null && v.CompareTo(max.Value) > 0)
                                    return ValidationResult.Error($"[red]Value must be ≤ [yellow]{max}[/][/]");

                                return ValidationResult.Success();
                            })
                    );

                    T.TryParse(input, null, out var value);
                    return value;
                }

            case GetDataOptions.Default:
                {
                    var input = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            $"[green]{prompt}[/] (press [grey]<Enter>[/] for default = [yellow]{default(T)}[/])")
                            .AllowEmpty()
                            .Validate(s =>
                            {
                                if (string.IsNullOrWhiteSpace(s))
                                    return ValidationResult.Success();

                                if (!T.TryParse(s, null, out var v))
                                    return ValidationResult.Error($"[red]Invalid {typeof(T).Name} format[/]");

                                if (min is not null && v.CompareTo(min.Value) < 0)
                                    return ValidationResult.Error($"[red]Value must be ≥ [yellow]{min}[/][/]");

                                if (max is not null && v.CompareTo(max.Value) > 0)
                                    return ValidationResult.Error($"[red]Value must be ≤ [yellow]{max}[/][/]");

                                return ValidationResult.Success();
                            })
                    );

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        AnsiConsole.MarkupLine($"[grey]No input provided, using default value [yellow]{default(T)}[/].[/]");
                        return default;
                    }

                    T.TryParse(input, null, out var value);
                    return value;
                }

            default:
                throw new ArgumentOutOfRangeException(nameof(options));
        }
    }

    public T SelectValue<T>(IReadOnlyCollection<T>? options, string? prompt = null) where T: notnull
    {
        if (options == null || options.Count == 0)
            throw new ArgumentException("No options provided", nameof(options));

        prompt ??= $"Select a value of type [yellow]{typeof(T).Name}[/]";

        var selectionPrompt = new SelectionPrompt<T>()
            .Title($"[green]{prompt}[/]")
            .AddChoices(options)
            .PageSize(10)
            .UseConverter(v => $"[yellow]{v?.ToString() ?? "<null>"}[/]");

        return AnsiConsole.Prompt(selectionPrompt);
    }

    public IEnumerable<T> SelectValue<T>(Group<T> options) where T: notnull
    {
        List<T> result = [options.value];
        var currentGroup = options;
        int depth = 0;

        while (true)
        {
            if (currentGroup.IsLeaf())
                break;
            var available = currentGroup.subgroups?.Select(g => g.value).ToList();

            var selected = SelectValue(available, currentGroup.value.ToString());
            result.Add(selected);

            currentGroup = currentGroup.subgroups!.First(g => EqualityComparer<T>.Default.Equals(g.value, selected));

            if (currentGroup.IsLeaf())
                break;

            depth++;
        }

        return result;
    }
}