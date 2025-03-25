using UtfUnknown;

namespace Accounting.Imports;

public static class ImportHelper
{
    public static async Task<DetectionResult> DetectEncodingAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var bytes = new byte[1000];

        await stream.ReadExactlyAsync(bytes, 0, 1000);

        var result = CharsetDetector.DetectFromBytes(bytes);

        return result;
    }
}