using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParser
{
    public static class HtmlCleaner
    {
        public static string CleanHtmlToSelector(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "";

            try
            {
                string[] parts = HtmlToParts(html);

                string tagName = parts[0];
                string attributesPart = parts.Length > 1 ? parts[1] : "";
                string id = "";
                List<string> classes = new List<string>();

                if (!string.IsNullOrEmpty(attributesPart))
                {
                    id = ParseAttributesPart(attributesPart, classes);
                }

                return BuildTag(tagName, id, classes);
            }
            catch
            {
                return "";
            }
        }

        private static string BuildTag(string tagName, string id, List<string> classes)
        {
            StringBuilder selector = new StringBuilder();
            selector.Append(tagName);

            if (!string.IsNullOrEmpty(id))
            {
                selector.Append("#" + id);
            }

            foreach (var cls in classes)
            {
                selector.Append("." + cls);
            }

            return selector.ToString();
        }

        private static string ParseAttributesPart(string attributesPart, List<string> classes)
        {
            string id = "";
            var attrs = attributesPart.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < attrs.Length - 1; i += 2)
            {
                string attrName = attrs[i].Trim(' ', '=');
                string attrValue = attrs[i + 1];

                if (attrName.Equals("class", StringComparison.OrdinalIgnoreCase))
                {
                    classes.AddRange(attrValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                }
                else if (attrName.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    id = attrValue;
                }
            }
            return id;
        }

        private static string[] HtmlToParts(string html)
        {
            html = html.Trim();

            if (!html.StartsWith("<"))
                html = "<" + html;

            int closingTagIndex = html.IndexOf('>');
            if (closingTagIndex == -1)
                closingTagIndex = html.Length;

            string tagContent = html.Substring(1, closingTagIndex - 1).Trim();

            return tagContent.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
