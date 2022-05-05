using System.Text;
using TGC.WIQLQueryBuilder.Models;

namespace TGC.WIQLQueryBuilder;
public class QueryBuilder
{
    private List<string> _selectFields = new List<string>();
    private string _orderByFields;
    private List<WiqlWhereClause> _wiqlWhereClause = new List<WiqlWhereClause>();

    public static QueryBuilder BuildWiqlQuery()
    {
        return new QueryBuilder();
    }

    public QueryBuilder Select(string selectField)
    {
        _selectFields.Add($"[{selectField}]");
        return this;
    }

    public QueryBuilder Select(string [] selectFields)
    {
        foreach(var field in selectFields)
        {
            Select(field);
        }
        return this;
    }

    public QueryBuilder Where(string rawClause, WiqlComparison comparison, object targetValue)
    {
        _wiqlWhereClause.Add(new WiqlWhereClause(rawClause, comparison, targetValue));
        return this;
    }

    public QueryBuilder Raw(string rawClause)
    {
        _wiqlWhereClause.Add(new WiqlWhereClause(rawClause));
        return this;
    }

    public QueryBuilder And(string rawClause, WiqlComparison comparison, object targetValue)
    {
        CheckQuery();
        _wiqlWhereClause.Add(new WiqlWhereClause(rawClause, comparison, targetValue).And());
        return this;        
    }

    public QueryBuilder Or(string rawClause, WiqlComparison comparison, object targetValue)
    {
        CheckQuery();
        _wiqlWhereClause.Add(new WiqlWhereClause(rawClause, comparison, targetValue).Or());
        return this;
    }

    public QueryBuilder Sub(WiqlWhereClause wiqlWhereClause)
    {
        throw new NotImplementedException();
    }

    public QueryBuilder OrderBy(string field, object targetValue)
    {
        throw new NotImplementedException();
    }

    private void CheckQuery()
    {
        if (!_wiqlWhereClause.Any())
        {
            throw new Exception("No first WHERE clause has been added to query builder.");
        }
    }

    public WiqlPostBody BuildQuery()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("SELECT ");
        stringBuilder.Append(String.Join(",", _selectFields.ToArray()));
        stringBuilder.Append(" FROM WorkItems ");
        stringBuilder.Append("WHERE ");

        foreach(var whereClause in _wiqlWhereClause)
        {
            stringBuilder.Append(whereClause.buildWhereClauseQuery());
        }

        return new WiqlPostBody(stringBuilder.ToString());
    }
}