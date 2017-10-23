using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace CodedUITestProject1
{
    public class MysqlHelper
    {
        private MySqlConnection mysqlcon;

        public MysqlHelper()
        {
            //string M_str_sqlcon = "server=localhost;user id=root;password=P@ssw0rd!;database=testdb";
            string M_str_sqlcon = "server=localhost;user id=root;password=123456;database=test";
            mysqlcon = new MySqlConnection(M_str_sqlcon);
            mysqlcon.Open();
        }

        public void execute(string sql)
        {
            MySqlCommand mysqlcom = new MySqlCommand(sql, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();

        }

        public MySqlDataReader query(string sql)
        {
            MySqlCommand mysqlcom = new MySqlCommand(sql, mysqlcon);
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }

    }
}
