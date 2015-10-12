using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimplHelpers
{
    public static class UrlHelper
    {
        public static string UrlSeparatorChar = "/";
        /// <summary>
        /// Combines the specified virtual paths. Like of <see cref="System.IO.Path.Combine"/>.
        /// </summary>
        /// <param name="virtualPaths">The virtual paths.<example>string[] {"path1","path2","path3"}</example></param>
        /// <returns> <value>path1/path2/path3</value> </returns>
        public static string Combine(params string[] virtualPaths)
        {
            if (virtualPaths.Length < 1)
                return null;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < virtualPaths.Length; i++)
            {
                var path = virtualPaths[i];
                if (String.IsNullOrEmpty(path))
                    continue;

                if (i > 0)
                {
                    // Not first one trim start '/'
                    path = path.TrimStart('/');
                    builder.Append("/");
                }
                if (i < virtualPaths.Length - 1)
                {
                    // Not last one trim end '/'
                    path = path.TrimEnd('/');
                }
                if (!path.Contains('/'))
                {
                    path = Uri.EscapeUriString(path);
                }
                builder.Append(path);
            }

            return builder.ToString();
        }


        /// <summary>
        /// Combines base url with queries that are set as key=>value
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="queries"></param>
        /// <returns></returns>
        public static string CombineQueryString(string baseUrl, Dictionary<string, string> queries)
        {

            string query = String.Join("&",
                                       queries.Where(x => !String.IsNullOrEmpty(x.Key) && !String.IsNullOrEmpty(x.Value))
                                           .Select(x => String.Format("{0}={1}", x.Key, x.Value)));
            if (!baseUrl.EndsWith("?"))
                baseUrl += "?";

            return baseUrl + query;
        }

        /// <summary>
        /// Adds quesry parameter to url
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddQueryParam(this string source, string key, string value)
        {
            string delim;
            if ((source == null) || !source.Contains("?"))
            {
                delim = "?";
            }
            else if (source.EndsWith("?") || source.EndsWith("&"))
            {
                delim = string.Empty;
            }
            else
            {
                delim = "&";
            }

            return source + delim + HttpUtility.UrlEncode(key)
                 + "=" + HttpUtility.UrlEncode(value);
        }


        public static Dictionary<string, string> GetQueryParamsFromUri(string sourcePath) 
        {
            if (sourcePath.LastIndexOf('?') != -1)
                sourcePath = sourcePath.Substring(sourcePath.LastIndexOf('?') + 1, sourcePath.Length - sourcePath.LastIndexOf('?') - 1);
            if (String.IsNullOrWhiteSpace(sourcePath))
                return new Dictionary<string, string> { };

            return sourcePath.Split('&')
                .Select(x => 
                {
                    var elKey = String.Empty;
                    var elValue = String.Empty;

                    if (x.Contains('='))
                    {
                        var el = x.Split('=');
                        elKey = el[0];
                        if (el.Count() > 1)
                            elValue = el[1];
                    }
                    return new { Key = elKey, Value = elValue };
                }).ToDictionary(x=>x.Key, x=>x.Value);
        }
    }
}
