using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using static HabitTracker.Enums;

namespace HabitTracker;

class Program
{
  static void Main(string[] args)
  {
    HabitRepository.CreateDB();
    PrintTitle();
    MainMenu();
  }

  private static void MainMenu()
  {
    bool exitApp = false;

    while (!exitApp)
    {
      var startChoice = AnsiConsole.Prompt(
        new SelectionPrompt<MenuChoice>()
        .Title("What do you want to do?")
        .HighlightStyle(Color.Grey)
        .UseConverter(choice => choice.GetDisplayName())
        .AddChoices(Enum.GetValues<MenuChoice>()));

      switch (startChoice)
      {
        case MenuChoice.addItem:
          AddItem();
          break;

        case MenuChoice.editItem:
          EditItem();
          break;

        case MenuChoice.removeItem:
          RemoveItem();
          break;

        case MenuChoice.viewItems:
          ViewItems();
          break;

        case MenuChoice.ExitApp:
          exitApp = true;
          break;
      }
    }
  }

  private static void ViewItems()
  {
    PrintTitle();

    if (HabitRepository.TotalRows() == 0)
      NoEntriesFound();

    else
    {
      string action = "View";
      string colorHighlight = "Blue";

      List<Habit> tableData = ChoicesPrompt.SelectAllOrCategory(action, colorHighlight);

      var table = new Table()
          .RoundedBorder()
          .Title("[bold]Habits Logged[/]");

      foreach (string column in Enum.GetNames<Column>())
        table.AddColumn(column);

      foreach (var entry in tableData)
        table.AddRow($"{entry.Id}", $"{entry.Category}", $"{entry.Date}", $"{entry.Quantity}");

      AnsiConsole.Write(table);
      AnsiConsole.WriteLine();
    }
  }

  private static void AddItem()
  {
    string category;
    string newCategory = $"[SeaGreen1]_Add a new {Column.category}[/]";

    string colorHighlight = "Green";

    List<string> categories = HabitRepository.QueryUniqueRowEntries($"{Column.category}");
    categories.Add(newCategory);
    categories.Sort();

    var selection = ChoicesPrompt.SelectCategory(categories, colorHighlight);

    if (selection == newCategory)
      category = AnsiConsole.Ask<string>($"Enter a new {Column.category} type:");

    else
      category = selection;

    AnsiConsole.MarkupLine($"[{colorHighlight}]Adding[/]: {selection}");
    string date = AnsiConsole.Ask<DateTime>($"Enter the {Column.date} (YYYY.MM.DD):").ToString("d");
    int quantity = AnsiConsole.Ask<int>($"Enter the {Column.quantity}:");

    HabitRepository.Add(category, date, quantity);

    AnsiConsole.MarkupLine($"[{colorHighlight}]A new entry has been succesfully added.[/]\n");
  }

  private static void EditItem()
  {
    PrintTitle();

    if (HabitRepository.TotalRows() == 0)
      NoEntriesFound();

    else
    {
      string action = "Edit";
      string colorHighlight = "Blue";

      List<Habit> tableData = ChoicesPrompt.SelectAllOrCategory(action, colorHighlight);
      Habit selectedEntry = ChoicesPrompt.SelectEntry(tableData, action, colorHighlight);

      AnsiConsole.WriteLine();
      AnsiConsole.MarkupLine($"[{colorHighlight}]Curently editing id {selectedEntry.Id}:[/] {selectedEntry.Category} from {selectedEntry.Date} => {selectedEntry.Quantity}");
      AnsiConsole.WriteLine();

      string[] choices = EnumExtensions.GetColumnWithoutId().ToArray();
      List<string> selection = ChoicesPrompt.SelectMultiString(action, colorHighlight, choices);

      if (selection.Contains($"{Column.date}"))
        selectedEntry.EditDate(AnsiConsole.Ask<DateTime>($"Edit the {Column.date} (YYYY.MM.DD):").ToString("d"));

      if (selection.Contains($"{Column.quantity}"))
        selectedEntry.EditQuantity(AnsiConsole.Ask<int>($"Edit the {Column.quantity}:"));

      if (selection.Contains($"{Column.category}"))
        selectedEntry.EditCategory(AnsiConsole.Ask<string>($"Edit the {Column.category}:"));

      HabitRepository.EditRow(selectedEntry);

      AnsiConsole.MarkupLine("[green]Entry succesfully edited.[/]");

      AnsiConsole.WriteLine();
    }
  }

  private static void RemoveItem()
  {
    PrintTitle();

    if (HabitRepository.TotalRows() == 0)
      NoEntriesFound();

    else
    {
      string action = "Remove";
      string colorHighlight = "Red";

      List<Habit> tableData = ChoicesPrompt.SelectAllOrCategory(action, colorHighlight);
      List<Habit> selection = ChoicesPrompt.SelectMultiEntries(tableData, action, colorHighlight);

      if (selection.Count == 0)
        AnsiConsole.MarkupLine($"[yellow]No entry has been deleted[/]");

      else
      {
        foreach (var entry in selection)
          HabitRepository.Remove(entry);

        if (selection.Count == 1)
          AnsiConsole.MarkupLine($"[green]{selection.Count} entry has been [red]deleted[/].[/]");

        else
          AnsiConsole.MarkupLine($"[green]{selection.Count} entries have been [red]deleted[/].[/]");
      }

      AnsiConsole.WriteLine();
    }
  }

  private static void NoEntriesFound()
  {
    AnsiConsole.MarkupLine($"[red]No entries have been found.[/]");
    AnsiConsole.WriteLine();
  }

  private static void PrintTitle()
  {
    Console.Clear();
    AnsiConsole.Write(new Panel("   Habit Logger   ").DoubleBorder());
    AnsiConsole.WriteLine();
  }
}





