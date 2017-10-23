using CodedUITestProject1.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.DAO
{
    public class ContactInfoService
    {
        public const string GetUnprocessedDataSql = "SELECT * FROM contactinfo where Status = '" + EntityStatus.Waiting + "'";
        public const string UpdateNewValueSql = "update contactinfo set NewValue = '{0}', Status = '" + EntityStatus.Success + "', ChangeTime = now() where id = {1}";
        public const string UpdateNewStatusSql = "update contactinfo set Status = '{0}', Message='{1}' where id = {2}";
        public const string InsertSql = "insert contactinfo(`Key`, OldValue, NewValue, CategoryID, Status, Message, CreateTime, ChangeTime ) values({0},'{1}','{2}',{3},'{4}','{5}',now(),now())";

        public List<ContactInfo> getUnprocessedData()
        {
            List<ContactInfo> results = new List<ContactInfo>();
            MysqlHelper helper = new MysqlHelper();
            MySqlDataReader dr = helper.query(ContactInfoService.GetUnprocessedDataSql);

            while (dr.Read())
            {
                ContactInfo entity = new ContactInfo();
                entity.ID = dr.GetInt64("ID");
                entity.Key = dr.GetInt64("Key");
                entity.OldValue = dr.GetString("OldValue");
                entity.NewValue = dr.GetString("NewValue")??"";
                entity.CategoryID = dr.GetInt16("CategoryID");
                entity.Status = dr.GetString("Status");
                entity.Message = dr.GetString("Message")??"";
                entity.CreateTime = dr.GetDateTime("CreateTime");
                entity.ChangeTime = dr.GetDateTime("ChangeTime");

                results.Add(entity);
            }

            return results;
        }

        public void UpdateNewValue(long id, string newValue)
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(ContactInfoService.UpdateNewValueSql, newValue, id));
        }

        public void UpdateStatus(long id, string newStatus, string message)
        {
            MysqlHelper helper = new MysqlHelper();
            helper.execute(string.Format(ContactInfoService.UpdateNewStatusSql, newStatus, message, id));
        }

        public void BulkInsert(List<ContactInfo> list)
        {
            MysqlHelper helper = new MysqlHelper();
            foreach (var item in list)
            {
                helper.execute(string.Format(ContactInfoService.InsertSql,
                    item.Key,
                    item.OldValue ?? "",
                    item.NewValue ?? "",
                    item.CategoryID ,
                    item.Status ?? "",
                    item.Message ?? ""));
            }
        }
    }
}
