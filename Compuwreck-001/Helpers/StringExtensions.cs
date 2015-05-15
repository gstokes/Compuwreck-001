using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Compuwreck_001.Helpers {
    public static class StringExtensions {

        public static string ToNonBreakingSpaceIfEmpty(this string value) {
            return string.IsNullOrWhiteSpace(value) ? "&nbsp;" : value;
        }

        public static string ToYesNo(this string value) {
            return value == "True" ? "Yes" : "No";
        }

        public static string IrishEncoded(this string value) {
            var sb = new StringBuilder();
            sb.Append(value);
            sb.Replace("Á", "&#193;");
            sb.Replace("É", "&#201;");
            sb.Replace("Í", "&#205;");
            sb.Replace("Ó", "&#211;");
            sb.Replace("Ú", "&#218;");
            sb.Replace("á", "&#225;");
            sb.Replace("é", "&#233;");
            sb.Replace("í", "&#237;");
            sb.Replace("ó", "&#243;");
            sb.Replace("ú", "&#250;");
            sb.Replace("€", "&euro;");
            sb.Replace(" & ", " &amp; ");
            return sb.ToString();
        }

        //public static string HtmlStripped(this string value) {
        //    if (string.IsNullOrEmpty(value.Trim())) {
        //        return value.Trim();
        //    }

        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(value);

        //    return doc.DocumentNode.InnerText;
        //}


        public static string Linkify(this string value) {
            value = " " + value + " ";
            // easier to convert BR's to something more neutral for now.
            value = Regex.Replace(value, "<br>|<br />|<br/>", "\n");
            value = Regex.Replace(value, @"([\s])(www\..*?|http://.*?)([\s])",
                                  "$1<a href=\"$2\" target=\"_blank\">$2</a>$3");

            value = Regex.Replace(value, @"href=""www\.", "href=\"http://www.");
            value = Regex.Replace(value,
                                  @"([\s])([a-zA-Z][\w\._%+-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z])([\s])",
                                  "$1<a href=\"mailto:$2\" target=\"_blank\">$2</a>$3");
            value = Regex.Replace(value, "\n", "<br />");
            return value.Trim();
        }

        public static string RestrictToLengthOf(this string value, int maxLength) {
            if (string.IsNullOrWhiteSpace(value) || value.Length <= maxLength) {
               return value;
            }
            return value.Substring(0, maxLength - 1);
        }

        public static string IfEmptyThen(this string value, string emptyValue) {
            return string.IsNullOrWhiteSpace(value) ? emptyValue : value;
        }
    }
}