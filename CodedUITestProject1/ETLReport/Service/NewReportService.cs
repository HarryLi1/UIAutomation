using CodedUITestProject1.Entity;
using CodedUITestProject1.ETLReport.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.ETLReport.Service
{
    public class NewReportService
    {
        public const string InsertSql = "insert into jnt_autotest_testreport(VendorId, Environment, TotalProgress, InterfaceProcessInfo, CaseInfo, Datachange_CreateUser, Datachange_CreateTime) values({0},1,'100','{1}','{2}','auto','{3}')";
     
        public void BulkInsert(List<NewReport> list)
        {
            foreach (var item in list)
            {
                MysqlHelper.execute(string.Format(NewReportService.InsertSql,
                    item.VendorId,
                    item.InterfaceProcessInfo ?? "",
                    item.CaseInfo ?? "",
                    item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));
            }
        }

        public void Insert(NewReport item)
        {
            BulkInsert(new List<NewReport>(new NewReport[] { item }));
        }
    }
}
