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
        public const string UpdateOldLinkSql = "update discussgrouplink set OldLink = '{0}',ChangeTime = now() where id = {1}";
        public const string UpdateNewStatusSql = "update discussgrouplink set Status = '{0}', Message='{1}',ChangeTime = now() where id = {2}";
        public const string DeleteAllSql = "delete from discussgrouplink";
        public const string InsertSql = "insert into discussgrouplink(`Key`, OldLink, NewLink, LinkType, Status, Message, CreateTime, ChangeTime ) values('{0}','{1}','{2}','{3}','{4}','{5}',now(),now())";
        public const string GetByKeySql = "select * from discussgrouplink where `Key`='{0}'";
        public const string CheckAllProcessedSql = "select count(1) from discussgrouplink where status <> '" + EntityStatus.Success + "'";

        public List<DiscussGroupLink> getByStatus(String status)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            MySqlDataReader dr = MysqlHelper.query(string.Format(DiscussGroupLinkService.GetByStatusSql, status));
            return parseEntities(dr);
        }

        public void UpdateNewLink(long id, string newLink)
        {
            MysqlHelper.execute(string.Format(DiscussGroupLinkService.UpdateNewLinkSql, newLink, id));
        }

        public void UpdateOldLink(long id, string oldLink)
        {
            MysqlHelper.execute(string.Format(DiscussGroupLinkService.UpdateOldLinkSql, oldLink, id));
        }

        public void UpdateStatus(long id, string newStatus, string message)
        {
            MysqlHelper.execute(string.Format(DiscussGroupLinkService.UpdateNewStatusSql, newStatus, message, id));
        }

        public void BulkInsert(List<DiscussGroupLink> list)
        {
            foreach (var item in list)
            {
                MysqlHelper.execute(string.Format(DiscussGroupLinkService.InsertSql,
                    item.Key,
                    item.OldLink ?? "",
                    item.NewLink ?? "",
                    item.LinkType ?? "",
                    item.Status ?? "",
                    item.Message ?? ""));
            }
        }

        public void Insert(DiscussGroupLink item)
        {
            BulkInsert(new List<DiscussGroupLink>(new DiscussGroupLink[] { item }));
        }

        public void DeleleAll()
        {
            MysqlHelper.execute(string.Format(DiscussGroupLinkService.DeleteAllSql));
        }

        public List<DiscussGroupLink> getByKey(string key)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            MySqlDataReader dr = MysqlHelper.query(string.Format(DiscussGroupLinkService.GetByKeySql, key));
            return parseEntities(dr);
        }

        public bool checkAllProcessed()
        {
            MySqlDataReader dr = MysqlHelper.query(DiscussGroupLinkService.CheckAllProcessedSql);
            try
            {
                if (dr.Read())
                {
                    return dr.GetInt32(0) == 0;
                }
            }
            finally
            {
                dr.Close();
                dr.Dispose();
            }

            return false;
        }

        private List<DiscussGroupLink> parseEntities(MySqlDataReader dr)
        {
            List<DiscussGroupLink> results = new List<DiscussGroupLink>();
            try
            {
                while (dr.Read())
                {
                    DiscussGroupLink entity = new DiscussGroupLink();
                    entity.ID = dr.GetInt64("ID");
                    entity.Key = dr.GetString("Key");
                    entity.OldLink = dr.GetString("OldLink");
                    entity.NewLink = dr.GetString("NewLink") ?? "";
                    entity.LinkType = dr.GetString("LinkType");
                    entity.Status = dr.GetString("Status");
                    entity.Message = dr.GetString("Message") ?? "";
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
