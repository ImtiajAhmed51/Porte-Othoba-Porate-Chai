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
    public partial class LoadingScreen : Form
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
        DataBase con;
        string sql;
        public LoadingScreen()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        private void label7_Click(object sender, EventArgs e)
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


        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
        int counter = 0;
        int len = 0;
        string text;

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            con = new DataBase();
            sql = "delete from Info WHERE (StudentStatus='Disagree' or StudentStatus='Edited') or(TeacherStatus='Waiting' or TeacherStatus='Edited' or TeacherStatus='Disagree') and AssignDate < DATEADD(day, -7, GETDATE())";
            con.ExecuteQuery(sql);
            con.Dismiss();

            label1.Hide();
            label3.Hide();
            text = label2.Text;
            len = text.Length;
            label2.Text = "";
            timer1.Start();
            timer4.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = text.Substring(0, counter);
            ++counter;
            if (counter > len)
            {
                timer1.Stop();
                text = label3.Text;
                len = text.Length;
                label3.Text = "";
                label3.Show();
                timer2.Start();
                counter = 0;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = text.Substring(0, counter);
            ++counter;
            if (counter > len)
            {
                text = label1.Text;
                len = text.Length;
                label1.Text = "";
                label1.Show();
                timer2.Stop();
                counter = 0;
                timer3.Start();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label1.Text = text.Substring(0, counter);
            ++counter;
            if (counter > len)
            {
                timer3.Stop();
            }

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            loading.Width += 8;
            if (loading.Width >= 2012)
            {
                timer4.Stop();
            }
        }

        private void LoadingScreen_Click(object sender, EventArgs e)
        {

            Login obj = new Login();
            this.Hide();
            obj.Location = this.Location;
            obj.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadingScreen_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LoadingScreen_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
