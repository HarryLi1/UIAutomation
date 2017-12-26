using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodedUITestProject1.ETLReport.Service;
using System.Collections.Generic;
using CodedUITestProject1.ETLReport.Entity;
using CodedUITestProject1.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CodedUITestProject1.Common;
using System.Net;
using CodedUITestProject1.Util;

namespace CodedUITestProject1.ETLReport.Case
{
    [TestClass]
    public class ETLReportUnitTest1
    {
        [TestMethod]
        public void TestReport_ETL()
        {
            OldReportService oldReportService = new OldReportService();
            NewReportService newReportService = new NewReportService();

            List<OldReport> list = oldReportService.getByStatus(EntityStatus.Waiting);
            Console.WriteLine("获得{0}条处理中的OldReport", list.Count);

            foreach (var info in list)
            {
                try
                {
                    Console.WriteLine("处理OldReport, ID={0}", info.ID);

                    List<NewReport> newReportList = parseNewReport(info);
                    newReportService.BulkInsert(newReportList);

                    oldReportService.UpdateStatus(info.ID, EntityStatus.Success, "成功");
                }
                catch (Exception ex)
                {
                    oldReportService.UpdateStatus(info.ID, EntityStatus.Fail, ex.Message);
                }
            }
        }

        private List<NewReport> parseNewReport(OldReport info)
        {
            List<NewReport> results = new List<NewReport>();

            //get all old report urls
            JArray reports = JArray.Parse(info.OldReportInfo);
            foreach (var report in reports)
            {
                string noticeMessage = report.SelectToken("noticeMessage").ToString();
                string reportUrl = report.SelectToken("reportUrl").ToString();
                string time = report.SelectToken("time").ToString();

                //call and get old report content
                WebHeaderCollection header = new WebHeaderCollection();
                header.Add("Authorization", "Basic 5d076e5c3d34cb8bb08e54a4bb7e223e");
                string body = HttpHelper.WebRequestToServer(reportUrl, "", null, 60000, "GET", "utf-8", "json", header, null, false, false);
                Console.Write(body);

                if (string.IsNullOrWhiteSpace(body)) continue;

                NewReport newReport = new NewReport()
                {
                    VendorId = info.VendorId
                };

                List<string> result1 = RegexUtil.getMatchedStrings(body, RegexUtil.GetReportTimePattern);
                DateTime tempDateTime; decimal tempDecimal;
                newReport.CreateTime = result1.Count > 1 && DateTime.TryParse(result1[1], out tempDateTime) ? tempDateTime : DateTime.MinValue;

                List<InterfaceInfoNode> interfaceList = new List<InterfaceInfoNode>();
                List<string> result2 = RegexUtil.getMatchedStrings(body, RegexUtil.GetInterfaceProcessInfoPattern);
                for (int i = 0; i < result2.Count; i += 3)
                {
                    InterfaceInfoNode interfaceInfoNode = new InterfaceInfoNode();
                    interfaceInfoNode.caseTypeName = (result2.Count > i + 1) ? result2[i + 1] : "";
                    interfaceInfoNode.progress = (result2.Count > i + 2) && decimal.TryParse(result2[i + 2], out tempDecimal) ? tempDecimal : 0m;
                    interfaceInfoNode.caseType = getCaseType(interfaceInfoNode.caseTypeName);

                    interfaceList.Add(interfaceInfoNode);
                }
                newReport.InterfaceProcessInfo = JsonConvert.SerializeObject(interfaceList);

                List<CaseInfoNode> caseList = new List<CaseInfoNode>();
                List<string> result3 = RegexUtil.getMatchedStrings(body, RegexUtil.GetCaseInfoPattern);
                for (int i = 0; i < result3.Count; i += 5)
                {
                    CaseInfoNode caseInfo = new CaseInfoNode();
                    caseInfo.caseId = (result3.Count > i + 1) ? result3[i + 1] : "";
                    caseInfo.caseName = (result3.Count > i + 2) ? result3[i + 2] : "";
                    caseInfo.caseTypeName = (result3.Count > i + 3) ? result3[i + 3] : "";
                    caseInfo.caseStateDesc = (result3.Count > i + 4) ? result3[i + 4] : "";

                    caseList.Add(caseInfo);
                }
                newReport.CaseInfo = JsonConvert.SerializeObject(caseList);

                results.Add(newReport);
            }

            return results;
        }

        private string getCaseType(string caseTypeName)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("健康检查", "1");
            dic.Add("下单验证", "2");
            dic.Add("订单下单", "3");
            dic.Add("订单取消", "4");
            dic.Add("订单查询", "5");
            dic.Add("消费通知", "6");
            dic.Add("订单退款", "7");

            if (dic.ContainsKey(caseTypeName))
            {
                return dic[caseTypeName];
            }

            return "0";
        }
    }
}
