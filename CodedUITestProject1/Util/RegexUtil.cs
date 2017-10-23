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

        public static List<String> getMatchedStrings(string subject, string pattern)
        {
            List<string> result = new List<string>();
            try
            {
                var groups = Regex.Match(subject, pattern).Groups;
                for (int i = 0; i < groups.Count; i++)
                {
                    result.Add(groups[i].Value);
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }

            return result;
        }
    }
}
