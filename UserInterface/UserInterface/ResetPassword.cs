using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
namespace UserInterface
{
    public partial class ResetPassword : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
           int left,
           int top,
           int right,
           int bottom,
           int width,
           int height
           );


        string emailId = ForgotPassword.to;
        string sql;
        DataBase con;
        Checker check;
        public ResetPassword()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == '●')
            {
                button3.BringToFront();
                txtPass.PasswordChar = '\0';

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (txtPass.PasswordChar == '\0')
            {
                button4.BringToFront();
                txtPass.PasswordChar = '●';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conTxtPass.PasswordChar == '●')
            {
                button5.BringToFront();
                conTxtPass.PasswordChar = '\0';
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (conTxtPass.PasswordChar == '\0')
            {
                button2.BringToFront();
                conTxtPass.PasswordChar = '●';
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "" || conTxtPass.Text == "")
            {
                MessageBox.Show("Please set your new password.");
            }
            else if (txtPass.Text == conTxtPass.Text)
            {
                con = new DataBase();
                check = new Checker();
                sql = "select Password from " + check.tableName(emailId) + " where Email='" + emailId + "' and Password='" + conTxtPass.Text + "'";
                DataTable dt = new DataTable();
                con.GetData(sql).Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    con.Dismiss();
                    txtPass.Text = "";
                    conTxtPass.Text = "";
                    MessageBox.Show("    You used this password. Please choose a different one.   ");
                }
                else
                {
                    sql = "update " + check.tableName(emailId) + " set Password='" + conTxtPass.Text + "' where Email='" + emailId + "'";
                    con.ExecuteQuery(sql);
                    con.Dismiss();
                    MessageBox.Show("       Reset Passsword Successfully.    ");
                    Login obj = new Login();
                    Hide();
                    obj.StartPosition = FormStartPosition.Manual;
                    obj.Location = this.Location;
                    obj.Show();
                }
                
            }
            else
            {
                MessageBox.Show("    The new password do not match so enter same password.");
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int mov, movX, movY;

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            Hide();
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = this.Location;
            obj.Show();
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }
    }
}
