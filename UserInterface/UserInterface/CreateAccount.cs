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
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace UserInterface
{
    public partial class CreateAccount : Form
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
        Checker check;
        string phnPattern = ("(?:\\+88|88)?(01[3-9]\\d{8})");
        string emailPattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        string imgLocation = "";
        public CreateAccount()
        {
            InitializeComponent();
            picture.Image = Properties.Resources.userDefault;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        private void reset()
        {
            txtName.Text = null;
            txtNum.Text = null;
            txtMail.Text = null;
            txtPass.Text = null;
            txtAdd.Text = null;
            Gender.SelectedIndex = -1;
            Type.SelectedIndex = -1;
            picture.Image = Properties.Resources.userDefault;
            label13.Visible=false;
            Qual.Text = null;
            Qual.Visible=false;
            DOB.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            errorProvider3.Clear();
            errorProvider5.Clear();
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

        private void label5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            Hide();
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = this.Location;
            obj.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtNum.Text == "" || txtMail.Text == "" || txtPass.Text == ""||txtAdd.Text==""||Gender.SelectedIndex==-1||Type.SelectedIndex==-1)
            {
                MessageBox.Show("       Missing Information.       ");
            }
            else
            {
                try
                {
                    /*check = new Checker();
                    byte[] images = null;
                    FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(Stream);
                    images = brs.ReadBytes((int)Stream.Length);*/
                    //MemoryStream ms = new MemoryStream();
                    //picture.Image.Save(ms, picture.Image.RawFormat);
                    //byte[] img = ms.ToArray();
                    //Image ping = picture.Image;
                    //ImageConverter Converter = new ImageConverter();
                    //var ImageConvert = Converter.ConvertTo(ping, typeof(byte[]));
                    if (Type.SelectedItem.ToString() == "Student")
                    { 
                        if (check.checkEmail(txtMail.Text) != 1 && check.checkPhnNum(txtNum.Text) != 1 && Regex.IsMatch(txtNum.Text, phnPattern) && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            con = new DataBase();
                            sql = "insert into Student values ('" + txtName.Text + "','" + check.getNumber(txtNum.Text) + "','" + txtMail.Text + "','" + txtPass.Text + "','"+txtAdd.Text+"','"+Gender.SelectedItem.ToString()+"','"+imgLocation+"','"+ DOB.Value.Date+"')";
                            con.ExecuteQuery(sql);
                            con.Dismiss();
                            reset();
                            MessageBox.Show("       Account Created Successfully.       ");
                        }
                        else if (check.checkEmail(txtMail.Text) != 0 && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            MessageBox.Show("       This email already exists.       ");
                        }
                        else if (check.checkPhnNum(txtNum.Text) != 0 && Regex.IsMatch(txtNum.Text, phnPattern))
                        {
                            MessageBox.Show("       This number already exists.       ");
                        }
                        else if (Regex.IsMatch(txtMail.Text, emailPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid email.       ");
                        }
                        else if (Regex.IsMatch(txtNum.Text, phnPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid number.       ");
                        }
                    }
                    else if (Type.SelectedItem.ToString() == "Teacher")
                    {
                        if (Qual.SelectedIndex == -1)
                        {
                            MessageBox.Show("       Please set your Qualification.       ");
                        }else if(check.checkEmail(txtMail.Text) != 1 && check.checkPhnNum(txtNum.Text) != 1 && Regex.IsMatch(txtNum.Text, phnPattern) && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            con = new DataBase();
                            sql = "insert into Teacher values ('" + txtName.Text + "','" + check.getNumber(txtNum.Text) + "','" + txtMail.Text + "','" + txtPass.Text + "','" + txtAdd.Text + "','" + Gender.SelectedItem.ToString() + "','" + imgLocation + "','" + DOB.Value.Date + "','"+Qual.SelectedItem.ToString()+"')";
                            con.ExecuteQuery(sql);
                            con.Dismiss();
                            reset();
                            MessageBox.Show("       Account Created Successfully.       ");
                        }
                        else if (check.checkEmail(txtMail.Text) != 0 && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            MessageBox.Show("       This email already exists.       ");
                        }
                        else if (check.checkPhnNum(txtNum.Text) != 0 && Regex.IsMatch(txtNum.Text, phnPattern))
                        {
                            MessageBox.Show("       This number already exists.       ");
                        }
                        else if (Regex.IsMatch(txtMail.Text, emailPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid email.       ");
                        }
                        else if (Regex.IsMatch(txtNum.Text, phnPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid number.       ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("       Please select your type.       ");
                    }


                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void txtNum_Leave(object sender, EventArgs e)
        {
            check = new Checker();
            if (txtNum.Text == "")
            {
                errorProvider3.Clear();
            }
            else if (check.checkPhnNum(txtNum.Text)==1&&Regex.IsMatch(txtNum.Text, phnPattern))
            {
                errorProvider4.SetError(txtNum, "This number already exists.");
                txtNum.Focus();
                return;
            }
            else
            {
                if (Regex.IsMatch(txtNum.Text, phnPattern))
                {
                    errorProvider4.Clear();
                    errorProvider3.Clear();
                }
                else
                {
                    txtNum.Focus();
                    errorProvider3.SetError(txtNum, "Please set your valid number.");
                    return;
                }
            }
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
            if (txtNum.Text == "")
            {
                errorProvider3.Clear();
            }
            else if (Regex.IsMatch(txtNum.Text, phnPattern))
            {
                txtNum.ForeColor = Color.Green;
                errorProvider3.Clear();
            }
            else
            {
                txtNum.ForeColor = Color.Red;
                errorProvider3.SetError(txtNum, "Please set your valid number.");
              
            }
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            if (txtMail.Text == "")
            {
                errorProvider5.Clear();
            }
            else if (Regex.IsMatch(txtMail.Text, emailPattern))
            {
                errorProvider5.Clear();
                txtMail.ForeColor = Color.DimGray;

            }
            else{
                txtMail.ForeColor = Color.Red;
                errorProvider5.SetError(this.txtMail, "Please set your valid Email.");
            }
        }
        private void txtMail_Leave(object sender, EventArgs e)
        {
            check = new Checker();
            if (txtMail.Text == "")
            {
                errorProvider5.Clear();
            }
            else if (check.checkEmail(txtMail.Text)==1&& Regex.IsMatch(txtMail.Text, emailPattern))
            {
                errorProvider6.SetError(txtMail, "This Email already exists.");
                txtMail.ForeColor = Color.Red;
                txtMail.Focus();
                return;

            }
            else
            {
                if (Regex.IsMatch(txtMail.Text, emailPattern))
                {
                    txtMail.ForeColor = Color.DimGray;
                    errorProvider6.Clear();
                    errorProvider5.Clear();

                }
                else
                {
                    errorProvider5.SetError(this.txtMail, "Please set your valid Email.");
                    txtMail.Focus();
                }

            }
             
        }
        private void label8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int mov, movX, movY;

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "Image Files(*.JPG;*.PNG;*.GIF)|*.JPG;*.PNG;*.GIF";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                picture.ImageLocation = imgLocation;
            }
        }


        private void Type_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (Type.SelectedItem.ToString() == "Student")
            {
                Qual.SelectedIndex = -1;
                label13.Visible = false;
                Qual.Visible = false;
            }
            else if (Type.SelectedItem.ToString() == "Teacher")
            {
                label13.Visible = true;
                Qual.Visible = true;
            }
        }

        private void Type_Leave(object sender, EventArgs e)
        {
            if (Type.Text.ToString()!= "Teacher")
            {
                Qual.SelectedIndex = -1;
                label13.Visible = false;
                Qual.Visible = false;
            }
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }
    }
}
