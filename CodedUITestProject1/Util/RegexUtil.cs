using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodedUITestProject1.Util
{
    public class RegexUtil
    {
        public const string QQTaoLunZuPattern = "http://url.cn/[^\"]*";
        public const string QQQunPattern = "https://jq.qq.com/[^\"]*";
        public const string HttpPattern = "https?:";

        public const string GetReportTimePattern = "<small class=\"smaller-20\"> 报告时间：\\s*([0-9- :]*)</small>";
        public const string GetInterfaceProcessInfoPattern = "<div class=\"infobox-data\">\\s*<div class=\"infobox-content\">\\s*(.*)\\s*</div>\\s*<div class=\"infobox-content\">(.*)</div>\\s*</div>";
        public const string GetCaseInfoPattern = "<tr>\\s*<td>(\\d*)</td><td>(.*)</td><td>\\s*(.*)\\s*</td><td><span class=\"label label-success\">\\s*(.*)\\s*</span></td>\\s*</tr>";

        public static List<String> getMatchedStrings(string subject, string pattern)
        {
            List<string> result = new List<string>();
            try
            {
                Match matchResult = Regex.Match(subject, pattern, RegexOptions.Multiline);

                while (matchResult.Success)
                {
                    for (int i = 0; i < matchResult.Groups.Count; i++)
                    {
                        result.Add(matchResult.Groups[i].Value);
                    }
                    matchResult = matchResult.NextMatch();
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }

            return result;
        }

        public static int getMatchedCount(string subject, string pattern)
        {
            int cnt = 0;

            try
            {
                Match matchResult = Regex.Match(subject, pattern);

                while (matchResult.Success)
                {
                    cnt += 1;
                    matchResult = matchResult.NextMatch();
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }

            return cnt;
        }
    }
}
