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
        public const string GetByStatusSql = "SELECT * FROM discussgrouplink where Status = '{0}'";
        public const string UpdateNewLinkSql = "update discussgrouplink set NewLink = '{0}', Status = '" + EntityStatus.Success + "',Message = '处理成功',ChangeTime = now() where id = {1}";
        public const string UpdateNewStatusSql = "update discussgrouplink set Status = '{0}', Message='{1}' where id = {2}";
        public const string DeleteAllSql = "delete from discussgrouplink";
        public const string InsertSql = "insert discussgrouplink(`Key`, OldLink, NewLink, LinkType, Status, Message, CreateTime, ChangeTime ) values({0},'{1}','{2}','{3}','{4}','{5}',now(),now())";
        public const string GetByKeySql = "select * from discussgrouplink where `Key`={0}";
        public const string CheckAllProcessedSql = "select count(1) from discussgrouplink where status <> '" + EntityStatus.Success + "'";

        public List<DiscussGroupLink> getByStatus(String status)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            MysqlHelper helper = new MysqlHelper();
            MySqlDataReader dr = helper.query(string.Format(DiscussGroupLinkService.GetByStatusSql, status));
            return parseEntities(dr);
        }

        public void UpdateNewLink(long id, string newLink)
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(DiscussGroupLinkService.UpdateNewLinkSql, newLink, id));
        }

        public void UpdateStatus(long id, string newStatus, string message)
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(DiscussGroupLinkService.UpdateNewStatusSql, newStatus, message, id));
        }

        public void BulkInsert(List<DiscussGroupLink> list)
        {
            MysqlHelper helper = new MysqlHelper();
            foreach (var item in list)
            {
                helper.execute(string.Format(DiscussGroupLinkService.InsertSql,
                    item.Key,
                    item.OldLink ?? "",
                    item.NewLink ?? "",
                    item.LinkType ?? "",
                    item.Status ?? "",
                    item.Message ?? ""));
            }
        }

        public void DeleleAll()
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(DiscussGroupLinkService.DeleteAllSql));
        }

        public List<DiscussGroupLink> getByKey(long key)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            MysqlHelper helper = new MysqlHelper();
            MySqlDataReader dr = helper.query(string.Format(DiscussGroupLinkService.GetByKeySql, key));
            return parseEntities(dr);
        }

        public bool checkAllProcessed()
        {
            MysqlHelper helper = new MysqlHelper();
            MySqlDataReader dr = helper.query(DiscussGroupLinkService.CheckAllProcessedSql);
            if (dr.Read())
            {
                return dr.GetInt32(0) == 0;
            }

            return false;
        }

        private List<DiscussGroupLink> parseEntities(MySqlDataReader dr)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();

            while (dr.Read())
            {
                DiscussGroupLink entity = new DiscussGroupLink();
                entity.ID = dr.GetInt64("ID");
                entity.Key = dr.GetInt64("Key");
                entity.OldLink = dr.GetString("OldLink");
                entity.NewLink = dr.GetString("NewLink") ?? "";
                entity.LinkType = dr.GetString("LinkType");
                entity.Status = dr.GetString("Status");
                entity.Message = dr.GetString("Message") ?? "";
                entity.CreateTime = dr.GetDateTime("CreateTime");
                entity.ChangeTime = dr.GetDateTime("ChangeTime");

                results.Add(entity);
            }

            return results;
        }
    }
}
