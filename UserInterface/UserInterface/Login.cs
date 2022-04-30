using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace UserInterface
{
    public partial class Login : Form
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

        public static string email, pass;
        string sql;
        DataBase con;
        Checker check;
        string emailPattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        public static string Idemail;
        public Login()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }


        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                errorProvider1.Clear();
                panel3.BackColor = Color.DodgerBlue;
            }
            else if (Regex.IsMatch(txtMail.Text, emailPattern))
            {
                errorProvider1.Clear();
                panel3.BackColor = Color.DodgerBlue;
            }
            else
            {
                errorProvider1.SetError(this.txtMail, "Please set valid email.");
                panel3.BackColor = Color.Red;
                return;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                txtMail.Focus();
            }
            else
            {
                if (Regex.IsMatch(txtMail.Text, emailPattern))
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(this.txtMail, "Please set valid email.");
                    txtMail.Focus();
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMail.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("        Please Type Your Email & Password.       ");
            }
            else
            {
                check = new Checker();
                if (check.checkEmail(txtMail.Text)==1)
                {
                    con = new DataBase();
                    sql = "select * from "+check.tableName(txtMail.Text)+" where Email='" + txtMail.Text + "' and Password='" + txtPass.Text + "'";
                    DataTable dt1 = new DataTable();
                    con.GetData(sql).Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Idemail = txtMail.Text.ToString();
                        Profile obj = new Profile();
                        Hide();
                        obj.Location = this.Location;
                        obj.Show();
                    }
                    else
                    {
                        MessageBox.Show("       Incorrect Paswword.       ");
                    }
                    con.Dismiss();
                }
                else
                {
                    MessageBox.Show("        Couldn't find your mail.       ");
                }
            }
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

        private void label4_Click(object sender, EventArgs e)
        {
            ForgotPassword obj = new ForgotPassword();
            Hide();
            obj.Location = this.Location;
            obj.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            CreateAccount obj = new CreateAccount();
            Hide();
            obj.Location = this.Location;
            obj.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        int mov, movX, movY;

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov==1)
            {
                this.SetDesktopLocation(MousePosition.X-movX,MousePosition.Y-movY);
            }
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

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
