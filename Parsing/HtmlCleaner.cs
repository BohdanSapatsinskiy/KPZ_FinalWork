using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParser.Parsing
{
    public static class HtmlCleaner
    {
        public static string CleanHtmlToSelector(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "";

            try
            {
                html = html.Trim();

                if (!html.StartsWith("<"))
                    html = "<" + html;

                int closingTagIndex = html.IndexOf('>');
                if (closingTagIndex == -1)
                    closingTagIndex = html.Length;

                string tagContent = html.Substring(1, closingTagIndex - 1).Trim();

                string[] parts = tagContent.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string tagName = parts[0];
                string attributesPart = parts.Length > 1 ? parts[1] : "";

                string id = "";
                List<string> classes = new List<string>();

                if (!string.IsNullOrEmpty(attributesPart))
                {
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
                }

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
            catch
            {
                return "";
            }
        }
    }
}
