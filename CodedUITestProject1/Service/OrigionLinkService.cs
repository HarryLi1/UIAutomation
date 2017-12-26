using CodedUITestProject1.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.DAO
{
    public class OrigionLinkService
    {
        public const string InsertSql = "insert into origionlink(QQOwner, RetrieveDate, Type, Name, JoinLink, CreateTime, ChangeTime ) values('{0}','{1}','{2}','{3}','{4}',now(),now())";

        public void BulkInsert(List<OrigionLink> list)
        {
            foreach (var item in list)
            {
                MysqlHelper.execute(string.Format(OrigionLinkService.InsertSql,
                    item.QQOwner??"",
                    item.RetrieveDate.ToString("yyyy-MM-dd"),
                    item.Type ?? "",
                    item.Name ?? "",
                    item.JoinLink ?? ""));
            }
        }

        public void Insert(OrigionLink item)
        {
            BulkInsert(new List<OrigionLink>(new OrigionLink[] { item }));
        }

        private List<OrigionLink> parseEntities(MySqlDataReader dr)
        {
            List<OrigionLink> results = new List<OrigionLink>();
            try
            {
                while (dr.Read())
                {
                    OrigionLink entity = new OrigionLink();
                    entity.ID = dr.GetInt64("ID");
                    entity.QQOwner = dr.GetString("QQOwner");
                    entity.RetrieveDate = dr.GetDateTime("RetrieveDate");
                    entity.Type = dr.GetString("Type") ?? "";
                    entity.Name = dr.GetString("Name");
                    entity.JoinLink = dr.GetString("JoinLink");
                    entity.CreateTime = dr.GetDateTime("CreateTime");
                    entity.ChangeTime = dr.GetDateTime("ChangeTime");

                    results.Add(entity);
                }
            }
            finally
            {
                dr.Close();
                dr.Dispose();
            }
            return results;
        }
    }
}
