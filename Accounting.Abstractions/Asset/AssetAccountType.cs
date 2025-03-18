using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Asset;

public enum AssetAccountType
{
    [Description("现金，储蓄卡，网络支付等")]
    [Display(Name = "资金")]
    Fund,

    [Description("花呗，白条，信用卡等")]
    [Display(Name = "信用")]
    Credit,

    [Description("交通卡，饭卡，会员卡等")]
    [Display(Name = "充值")]
    Recharge,

    [Description("基金，股票等")]
    [Display(Name = "投资")]
    Investment,

    [Description("借入，借出等")]
    [Display(Name = "债务")]
    Debt
}
