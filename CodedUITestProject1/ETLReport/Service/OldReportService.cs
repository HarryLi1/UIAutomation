using CodedUITestProject1.Entity;
using CodedUITestProject1.ETLReport.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.ETLReport.Service
{
    public class OldReportService
    {
        
        public const string GetByStatusSql = "SELECT * FROM jnt_autotest_oldreport where Status = '{0}'";
        public const string UpdateNewStatusSql = "update jnt_autotest_oldreport set Status = '{0}', Message='{1}',ChangeTime=now() where id = {2}";

        public List<OldReport> getByStatus(String status)
        {
            List<OldReport> results = new List<OldReport>();
            MySqlDataReader dr = MysqlHelper.query(string.Format(OldReportService.GetByStatusSql, status));
            return parseEntities(dr);
        }

        public void UpdateStatus(long id, string newStatus, string message)
        {
            MysqlHelper.execute(string.Format(OldReportService.UpdateNewStatusSql, newStatus, message, id));
        }

        private List<OldReport> parseEntities(MySqlDataReader dr)
        {
            List<OldReport> results = new List<OldReport>();

            while (dr.Read())
            {
                OldReport entity = new OldReport();
                entity.ID = dr.GetInt64("Id");
                entity.VendorId = dr.GetInt64("VendorId");
                entity.OldReportInfo = dr.GetString("OldReport");
                entity.Status = dr.GetString("Status");
                entity.Message = dr.GetString("Message") ?? "";
                entity.CreateTime = dr.GetDateTime("CreateTime");
                entity.ChangeTime = dr.GetDateTime("ChangeTime");
                entity.CreateUser = dr.GetString("CreateUser");
                entity.ChangeUser = dr.GetString("ChangeUser");

                results.Add(entity);
            }

            dr.Close();
            dr.Dispose();

            return results;
        }
    }
}
