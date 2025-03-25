using System.Diagnostics.CodeAnalysis;

namespace Accounting.Imports;

public record ImportChannel(string Code, string Name);

public static class ImportChannels
{
    public const string AlipayKey = "Alipay";
    public const string WeChatKey = "WeChat";

    public static ImportChannel Alipay = new("Alipay", "支付宝");
    public static ImportChannel WeChat = new("WeChat", "微信支付");

    public static IEnumerable<ImportChannel> Channels => [Alipay, WeChat];

    public static ImportChannel Parse(string name)
    {
        return name switch
        {
            AlipayKey => Alipay,
            WeChatKey => WeChat,
            _ => throw new ArgumentOutOfRangeException(name)
        };
    }

    public static bool TryParse(string name, [NotNullWhen(true)] out ImportChannel? channel)
    {
        channel = name switch
        {
            AlipayKey => Alipay,
            WeChatKey => WeChat,
            _ => (ImportChannel?)null
        };

        return channel != null;
    }
}