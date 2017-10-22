using CodedUITestProject1.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.DAO
{
    public class DiscussGroupLinkService
    {
        public const string GetUnprocessedDataSql = "SELECT * FROM testdb.discussgrouplink where IsComputed = 0";
        public const string UpdateNewLinkSql = "update testdb.discussgrouplink set NewLink = '{0}', IsComputed = 1 where id = {1}";

        public List<DiscussGroupLink> getUnprocessedData()
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            MysqlHelper helper = new MysqlHelper();
            MySqlDataReader dr = helper.query(DiscussGroupLinkService.GetUnprocessedDataSql);

            while (dr.Read())
            {
                DiscussGroupLink entity = new DiscussGroupLink();
                entity.ID = dr.GetInt64("ID");
                entity.OldLink = dr.GetString("OldLink");
                entity.NewLink = dr.GetString("NewLink");
                entity.IsComputed = dr.GetBoolean("IsComputed");

                results.Add(entity);
            }


            return results;
        }

        public void UpdateData(long id, string newLink)
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(DiscussGroupLinkService.UpdateNewLinkSql, newLink, id));
        }
    }
}
