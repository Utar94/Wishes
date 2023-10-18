using Logitar.Data;
using Logitar.Data.SqlServer;
using Logitar.Wishes.EntityFrameworkCore.Relational;

namespace Logitar.Wishes.EntityFrameworkCore.SqlServer;

internal class SqlServerHelper : SqlHelper, ISqlHelper
{
  public IDeleteBuilder DeleteFrom(TableId table) => SqlServerDeleteBuilder.From(table);
  public IQueryBuilder QueryFrom(TableId table) => SqlServerQueryBuilder.From(table);
}
