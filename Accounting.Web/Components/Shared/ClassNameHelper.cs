namespace Accounting.Web.Components.Shared
{
    public static class ClassNameHelper
    {
        public static string Merge(params IEnumerable<string> classNames)
        {
            return string.Join(" ", classNames);
        }
    }
}
