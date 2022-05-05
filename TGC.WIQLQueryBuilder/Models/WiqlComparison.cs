namespace TGC.WIQLQueryBuilder.Models;
public class WiqlComparison
{
    private string _comparisonSign;
    public WiqlComparison(string comparisonSign)
    {
        _comparisonSign = comparisonSign;
    }

    public override string ToString()
    {
        return _comparisonSign;
    }

    public static WiqlComparison Equal = new WiqlComparison("=");
    public static WiqlComparison NotEqual = new WiqlComparison("<>");
    public static WiqlComparison LargerThan = new WiqlComparison(">");
    public static WiqlComparison LessThan = new WiqlComparison("<");
}
