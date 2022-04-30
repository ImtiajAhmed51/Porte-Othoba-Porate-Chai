using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UserInterface
{
    public  class DataBase
    {
        private SqlConnection con;
        private SqlCommand command;
        public static string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Imtiaj Ahmed\source\repos\Project\UserInterface\UserInterface\DataStore\DataStore.mdf;Integrated Security=True;Connect Timeout=30";
        public DataBase()
        {
            con = new SqlConnection(conString);
            con.Open();
        }

        public SqlDataReader GetReaderData(string sql)
        {
            command = new SqlCommand(sql,con);
            return command.ExecuteReader();
        }

        public SqlDataAdapter GetData(string sql)
        {
            return new SqlDataAdapter(sql,con);
        }

        public int ExecuteQuery(string sql)
        {
            command = new SqlCommand(sql,con);
            return command.ExecuteNonQuery();
        }

        public void Dismiss()
        {
            con.Close();
        }
    }
}

