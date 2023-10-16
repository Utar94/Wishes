namespace Logitar.Wishes;

internal class DatabaseProviderNotSupportedException : NotSupportedException
{
  public DatabaseProvider DatabaseProvider
  {
    get => Enum.Parse<DatabaseProvider>((string)Data[nameof(DatabaseProvider)]!);
    private set => Data[nameof(DatabaseProvider)] = value.ToString();
  }

  public DatabaseProviderNotSupportedException(DatabaseProvider databaseProvider)
    : base($"The database provider '{databaseProvider}' is not supported.")
  {
    DatabaseProvider = databaseProvider;
  }
}
