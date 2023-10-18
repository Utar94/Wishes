using Logitar.Data;
using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.EntityFrameworkCore.Relational;

public interface ISqlHelper
{
  IQueryBuilder ApplyTextSearch(IQueryBuilder builder, TextSearch search, params ColumnId[] columns);
  IDeleteBuilder DeleteFrom(TableId table);
  IQueryBuilder QueryFrom(TableId table);
}
