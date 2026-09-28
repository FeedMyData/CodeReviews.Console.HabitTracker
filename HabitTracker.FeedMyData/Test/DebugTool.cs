namespace HabitTracker;

internal class DebugTool
{
  internal static void SeedData()
  {
    string[] categories = ["squats", "burpees", "pull-ups", "push-ups", "hands washing", "getting rickrolled"];

    for (int i = 0; i < 100; i++)
    {
      Random rand = new();
      string date = RandomDateTime();
      string category = categories[rand.Next(0, categories.Length)];
      int quantity = rand.Next(0, 100);

      HabitRepository.Add(category, date, quantity);
    }
  }

  private static string RandomDateTime()
  {
    Random rand = new();

    DateTime startDay = new DateTime(2026, 8, 1);
    int maxRange = (DateTime.Today - startDay).Days + 1;
    string date = startDay.AddDays(rand.Next(0, maxRange)).ToString("d");

    return date;
  }
}