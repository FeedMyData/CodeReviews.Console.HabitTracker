namespace HabitTracker;

class Habit
{
  public int Id { get; private set; }
  public string Category { get; private set; }
  public string Date { get; private set; }
  public int Quantity { get; private set; }

  public Habit(int id, string category, string date, int quantity)
  {
    Id = id;
    Category = category;
    Date = date;
    Quantity = quantity;
  }

  public void EditDate(string date)
  {
    Date = date;
  }

  public void EditQuantity(int quantity)
  {
    Quantity = quantity;
  }

  public void EditCategory(string category)
  {
    Category = category;
  }

  public string DisplayData()
  {
    return $"{Id}\t\t{Category}\t\t\t{Date}\t{Quantity}";
  }
}

