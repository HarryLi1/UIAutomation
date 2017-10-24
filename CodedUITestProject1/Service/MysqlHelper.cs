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
        private static MySqlConnection mysqlcon = null;

        private static MySqlConnection GetMySqlConn
        {
            get
            {
                if (mysqlcon == null)
                {
                    //string M_str_sqlcon = "server=localhost;user id=root;password=P@ssw0rd!;database=testdb";
                    string M_str_sqlcon = "server=localhost;user id=root;password=123456;database=test";
                    mysqlcon = new MySqlConnection(M_str_sqlcon);
                }

                if (mysqlcon.State == ConnectionState.Closed)
                    mysqlcon.Open();

                return mysqlcon;
            }
        }

        public static void execute(string sql)
        {
            MySqlCommand mysqlcom = new MySqlCommand(sql, GetMySqlConn);
            mysqlcom.ExecuteNonQuery();
            //mysqlcom.Dispose();

        }

        public static MySqlDataReader query(string sql)
        {
            MySqlCommand mysqlcom = new MySqlCommand(sql, GetMySqlConn);
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader();
            return mysqlread;
        }

    }
}
