using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using static HabitTracker.Enums;

namespace HabitTracker;

internal class HabitRepository
{
  static string connectionString = @"Data Source=HabitTracker.db";

  internal static void CreateDB()
  {
    bool tableExisted;

    using (var connection = new SqliteConnection(connectionString))
    {
      connection.Open();

      using var existsCommand = connection.CreateCommand();
      existsCommand.CommandText =
        $@"SELECT EXISTS (
        SELECT name
        FROM sqlite_master
        WHERE type = 'table'
        AND name = 'habit_logger')";

      tableExisted = Convert.ToBoolean(existsCommand.ExecuteScalar());

      var createCommand = connection.CreateCommand();
      createCommand.CommandText =
        $@"CREATE TABLE IF NOT EXISTS habit_logger(
        {Column.id} INTEGER PRIMARY KEY AUTOINCREMENT,
        {Column.category} TEXT,
        {Column.date} TEXT,
        {Column.quantity} INTEGER)";

      createCommand.ExecuteNonQuery();
    }

    if (!tableExisted)
      DebugTool.SeedData();
  }

  internal static void Add(string category, string date, int quantity)
  {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    var command = connection.CreateCommand();

    command.CommandText =
        @$"INSERT INTO habit_logger ({Column.category}, {Column.date}, {Column.quantity})
          VALUES('{category}','{date}',{quantity})";

    command.ExecuteNonQuery();
  }

  internal static void Remove(Habit entry)
  {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText =
      @$"DELETE FROM habit_logger 
          WHERE {Column.id} = {entry.Id}";

    command.ExecuteNonQuery();
  }

  internal static void EditRow(Habit entry)
  {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText =
      $@"UPDATE habit_logger
        SET {Column.category} = '{entry.Category}', 
            {Column.date} = '{entry.Date}', 
            {Column.quantity} = {entry.Quantity}
        WHERE {Column.id} = {entry.Id}";

    command.ExecuteNonQuery();
  }

  internal static List<Habit> GetDataOrderBy(string column)
  {
    List<Habit> tableData = new();

    using (var connection = new SqliteConnection(connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
        $@"SELECT * FROM habit_logger
      ORDER BY {column}";

      SqliteDataReader reader = command.ExecuteReader();

      if (reader.HasRows)
      {
        while (reader.Read())
        {
          Habit entry = new Habit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
          tableData.Add(entry);
        }
      }
      return tableData;
    }
  }

  internal static List<Habit> GetDataFilterCategory(string filter)
  {
    List<Habit> tableData = new();

    using (var connection = new SqliteConnection(connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
        $@"SELECT * FROM habit_logger
        WHERE {Column.category} = '{filter}'";

      SqliteDataReader reader = command.ExecuteReader();

      if (reader.HasRows)
      {
        while (reader.Read())
        {
          Habit entry = new Habit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
          tableData.Add(entry);
        }
      }
      return tableData;
    }
  }

  internal static List<Habit> GetAllData()
  {
    List<Habit> tableData = new();

    using (var connection = new SqliteConnection(connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
        @"SELECT * FROM habit_logger";

      SqliteDataReader reader = command.ExecuteReader();

      if (reader.HasRows)
      {
        while (reader.Read())
        {
          Habit entry = new Habit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
          tableData.Add(entry);
        }
      }
      return tableData;
    }
  }

  internal static List<string> QueryUniqueRowEntries(string columnName)
  {
    List<Habit> tableData = new();
    List<string> uniqueRowEntries = new();

    using var connection = new SqliteConnection(connectionString);
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
        $@"SELECT {columnName} 
        FROM habit_logger
        GROUP BY {columnName}";

      using SqliteDataReader reader = command.ExecuteReader();
      while (reader.Read())
        uniqueRowEntries.Add(reader.GetString(0));
    }

    return uniqueRowEntries;
  }

  internal static int TotalRows()
  {
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText =
      $@"SELECT COUNT(*)
        FROM habit_logger";

    return Convert.ToInt32(command.ExecuteScalar());
  }
}