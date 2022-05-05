using System.Text;

namespace TGC.WIQLQueryBuilder.Models;

public class WiqlWhereClause
{
    private string _field;
    private WiqlComparison _comparison;
    private object _targetValue;
    private string _queryString;
    private bool _isAnd;
    private bool _isOr;

    public WiqlWhereClause(string field, WiqlComparison comparison, object targetValue)
    {
        _field = field;
        _comparison = comparison;
        _targetValue = targetValue;
    }

    public WiqlWhereClause(string rawClause)
    {
        _queryString = rawClause;
    }

    public WiqlWhereClause And()
    {
        _isAnd = true;
        _isOr = false;
        return this;
    }

    public WiqlWhereClause Or()
    {
        _isAnd = false;
        _isOr = true;
        return this;
    }

    public void Sub(WiqlWhereClause wiqlWhereClause)
    {
        throw new NotImplementedException();
    }

    public string buildWhereClauseQuery()
    {
        if (_queryString != null)
        {
            return _queryString;
        }

        var stringBuilder = new StringBuilder();
        if (_isAnd || _isOr)
        {
            if (_isAnd)
            {
                stringBuilder.Append("AND ");
            }
            else
            {
                stringBuilder.Append("OR ");
            }

        }
        stringBuilder.Append($"[{_field}] {_comparison.ToString()} {_targetValue} ");
        return stringBuilder.ToString();
    }
}