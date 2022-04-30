using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UserInterface
{
    public partial class ChangePassword : Form
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
        string IDemail = Profile.to;
        string sql;
        DataBase con;
        Checker check;
        public ChangePassword()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "" || conTxtPass.Text == "")
            {
                MessageBox.Show("       Please Current or New Password set.       ");
            }
            else
            {
                if (txtPass.Text == conTxtPass.Text)
                {
                    MessageBox.Show("       New password matched with previous password.       ");
                }
                else
                {
                    check = new Checker();
                    con = new DataBase();
                    sql = "select * from " + check.tableName(IDemail) + " where Email='" +IDemail + "' and Password='" + txtPass.Text + "'";
                    DataTable dt = new DataTable();
                    con.GetData(sql).Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "update " + check.tableName(IDemail) + " set Password='" + conTxtPass.Text + "' where Email='" + IDemail + "'";
                        con.ExecuteQuery(sql);
                        con.Dismiss();
                        txtPass.Text = "";
                        conTxtPass.Text = "";
                        MessageBox.Show("       New password updated.       ");
                    }
                    else
                    {

                        MessageBox.Show("       Current password is wrong.       ");
                    }
                    con.Dismiss();
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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
            Profile obj = new Profile(IDemail);
            Hide();
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = this.Location;
            obj.Show();
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
