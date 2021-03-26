using System;

namespace Food.Core.Utilities
{
    public static class FoodUtilities
    {
        public static string FormatVideoUrl(string url)
        {
            url = url.Replace("<iframe width=\"560\" height=\"315\" src=\"", "");
            url = url.Replace("\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>", "");
            url = url.Replace("watch?v=", "embed/");

            if (url.Contains("&ab_channel"))
            {
                var index = url.IndexOf("&ab_channel", StringComparison.Ordinal);
                url = url.Remove(index);
            }
            return url;
        }


    }
}
