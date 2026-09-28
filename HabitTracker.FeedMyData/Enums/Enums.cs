using System.ComponentModel.DataAnnotations;

namespace HabitTracker;

internal class Enums
{
  internal enum MenuChoice
  {
    [Display(Name = "View entries")]
    viewItems,

    [Display(Name = "[green]Add[/] entry")]
    addItem,

    [Display(Name = "[blue]Edit[/] entry")]
    editItem,

    [Display(Name = "[red]Delete[/] entries")]
    removeItem,

    [Display(Name = "Exit")]
    ExitApp
  }

  internal enum Column
  {
    [Display(Name = "ID")]
    id,

    [Display(Name = "Habit")]
    category,

    [Display(Name = "Date")]
    date,

    [Display(Name = "Measurement")]
    quantity,
  }
}

