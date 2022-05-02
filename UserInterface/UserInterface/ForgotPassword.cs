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
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace UserInterface
{
    public partial class ForgotPassword : Form
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

        public static string to;
        private static string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        private int randomCode;
        Checker check;
        public ForgotPassword()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                MessageBox.Show("       Please set your email.       ");
            }
            else
            {
                if (Regex.IsMatch(txtMail.Text, pattern))
                {
                    check = new Checker();
                    if (check.checkEmail(txtMail.Text)==1)
                    {
                        Random x = new Random();
                        randomCode = x.Next(100000, 999999);
                        string to, from, pass;
                        MailMessage message = new MailMessage();
                        to = txtMail.Text;
                        from = "imtiajpes@gmail.com";
                        pass = "imtiajaa8151609";
                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = "Porte Othoba Porate Chai. \nYour reset code is : " + randomCode.ToString() + ".";
                        message.Subject = "Password Reseting Code.";
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);
                        try
                        {
                            smtp.Send(message);
                            MessageBox.Show("        Code send successfully.\n        Please check your mail!         ");
                            button2.Enabled = true;
                            txtCode.Enabled = true;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("       "+ex.Message+ "\n       Please check your internet connection and try again!!       ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("       Account does not exist.       ");
                    }                  
                }
                else
                {
                    MessageBox.Show("       Please type your valid email.       ");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (randomCode.ToString() == txtCode.Text.ToString())
            {
                to = txtMail.Text;
                ResetPassword obj = new ResetPassword();
                Hide();
                obj.Location = this.Location;
                obj.Show();
            }
            else
            {
                MessageBox.Show("       Wrong Code.                  ");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            Hide();
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = this.Location;
            obj.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void txtMail_Leave(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                button2.Enabled = false;
                txtCode.Enabled = false;
                errorProvider1.Clear();
            }
        }
        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            if (txtMail.Text == "")
            {
                errorProvider1.Clear();
            }
            else if (Regex.IsMatch(txtMail.Text, pattern))
            {
                errorProvider1.Clear();
                button2.Enabled = false;
                txtCode.Enabled = false;
                txtCode.Text = "";

            }
            else
            {
                
                button2.Enabled = false;
                txtCode.Enabled = false;
                txtCode.Text = "";
                errorProvider1.SetError(txtMail, "Please set your valid email.");
                return;
            }
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
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

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }
    }
}
