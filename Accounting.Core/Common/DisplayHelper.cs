using System;

namespace Accounting.Common;

public static class DisplayHelper
{
    public static string PrettySizeDisplay(long size)
    {
        // 转换文件大小的展示形式，单位字节数
        if (size < 1024)
            return size + " B";
        else if (size < 1024 * 1024)
            return (size / 1024.0).ToString("F2") + " KB";
        else if (size < 1024 * 1024 * 1024)
            return (size / (1024.0 * 1024)).ToString("F2") + " MB";
        else
            return (size / (1024.0 * 1024 * 1024)).ToString("F2") + " GB";
    }
}
