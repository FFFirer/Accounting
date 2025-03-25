namespace Accounting.Asset;

public enum AssetFlowDirection
{
    /// <summary>
    /// 收入
    /// </summary>
    Revenue = 1,

    /// <summary>
    /// 不计收支
    /// </summary>
    Ignore = 0,

    /// <summary>
    /// 支出
    /// </summary>
    Expenditure = -1
}
