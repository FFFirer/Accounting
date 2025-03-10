using System.Text.RegularExpressions;

using Yarp.ReverseProxy.Transforms;

namespace DevProxy
{
    public static class CustomRequestTransforms
    {
        static readonly Regex _regex = new Regex(@"(^.*?/frontend/index).*?\.(js|css)", RegexOptions.Compiled);
        public static ValueTask RemoveFileHash(RequestTransformContext context)
        {
            var formatted = _regex.Replace(context.Path, "$1.$2");

            context.ProxyRequest.RequestUri = new Uri(context.DestinationPrefix + formatted);

            return default;
        }
    }
}
