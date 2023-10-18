using Logitar.Data;
using Logitar.Data.PostgreSQL;
using Logitar.Wishes.EntityFrameworkCore.Relational;

namespace Logitar.Wishes.EntityFrameworkCore.PostgreSQL;

internal class PostgresHelper : SqlHelper, ISqlHelper
{
  public IDeleteBuilder DeleteFrom(TableId table) => PostgresDeleteBuilder.From(table);
  public IQueryBuilder QueryFrom(TableId table) => PostgresQueryBuilder.From(table);

  protected override ConditionalOperator CreateOperator(string pattern) => PostgresOperators.IsLikeInsensitive(pattern);
}
