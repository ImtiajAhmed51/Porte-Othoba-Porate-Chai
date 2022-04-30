using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace UserInterface
{
    public class Checker
    {
        private string sql;
        DataBase con;
        public string getNumber(string txtNum)
        {
            if (txtNum.Length == 11)
            {
                return "+88" + txtNum;
            }
            else if (txtNum.Length == 13)
            {
                return "+" + txtNum;
            }
            else if (txtNum.Length == 14)
            {
                return txtNum;
            }
            else
            {
                return null;
            }
        }

        public int checkPhnNum(string txtNum)
        {
            con = new DataBase();
            sql = "select * from Student where PhoneNum='" +getNumber(txtNum) + "'";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Dismiss();
                return 1;
                
            }
            else
            {
                sql = "select * from Teacher where PhoneNum='" +getNumber(txtNum) + "'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    con.Dismiss();
                    return 1;
                }
                con.Dismiss();
                return 0;
            }

        }

        public int checkEmail(string txtMail)
        {
            con = new DataBase();
            sql = "select * from Student where Email='" + txtMail+ "'";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Dismiss();
                return 1;
            }
            else
            {
                sql = "select * from Teacher where Email='" + txtMail+ "'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    con.Dismiss();
                    return 1;
                }
                con.Dismiss();
                return 0;
            }
        }
        public string tableName(string txtMail)
        {
            con = new DataBase();
            sql = "select * from Student where Email='" + txtMail + "'";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Dismiss();
                return "Student";
                
            }
            else
            {
                sql = "select * from Teacher where Email='" + txtMail + "'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    con.Dismiss();
                    return "Teacher";
                }
                con.Dismiss();
                return "";
            }
        }
        public int getId(string email)
        {
            SqlDataReader reader = null;
            sql = "select Id from " +tableName(email) + " where Email='" + email + "'";
            con = new DataBase();
            reader = con.GetReaderData(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //con.Dismiss();
                    return reader.GetInt32(0);
                }
            }
            return 0;
        }
        public string getInfoPicture (string email)
        {
            SqlDataReader reader = null;
            
            sql = "select Picture from "+tableName(email)+" where Email='"+email+"'";
            con = new DataBase();
            reader = con.GetReaderData(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //con.Dismiss();
                    return reader.GetString(0);
                }
            }
            return "";
        }

        public string getInfoGender(string email)
        {
            SqlDataReader reader = null;

            sql = "select Gender from " + tableName(email) + " where Email='" + email + "'";
            con = new DataBase();
            reader = con.GetReaderData(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //con.Dismiss();
                    return reader.GetString(0);
                }
            }
            return "";
        }
        public bool existOrNot(string sEmail,string tEmail)
        {
            sql = "select Name from Info a inner join Student on Student.Id =a.Sid where Sid='"+getId(sEmail)+"' and TId='"+getId(tEmail)+"'";
            con = new DataBase();
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Dismiss();
                return false;

            }
            else
            {
                con.Dismiss();
                return true;
            }
        }
    }
}
