using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodedUITestProject1.Util;

namespace CodedUITestProject1.ETLReport.Case
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int cnt = RegexUtil.getMatchedCount("{\"First\":{\"Id\":0,\"Name\":\"\",\"Mobile\":\"\",\"Email\":\"\",\"Other\":\"\"},\"Spare\":[{\"Id\":0,\"Name\":\"*QQ讨论组\",\"Mobile\":\"\",\"Email\":\"\",\"Other\":\"http://url.cn/581GKjZ\"},{\"Id\":0,\"Name\":\"*QQ群\",\"Mobile\":\"\",\"Email\":\"\",\"Other\":\"https://jq.qq.com/?_wv=1027&k=5nikSKr\"}]}", "http");

            Assert.AreEqual(cnt, 2);
        }
    }
}
