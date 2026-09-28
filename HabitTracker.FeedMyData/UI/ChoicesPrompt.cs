using Microsoft.Data.Sqlite;
using Spectre.Console;
using static HabitTracker.Enums;

namespace HabitTracker;

// If you already have a bit of experience with programming, we highly recommend you get into the habit of writing unit tests for a few methods in your project. Any method that outputs data and doesn't talk to a database (those are tested in integration tests) can be unit tested. A good example is any method that deals with validation. Here's a quick tutorial.
// If you haven't, try using parameterized queries to make your application more secure.
// choose measurements?

// order when previewing
// why would it clear when asking for the date?

class ChoicesPrompt
{
  public static List<Habit> SelectAllOrCategory(string action, string colorHighlight)
  {
    List<Habit> tableData = new();

    var selection = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .Title($"What do you want to [BOLD {colorHighlight}]{action}[/]")
      .HighlightStyle(colorHighlight)
      .Mode(SelectionMode.Leaf)
      .AddChoiceGroup(action + " from all", new[] { action })
      .AddChoiceGroup($"{action} from specific habit", HabitRepository.QueryUniqueRowEntries($"{Column.category}")));

    if (selection == action)
      tableData = HabitRepository.GetAllData();
    else
      tableData = HabitRepository.GetDataFilterCategory(selection);

    return tableData;
  }

  public static Habit SelectEntry(List<Habit> tableData, string action, string colorHighlight)
  {
    var selection = AnsiConsole.Prompt(
    new SelectionPrompt<Habit>()
    .Title($"[BOLD {colorHighlight}]Select[/] the entry you want to [BOLD {colorHighlight}]{action}[/]:")
    .HighlightStyle(colorHighlight)
    .UseConverter(entry => entry.DisplayData())
    .AddChoices(tableData));

    return selection;
  }

  public static List<string> SelectMultiString(string action, string colorHighlight, string[] choices)
  {
    var selection = AnsiConsole.Prompt(
      new MultiSelectionPrompt<string>()
      .Title($"What do you want to [BOLD {colorHighlight}]{action}[/]")
      .HighlightStyle(colorHighlight)
      .InstructionsText($"[grey](Press [{colorHighlight}]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
      .AddChoices(choices));

    return selection;
  }

  public static List<Habit> SelectMultiEntries(List<Habit> tableData, string action, string colorHighlight)
  {
    var selection = AnsiConsole.Prompt(
    new MultiSelectionPrompt<Habit>()
    .Title($"[BOLD {colorHighlight}]Select[/] the entry you want to [BOLD {colorHighlight}]{action}[/]:")
    .HighlightStyle(colorHighlight)
    .InstructionsText($"[grey](Press [{colorHighlight}]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]")
    .UseConverter(entry => entry.DisplayData())
    .AddChoices(tableData));

    return selection;
  }

  public static string SelectCategory(List<string> categories, string colorHighlight)
  {
    var selection = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title($"[BOLD {colorHighlight}]Select[/] the type of {Column.category}:")
    .HighlightStyle(colorHighlight)
    .AddChoices(categories));

    return selection;
  }
}





