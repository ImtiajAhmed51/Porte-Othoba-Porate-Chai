using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
namespace UserInterface
{
    public partial class Profile : Form
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
        string IDemail = Login.Idemail;
        DataBase con;
        private string sql,sql1;
        Checker check;
        string phnPattern = ("(?:\\+88|88)?(01[3-9]\\d{8})");
        string emailPattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        int numCheck = 0, emailCheck = 0;
        string imgLocation="";
        public static string to;
        public Profile()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        public Profile(string email)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            IDemail = email;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int mov, movX, movY;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        int dashBoard = 0;
        private void WelCome_Load(object sender, EventArgs e)
        {
            con = new DataBase();
            check = new Checker();
            sql = "select * from " + check.tableName(IDemail) + " where Email='" + IDemail + "'";
            DataTable dt = new DataTable();
            Type.Text = check.tableName(IDemail);
            con.GetData(sql).Fill(dt);
            if (dt.Rows.Count > 0)
            {
                check = new Checker();
                try
                {
                    if (dt.Rows[0][7].ToString() == "")
                    {
                        if (dt.Rows[0][6].ToString() == "Male")
                        {
                            picture.Image = Properties.Resources.userMan;
                        }
                        else if (dt.Rows[0][6].ToString() == "Female")
                        {
                            picture.Image = Properties.Resources.userWoman;
                        }
                        else
                        {
                            picture.Image = Properties.Resources.userDefault;
                        }
                    }
                    else
                    {
                        Image image = Image.FromFile(@"" + dt.Rows[0][7].ToString() + "");
                        var ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        var bytes = ms.ToArray();
                        var imageMemoryStream = new MemoryStream(bytes);
                        Image imgFromStream = Image.FromStream(imageMemoryStream);
                        picture.Image = imgFromStream;
                    }
                    
                }catch
                {
                    if (dt.Rows[0][6].ToString() == "Male")
                    {
                        picture.Image = Properties.Resources.userMan;
                    }
                    else if(dt.Rows[0][6].ToString() == "Female")
                    {
                        picture.Image = Properties.Resources.userWoman;
                    }
                    else
                    {
                        picture.Image = Properties.Resources.userDefault;
                    }
                }
                if (check.tableName(IDemail) == "Teacher")
                {
                    label13.Visible = true;
                    Qual.Visible = true;
                    Qual.SelectedItem = dt.Rows[0][9].ToString();
                }
                label3.Text= dt.Rows[0][1].ToString();
                txtName.Text = dt.Rows[0][1].ToString();
                txtNum.Text = dt.Rows[0][2].ToString();
                txtMail.Text = dt.Rows[0][3].ToString();
                txtAdd.Text = dt.Rows[0][5].ToString();
                Gender.SelectedItem = dt.Rows[0][6].ToString();
                DOB.Text = dt.Rows[0][8].ToString();
                if (dashBoard == 0)
                {
                    dashBoardView();
                    ++dashBoard;
                }
            }
            con.Dismiss();
        }
        void dashBoardView()
        {
            if (check.tableName(IDemail) == "Teacher")
            {



                //btnInfoUpdate.Visible = true;
                dashExpand = false;
                //btnInfoAgree.Visible = true;
                //btnInfoDis.Visible = true;
                label27.Visible = true;
                label26.Visible = true;
                label27.Text = "Student Status";
                Status1.Visible = true;
                Status2.Visible = true;

                startingDate.Enabled = false;
                txtDes.Enabled = false;
                txtTime.Enabled = false;
                txtSal.Enabled = false;

                dataInfoView.Visible = true;
                btnTeaApp.Left = 137;
                panel11.Left = 22;
                infoPhnNum.Left = 22;
                label19.Left = 15;
                btnTeaApp.Text = "Student Approval Status";
                btnTeaApp.BackColor = Color.MediumSeaGreen;
                btnTeaInfo.Visible = false;
                studentInfo();
            }

            else
            {
                btnRequest.Visible = true;
                dashPanel.Left = 135;
                label18.Visible = true;
                infoQual.Visible = true;
                panel14.Visible = true;
                infoPicture.Visible = false;
                dataInfoView.Visible = true;
                teacherInfo();
            }
        }
        private void Update_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            label8.Visible = false;
            label6.Visible = true;
            txtName.Visible = true;
            panel3.Visible = true;
            txtName.Enabled = true;
            txtNum.Enabled = true;
            txtMail.Enabled = true;
            txtAdd.Enabled = true;
            Gender.Enabled = true;
            DOB.Enabled = true;
            Qual.Enabled = true;
            Update.Visible = false;
            Save.Visible = true;
            logOut.Visible = false;
            cancel.Visible = true;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            check = new Checker();
            if (check.tableName(IDemail) == "Teacher")
            {
                if (txtName.Text == "" || txtNum.Text == "" || txtMail.Text == "" || txtAdd.Text == "" || Gender.SelectedIndex == -1||Qual.SelectedIndex==-1)
                {
                    MessageBox.Show("       Missing Information.       ");
                }
                else
                {
                    try
                    {
                        if (Regex.IsMatch(txtMail.Text, emailPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid email.       ");
                        }
                        else if (Regex.IsMatch(txtNum.Text, phnPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid number.       ");
                        }
                        else if (check.checkPhnNum(txtNum.Text) == 1 || check.checkEmail(txtMail.Text) == 1 && Regex.IsMatch(txtNum.Text, phnPattern) && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            this.txtMail_Leave(sender, e);
                            this.txtNum_Leave(sender, e);
                            con = new DataBase();
                            if (numCheck == 1 && emailCheck == 1)
                            {
                                sql1 = "update " + check.tableName(IDemail) + " set Name= '" + txtName.Text + "' , PhoneNum= '" + check.getNumber(txtNum.Text) + "', Email= '" + txtMail.Text + "' , Address= '" + txtAdd.Text + "' , Gender= '" + Gender.SelectedItem.ToString() + "' , DOB= '" + DOB.Value.Date + "' ,Qualification='"+Qual.SelectedItem.ToString()+"' where Email= '" + IDemail + "'";
                                con.ExecuteQuery(sql1);
                                con.Dismiss();
                                Update.Visible = true;
                                Gender.Enabled = false;
                                DOB.Enabled = false;
                                txtName.Enabled = false;
                                txtNum.Enabled = false;
                                txtMail.Enabled = false;
                                txtAdd.Enabled = false;
                                //qual
                                Qual.Enabled = false;
                                Save.Visible = false;
                                logOut.Visible = true;
                                cancel.Visible = false;

                                label6.Visible = false;
                                txtName.Visible = false;
                                panel3.Visible = false;
                                label8.Visible = true;
                                label5.Visible = true;
                                label3.Text = txtName.Text;
                                IDemail = txtMail.Text.ToString();

                                MessageBox.Show("       Updated Successfully.       ");
                            }
                            else if (numCheck != 1)
                            {

                                MessageBox.Show("       This number already exists.       ");
                            }
                            else if (emailCheck != 1)
                            {
                                MessageBox.Show("       This email already exists.       ");
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
            else
            {
                if (txtName.Text == "" || txtNum.Text == "" || txtMail.Text == "" || txtAdd.Text == "" || Gender.SelectedIndex == -1)
                {
                    MessageBox.Show("       Missing Information.       ");
                }
                else
                {
                    try
                    {
                        if (Regex.IsMatch(txtMail.Text, emailPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid email.       ");
                        }
                        else if (Regex.IsMatch(txtNum.Text, phnPattern) != true)
                        {
                            MessageBox.Show("       Please set your valid number.       ");
                        }
                        else if (check.checkPhnNum(txtNum.Text) == 1 || check.checkEmail(txtMail.Text) == 1 && Regex.IsMatch(txtNum.Text, phnPattern) && Regex.IsMatch(txtMail.Text, emailPattern))
                        {
                            this.txtMail_Leave(sender, e);
                            this.txtNum_Leave(sender, e);
                            con = new DataBase();
                            if (numCheck == 1 && emailCheck == 1)
                            {
                                sql1 = "update " + check.tableName(IDemail) + " set Name= '" + txtName.Text + "' , PhoneNum= '" + check.getNumber(txtNum.Text) + "', Email= '" + txtMail.Text + "' , Address= '" + txtAdd.Text + "' , Gender= '" + Gender.SelectedItem.ToString() + "' , DOB= '" + DOB.Value.Date + "' where Email= '" + IDemail + "'";
                                con.ExecuteQuery(sql1);
                                con.Dismiss();
                                Update.Visible = true;
                                Gender.Enabled = false;
                                DOB.Enabled = false;
                                txtName.Enabled = false;
                                txtNum.Enabled = false;
                                txtMail.Enabled = false;
                                txtAdd.Enabled = false;
                                Save.Visible = false;
                                logOut.Visible = true;
                                cancel.Visible = false;
                                label6.Visible = false;
                                txtName.Visible = false;
                                panel3.Visible = false;
                                label8.Visible = true;
                                label5.Visible = true;
                                label3.Text = txtName.Text;
                                IDemail = txtMail.Text.ToString();

                                MessageBox.Show("       Updated Successfully.       ");
                            }
                            else if (numCheck != 1)
                            {

                                MessageBox.Show("       This number already exists.       ");
                            }
                            else if (emailCheck != 1)
                            {
                                MessageBox.Show("       This email already exists.       ");
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void txtNum_Leave(object sender, EventArgs e)
        {
            check = new Checker();
            if (txtNum.Text == "")
            {
                panel2.BackColor = Color.DodgerBlue;
                errorProvider3.Clear();
            }
            else if (check.checkPhnNum(txtNum.Text) == 1 && Regex.IsMatch(txtNum.Text, phnPattern))
            {
                con = new DataBase();
                sql = "select  PhoneNum from " + check.tableName(IDemail) + " where Email='" + IDemail + "'";
                DataTable dt = new DataTable();
                con.GetData(sql).Fill(dt);
                if (check.getNumber(txtNum.Text) == dt.Rows[0][0].ToString())
                {
                    errorProvider4.Clear();
                    numCheck = 1;
                    panel2.BackColor = Color.DodgerBlue;
                }
                else
                {
                    errorProvider4.SetError(txtNum, "This number already exists.");
                    txtNum.Focus();

                    panel2.BackColor = Color.Red;
                    numCheck = 0;
                }
            }
            else
            {
                if (Regex.IsMatch(txtNum.Text, phnPattern))
                {
                    errorProvider4.Clear();
                    errorProvider3.Clear();
                    numCheck = 1;
                    panel2.BackColor = Color.DodgerBlue;
                }
                else
                {
                    txtNum.Focus();
                    errorProvider3.SetError(txtNum, "Please set your valid number.");

                    panel2.BackColor = Color.Red;
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

                panel2.BackColor = Color.Red;
            }
            else if (Regex.IsMatch(txtNum.Text, phnPattern))
            {
                txtNum.ForeColor = Color.Green;
                errorProvider3.Clear();

                panel2.BackColor = Color.DodgerBlue;
            }
            else
            {

                panel2.BackColor = Color.Red;
                txtNum.ForeColor = Color.Red;
                errorProvider3.SetError(txtNum, "Please set your valid number.");

            }
        }

        private void uploadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "Image Files(*.JPG;*.PNG;*.GIF)|*.JPG;*.PNG;*.GIF";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                picture.ImageLocation = imgLocation;

                con = new DataBase();

                sql = "update " + check.tableName(IDemail) + " set Picture= '" +imgLocation+ "' where Email= '" + IDemail + "'";
                con.ExecuteQuery(sql);
                con.Dismiss();

            }
        }

        private void txtName_MouseClick(object sender, MouseEventArgs e)
        {
            panel3.BackColor = Color.Red;
        }


        private void txtName_Leave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.DodgerBlue;
        }

        private void txtNum_MouseClick(object sender, MouseEventArgs e)
        {

            panel2.BackColor = Color.Red;
        }

        private void uploadPicture_MouseMove(object sender, MouseEventArgs e)
        {
            //uploadPicture.Visible = false;
        }

        private void uploadPicture_MouseLeave(object sender, EventArgs e)
        {
            uploadPicture.Visible = false;
        }

        private void picture_Click(object sender, EventArgs e)
        {
            uploadPicture.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            Hide();
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = this.Location;
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check = new Checker();
            if (click == 1)
            {
                if (check.tableName(IDemail) == "Teacher")
                {
                    label13.Visible = true;
                    Qual.Visible = true;
                }
                label6.Visible = false;
                txtName.Visible = false;
                panel3.Visible = false;
                picture.Visible = true;
                //label6.Visible = true;
                //txtName.Visible = true;
                label7.Visible = true;
                txtNum.Visible = true;
                label12.Visible = true;
                DOB.Visible = true;
                label9.Visible = true;
                Gender.Visible = true;
                
                label4.Visible = true;
                txtMail.Visible = true;
                label10.Visible = true;
                txtAdd.Visible = true;
                Update.Visible = true;
                logOut.Visible = true;
                cancel.Visible = false;
                //panel3.Visible = true;
                panel2.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                label5.Visible = true;
                label8.Visible = true;
                Delete.Visible = false;

                button4.Visible = false;
                button3.Visible = false;
                label11.Visible = false;
                panel6.Visible = false;
                txtPass.Visible = false;
                label14.Visible = false;
                txtPass.Text = "";
                click = 0;
            }
            else
            {
                /*if (teacherAppChecker == 1)
                {
                    btnTeaInfo_Click(sender, e);
                }*/
                WelCome_Load(sender,e);
                label8.Visible = true;
                label5.Visible = true;
                label6.Visible = false;
                txtName.Visible = false;
                panel3.Visible = false;
                Gender.Enabled = false;
                DOB.Enabled = false;
                txtName.Enabled = false;
                txtNum.Enabled = false;
                txtMail.Enabled = false;
                txtAdd.Enabled = false;
                //qual
                Qual.Enabled = false;
                Update.Visible = true;
                logOut.Visible = true;
                Save.Visible = false;
                cancel.Visible = false;
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            to =IDemail;
            ChangePassword obj = new ChangePassword();
            Hide();
            obj.Location = this.Location;
            obj.Show();
        }


        private void txtAdd_Leave(object sender, EventArgs e)
        {

            panel5.BackColor = Color.DodgerBlue;
        }

        private void txtAdd_MouseClick(object sender, MouseEventArgs e)
        {
            panel5.BackColor = Color.Red;
        }
        int click = 0;
        private void label8_Click(object sender, EventArgs e)
        {
            picture.Visible = false;
            label6.Visible = false;
            txtName.Visible = false;
            label7.Visible = false;
            txtNum.Visible = false;
            label12.Visible = false;
            DOB.Visible = false;
            label9.Visible = false;
            Gender.Visible = false;
            label13.Visible = false;
            Qual.Visible = false;
            label4.Visible = false;
            txtMail.Visible = false;
            label10.Visible = false;
            txtAdd.Visible = false;
            Update.Visible = false;
            logOut.Visible = false;
            cancel.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            label5.Visible = false;
            label8.Visible = false;
            Delete.Visible = true;

            button4.Visible = true;
            button3.Visible = true;
            label11.Visible = true;
            panel6.Visible = true;
            txtPass.Visible = true;
            label14.Visible = true;
            
            
            click = 1;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                MessageBox.Show("       Please enter your Password.       ");
                return;
            }
            try
            {
                check = new Checker();
                con = new DataBase();
                sql = "select * from " + check.tableName(IDemail) + " where Email='" + IDemail + "' and Password='" + txtPass.Text + "'";
                DataTable dt = new DataTable();
                con.GetData(sql).Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("  Do You Want Delete Your Account ?            ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        sql = "delete from " + check.tableName(IDemail) + " where Email='" + IDemail + "' and Password='" + txtPass.Text + "'";
                        con.ExecuteQuery(sql);
                        con.Dismiss();
                        txtPass.Text = "";
                        MessageBox.Show("       Successfully deleted.       ");
                        Login obj = new Login();
                        Hide();
                        obj.StartPosition = FormStartPosition.Manual;
                        obj.Location = this.Location;
                        obj.Show();
                    }
                }
                else
                {
                    MessageBox.Show("       Incorrect password.       ");
                    con.Dismiss();
                }
            }
            catch
            {

                check = new Checker();
                if (check.tableName(IDemail) == "Student")
                {
                    MessageBox.Show("You can not delete your account because you're supervised by your teacher !!!\n\nIf there is no consent between two parties within seven days then the data will be automatically deleted. Then you can delete your account.");
                }
                else
                {
                    MessageBox.Show("You cannot delete your account because you are supervising the students !!!\n\nIf there is no consent between two parties within seven days then the data will be automatically deleted. Then you can delete your account.");
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

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

            errorProvider6.Clear();
            if (txtMail.Text == "")
            {
                panel4.BackColor = Color.Red;
                errorProvider5.Clear();
            }
            else if (Regex.IsMatch(txtMail.Text, emailPattern))
            {
                errorProvider5.Clear();
                txtMail.ForeColor = Color.DimGray;
                panel4.BackColor = Color.DodgerBlue;

            }
            else
            {
                txtMail.ForeColor = Color.Red;
                errorProvider5.SetError(this.txtMail, "Please set your valid Email.");
                panel4.BackColor = Color.Red;
            }
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {


            check = new Checker();
            if (txtMail.Text == "")
            {
                panel4.BackColor = Color.DodgerBlue;
                errorProvider5.Clear();
            }
            else if (check.checkEmail(txtMail.Text) == 1 && Regex.IsMatch(txtMail.Text, emailPattern))
            {
                con = new DataBase();
                sql = "select  Email from " + check.tableName(IDemail) + " where Email='" + IDemail + "'";
                DataTable dt = new DataTable();
                con.GetData(sql).Fill(dt);
                if (txtMail.Text == dt.Rows[0][0].ToString())
                {
                    panel4.BackColor = Color.DodgerBlue;
                    emailCheck = 1;
                    errorProvider6.Clear();
                }
                else
                {
                    errorProvider6.SetError(txtMail, "This Email already exists.");
                    txtMail.ForeColor = Color.Red;
                    txtMail.Focus();
                    emailCheck = 0;
                    panel4.BackColor = Color.Red;
                }
                con.Dismiss();
            }
            else
            {
                if (Regex.IsMatch(txtMail.Text, emailPattern))
                {

                    panel4.BackColor = Color.DodgerBlue;
                    txtMail.ForeColor = Color.DimGray;
                    errorProvider6.Clear();
                    errorProvider5.Clear();
                    emailCheck = 1;
                }
                else
                {
                    errorProvider5.SetError(this.txtMail, "Please set your valid Email.");
                    txtMail.Focus();
                    panel4.BackColor = Color.Red;
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
            else
            {
                sidebarExpand = false;
                sidebarTimer.Start();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtSearch.Text);
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
            panel7.BackColor = Color.Red;
        }

        private void dataTeacherView_Click(object sender, EventArgs e)
        {
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.DodgerBlue;
        }


        void teacherInfo()
        {
            con = new DataBase();
            sql = "select Id, Name, Qualification, PhoneNum, Email, Address, Gender from Teacher";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            dataInfoView.DataSource = dt;  
            dataInfoView.AutoResizeColumns();
            con.Dismiss();

        }
        void studentInfo()
        {
            check=new Checker();
            con = new DataBase();
            sql = "select Sid,Student.Name,Student.PhoneNum,Student.Email,Student.Address,TimeDuration as Time,StartingDate as Date,Salary,Description,StudentStatus,TeacherStatus as Status from Info a inner join Student on Student.Id =a.Sid  where Tid='"+check.getId(IDemail)+"'";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            dataInfoView.DataSource = dt;
            dataInfoView.AutoResizeColumns();
            con.Dismiss();
        }
        int teacherAppChecker = 0;
        private void btnTeaApp_Click(object sender, EventArgs e)
        {
            con = new DataBase();
            check = new Checker();
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
            if (check.tableName(IDemail) == "Teacher")
            {
                
                if (dashExpand == false)
                {
                    dashBoardTimer.Start();
                    dashExpand = true;
                }
                else
                {
                    dashBoardTimer.Start();
                    dashExpand = false;
                }
                //btnInfoUpdate.Visible = true;
                //btnInfoAgree.Visible = true;
                //btnInfoDis.Visible = true;
                label27.Visible = true;
                label26.Visible = true;
                Status1.Visible = true;
                Status2.Visible = true;

                label27.Text = "Student Status";

                startingDate.Enabled = false;
                txtDes.Enabled = false;
                txtTime.Enabled = false;
                txtSal.Enabled = false;

                btnInfoSave.Visible = false;
                btnInfoCan.Visible = false;

                /*infoName.Text = "";
                infoQual.Text = "";
                infoPhnNum.Text = "";
                infoEmail.Text = "";
                infoAdd.Text = "";
                startingDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                txtDes.Text = "";
                txtTime.Text = "";
                txtSal.Text = "";
                infoPicture.Image = null;*/
                studentInfo();
            }
            else
            {
                if (dashExpand == true)
                {
                    dashBoardTimer.Start();
                    dashExpand = false;
                }
                btnInfoSave.Visible = false;
                btnInfoCan.Visible = false;
                // btnInfoUpdate.Visible = true;
                //btnInfoAgree.Visible = true;
                //btnInfoDis.Visible = true;
                label27.Visible = true;
                label26.Visible = true;
                Status1.Visible = true;
                Status2.Visible = true;

                Status1.Text = "";
                Status2.Text = "";

                label27.Text = "Teacher Status";

                startingDate.Enabled = false;
                txtDes.Enabled = false;
                txtTime.Enabled = false;
                txtSal.Enabled = false;


                btnRequest.Visible = false;
                infoName.Text = "";
                infoQual.Text = "";
                infoPhnNum.Text = "";
                infoEmail.Text = "";
                infoAdd.Text = "";
                startingDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                txtDes.Text = "";
                txtTime.Text = "";
                txtSal.Text = "";
                teacherAppChecker = 1;
                infoPicture.Visible = true;
                btnTeaApp.BackColor = Color.MediumSeaGreen;
                btnTeaInfo.BackColor = Color.LightSlateGray;
                infoPicture.Image = null;
                teacherAssignInfo();
            }
        }
        void teacherAssignInfo()
        {
            con = new DataBase();
            sql = "select TId,Teacher.Name,Teacher.Qualification as Qual,Teacher.PhoneNum,Teacher.Email,Teacher.Address,TimeDuration as Time,StartingDate as Date,Salary,Description,TeacherStatus,StudentStatus as Status from Info a inner join Teacher on Teacher.Id =a.Tid where Sid='" + check.getId(IDemail) + "'";
            DataTable dt = new DataTable();
            con.GetData(sql).Fill(dt);
            dataInfoView.DataSource = dt;
            dataInfoView.AutoResizeColumns();
            con.Dismiss();
        }
        private void btnTeaInfo_Click(object sender, EventArgs e)
        {
            if (dashExpand == false)
            {
                dashBoardTimer.Start();
                dashExpand = true;
            }
            btnInfoSave.Visible = false;
            btnInfoCan.Visible = false;
            btnInfoUpdate.Visible = false;
            btnInfoAgree.Visible = false;
            btnInfoDis.Visible = false;
            Status1.Text = "";
            Status2.Text = "";
            label27.Visible = false;
            label26.Visible = false;
            Status1.Visible = false;
            Status2.Visible = false;

            startingDate.Enabled = true;
            txtDes.Enabled = true;
            txtTime.Enabled = true;
            txtSal.Enabled = true;

            btnRequest.Visible = true;
            infoName.Text = "";
            infoQual.Text = "";
            infoPhnNum.Text = "";
            infoEmail.Text = "";
            infoAdd.Text = "";
            startingDate.Value= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            txtDes.Text = "";
            txtTime.Text = "";
            txtSal.Text = "";
            teacherAppChecker = 0;
            infoPicture.Visible = false;
            btnTeaInfo.BackColor = Color.MediumSeaGreen;
            btnTeaApp.BackColor = Color.LightSlateGray;
            teacherInfo();
            infoPicture.Image = null;
        }
        private void request_Click(object sender, EventArgs e)
        {
            if (infoName.Text == "" || infoEmail.Text == "" || infoPhnNum.Text == "" || infoQual.Text == "" || infoAdd.Text == "" || txtTime.Text == "" || txtSal.Text == "")
            {
                MessageBox.Show("       Missing Information.       ");
            }
            else
            {
                con = new DataBase();
                check = new Checker();
                if (check.existOrNot(IDemail, infoEmail.Text.ToString())){
                    try
                    {
                        if (txtDes.Text != "")
                        {
                            sql = "insert into Info values ('" + check.getId(IDemail) + "','" + check.getId(infoEmail.Text) + "','" + txtTime.Text + "','" + startingDate.Value.Date + "','" + txtSal.Text + "','Student: " + txtDes.Text + "','Agree','Waiting',getDate(),'0','0')";
                        }
                        else
                        {
                            sql = "insert into Info values ('" + check.getId(IDemail) + "','" + check.getId(infoEmail.Text) + "','" + txtTime.Text + "','" + startingDate.Value.Date + "','" + txtSal.Text + "','" + txtDes.Text + "','Agree','Waiting',getDate(),'0','0')";
                        }
                        con.ExecuteQuery(sql);
                        con.Dismiss();
                        MessageBox.Show("       Teacher request sent successfully.");
                    }
                    catch
                    {
                        MessageBox.Show("       Salary is integer number, nothing else.");
                    }
                }
                else
                {
                    MessageBox.Show("       Your are my already Student.");
                }
            }
        }
        private void dashPanel_Click(object sender, EventArgs e)
        {
            if (sidebarExpand != true)
            {
                sidebarExpand = true;
                sidebarTimer.Start();
            }
        }
        private void dataInfoView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                check = new Checker();
                //int tId=Convert.ToInt32(dataTeacherView.SelectedRows[0].Cells[0].Value.ToString());
                infoName.Text = dataInfoView.SelectedRows[0].Cells[1].Value.ToString();
                infoQual.Text = dataInfoView.SelectedRows[0].Cells[2].Value.ToString();
                infoPhnNum.Text = dataInfoView.SelectedRows[0].Cells[3].Value.ToString();
                infoEmail.Text = dataInfoView.SelectedRows[0].Cells[4].Value.ToString();
                infoAdd.Text = dataInfoView.SelectedRows[0].Cells[5].Value.ToString();
                if (check.tableName(IDemail) == "Teacher")
                {
                    //infoQual.Text = dataTeacherView.SelectedRows[0].Cells[2].Value.ToString();
                    infoPhnNum.Text = dataInfoView.SelectedRows[0].Cells[2].Value.ToString();
                    infoEmail.Text = dataInfoView.SelectedRows[0].Cells[3].Value.ToString();
                    infoAdd.Text = dataInfoView.SelectedRows[0].Cells[4].Value.ToString();
                    txtTime.Text= dataInfoView.SelectedRows[0].Cells[5].Value.ToString();
                    startingDate.Text= dataInfoView.SelectedRows[0].Cells[6].Value.ToString();
                    txtSal.Text= dataInfoView.SelectedRows[0].Cells[7].Value.ToString();
                    txtDes.Text = dataInfoView.SelectedRows[0].Cells[8].Value.ToString();
                    Status1.Text= dataInfoView.SelectedRows[0].Cells[9].Value.ToString();
                    Status2.Text = dataInfoView.SelectedRows[0].Cells[10].Value.ToString();
                }
                else
                {
                    if (teacherAppChecker == 1)
                    {
                        txtTime.Text = dataInfoView.SelectedRows[0].Cells[6].Value.ToString();
                        startingDate.Text = dataInfoView.SelectedRows[0].Cells[7].Value.ToString();
                        txtSal.Text= dataInfoView.SelectedRows[0].Cells[8].Value.ToString();
                        txtDes.Text = dataInfoView.SelectedRows[0].Cells[9].Value.ToString();
                        Status1.Text= dataInfoView.SelectedRows[0].Cells[10].Value.ToString();
                        Status2.Text = dataInfoView.SelectedRows[0].Cells[11].Value.ToString();
                    }
                }
                try
                {
                    if (check.getInfoPicture(infoEmail.Text.ToString()) == "")
                    {
                        if (check.getInfoGender(infoEmail.Text.ToString()) == "Male")
                        {
                            infoPicture.Image = Properties.Resources.userMan;
                        }
                        else if (check.getInfoGender(infoEmail.Text.ToString()) == "Female")
                        {
                            infoPicture.Image = Properties.Resources.userWoman;
                        }
                        else
                        {
                            infoPicture.Image = Properties.Resources.userDefault;
                        }
                    }
                    else
                    {

                        Image image = Image.FromFile(@"" + check.getInfoPicture(infoEmail.Text.ToString()) + "");
                        var ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        var bytes = ms.ToArray();
                        var imageMemoryStream = new MemoryStream(bytes);
                        Image imgFromStream = Image.FromStream(imageMemoryStream);
                        infoPicture.Image = imgFromStream;
                    }
                }
                catch
                {
                    if (check.getInfoGender(infoEmail.Text.ToString()) == "Male")
                    {
                        infoPicture.Image = Properties.Resources.userMan;
                    }
                    else if (check.getInfoGender(infoEmail.Text.ToString()) == "Female")
                    {
                        infoPicture.Image = Properties.Resources.userWoman;
                    }
                    else
                    {
                        infoPicture.Image = Properties.Resources.userDefault;
                    }
                }
            }
            catch
            {
                startingDate.Enabled = false;
                txtDes.Enabled = false;
                txtTime.Enabled = false;
                txtSal.Enabled = false;
                btnInfoSave.Visible = false;
                btnInfoCan.Visible = false;
                infoPicture.Image = null;
                MessageBox.Show("       Empty Cell Click!!!       ");
            }
        }
        private void dataInfoView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataInfoView_CellContentClick(sender, e);
        }

        private void dataInfoView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataInfoView_CellContentClick(sender, e);
        }

        private void btnInfoAgree_Click(object sender, EventArgs e)
        {
            check = new Checker();
            con = new DataBase();
            if (check.tableName(IDemail) == "Teacher")
            {
                sql = "select * from Info where SId = '" + check.getId(infoEmail.Text) + "' and TId = '" + check.getId(IDemail) + "' and  TeacherUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    sql = "update Info set TeacherStatus = 'Agree',TeacherUpCheck='1',StudentUpCheck='0' where SId = '" + check.getId(infoEmail.Text) + "' and TId = '" + check.getId(IDemail) + "'";
                    con.ExecuteQuery(sql);
                    MessageBox.Show("       You have agreed the student request.    ");
                    studentInfo();
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
                con.Dismiss();
            }
            else
            {
                sql = "select * from Info where SId = '" + check.getId(IDemail) + "' and TId = '" + check.getId(infoEmail.Text) + "' and StudentUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    sql = "update Info set StudentStatus = 'Agree', TeacherUpCheck='0',StudentUpCheck='1' where SId = '" + check.getId(IDemail) + "' and TId = '" + check.getId(infoEmail.Text) + "'";
                    con.ExecuteQuery(sql);
                    MessageBox.Show("       You have agreed the teacher request.    ");
                    
                    teacherAssignInfo();
                    //btnTeaApp_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
                con.Dismiss();
            }
        }
        string strDate, des, time, sal; 
        private void btnInfoUpdate_Click(object sender, EventArgs e)
        {
            startingDate.Enabled = true;
            txtDes.Enabled = true;
            txtTime.Enabled = true;
            txtSal.Enabled = true;
            strDate=startingDate.Text;
            des=txtDes.Text;
            time=txtTime.Text;
            sal=txtSal.Text;
            btnInfoSave.Visible = true;
            btnInfoCan.Visible = true;
            btnInfoAgree.Visible = false;
            btnInfoDis.Visible = false;
            btnInfoUpdate.Visible = false;
        }

        private void btnInfoCan_Click(object sender, EventArgs e)
        {
            startingDate.Text=strDate;
            txtDes.Text=des;
            txtTime.Text=time;
            txtSal.Text=sal;
            startingDate.Enabled = false;
            txtDes.Enabled = false;
            txtTime.Enabled = false;
            txtSal.Enabled = false;
            btnInfoSave.Visible = false;
            btnInfoCan.Visible = false;
            btnInfoAgree.Visible = true;
            btnInfoDis.Visible = true;
            btnInfoUpdate.Visible = true;
        }

        private void btnInfoDis_Click(object sender, EventArgs e)
        {
            con = new DataBase();
            check = new Checker();
            if (check.tableName(IDemail) == "Teacher")
            {
                sql = "select * from Info where SId = '" + check.getId(infoEmail.Text) + "' and TId = '" + check.getId(IDemail) + "' and TeacherUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    sql = "update Info set TeacherStatus = 'Disagree',TeacherUpCheck='1',StudentUpCheck='0' where SId = '" + check.getId(infoEmail.Text) + "' and TId = '" + check.getId(IDemail) + "'";
                    con.ExecuteQuery(sql);
                    MessageBox.Show("       You have disagreed the student request.    ");
                    studentInfo();
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
            }
            else
            {
                sql = "select * from Info where SId = '" + check.getId(IDemail) + "' and TId = '" + check.getId(infoEmail.Text) + "' and StudentUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    sql = "update Info set StudentStatus = 'Disagree', TeacherUpCheck='0',StudentUpCheck='1' where SId = '" + check.getId(IDemail) + "' and TId = '" + check.getId(infoEmail.Text) + "'";
                    con.ExecuteQuery(sql);
                    MessageBox.Show("       You have disagreed the teacher request.    ");

                    teacherAssignInfo();
                    //btnTeaApp_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
            }
        }

        private void btnInfoSave_Click(object sender, EventArgs e)
        {
            con = new DataBase();
            check = new Checker();
            if(infoAdd.Text == "" || txtTime.Text == "" || txtSal.Text == "")
            {
                MessageBox.Show("       Missing Informattion.       ");
            }
            else if (check.tableName(IDemail) == "Teacher")
            {
                sql = "select * from Info where SId = '" + check.getId(infoEmail.Text) + "' and TId = '" + check.getId(IDemail) + "' and TeacherUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (txtDes.Text != "")
                    {
                        sql1 = "update Info set TeacherUpCheck='1',StudentUpCheck='0', TimeDuration= '" + txtTime.Text + "' , StartingDate= '" + startingDate.Value.Date + "', Salary= '" + txtSal.Text + "' , Description= 'Teacher: " + txtDes.Text + "' , TeacherStatus= 'Edited' where SId='" + check.getId(infoEmail.Text) + "' and TId='" + check.getId(IDemail) + "'";
                    }
                    else
                    {
                        sql1 = "update Info set TeacherUpCheck='1',StudentUpCheck='0' ,TimeDuration= '" + txtTime.Text + "' , StartingDate= '" + startingDate.Value.Date + "', Salary= '" + txtSal.Text + "' , Description= '" + txtDes.Text + "' , TeacherStatus= 'Edited' where SId='" + check.getId(infoEmail.Text) + "' and TId='" + check.getId(IDemail) + "'";
                    }
                    MessageBox.Show("       Information Status Updated Successfully.       ");
                    con.ExecuteQuery(sql1);

                    btnInfoCan.Visible = false;
                    btnInfoSave.Visible = false;
                    btnInfoUpdate.Visible = true;
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;


                    startingDate.Enabled = false;
                    txtDes.Enabled = false;
                    txtTime.Enabled = false;
                    txtSal.Enabled = false;

                    con.Dismiss();
                    studentInfo();
                    //teacherAssignInfo();
                    //btnTeaApp_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
            }
            else
            {
                sql = "select * from Info where SId = '" + check.getId(IDemail) + "' and TId = '" + check.getId(infoEmail.Text) + "' and StudentUpCheck='0'";
                DataTable dt1 = new DataTable();
                con.GetData(sql).Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (txtDes.Text != "")
                    {
                        sql1 = "update Info set TeacherUpCheck='0',StudentUpCheck='1' , TimeDuration= '" + txtTime.Text + "' , StartingDate= '" + startingDate.Value.Date + "', Salary= '" + txtSal.Text + "' , Description= 'Student: " + txtDes.Text + "' , StudentStatus= 'Edited' where SId='" + check.getId(IDemail) + "' and TId='" + check.getId(infoEmail.Text) + "'";
                    }
                    else
                    {
                        sql1 = "update Info set TeacherUpCheck='0',StudentUpCheck='1' , TimeDuration= '" + txtTime.Text + "' , StartingDate= '" + startingDate.Value.Date + "', Salary= '" + txtSal.Text + "' , Description= '" + txtDes.Text + "' , StudentStatus= 'Edited' where SId='" + check.getId(IDemail) + "' and TId='" + check.getId(infoEmail.Text) + "'";
                    }
                    MessageBox.Show("       Information Status Updated Successfully.       ");
                    con.ExecuteQuery(sql1);

                    btnInfoCan.Visible = false;
                    btnInfoSave.Visible = false;
                    btnInfoUpdate.Visible = true;
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;
                    startingDate.Enabled = false;
                    txtDes.Enabled = false;
                    txtTime.Enabled = false;
                    txtSal.Enabled = false;

                    con.Dismiss();
                    teacherAssignInfo();
                    //btnTeaApp_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("       You have already edited once.        ");
                }
            }
        }

        private void infoEmail_TextChanged(object sender, EventArgs e)
        {
            check = new Checker();
            if (check.tableName(IDemail) == "Student")
            {
                if (infoEmail.Text != "" && teacherAppChecker == 1)
                {
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;
                    btnInfoUpdate.Visible = true;

                }
                else
                {

                    btnInfoAgree.Visible = false;
                    btnInfoDis.Visible = false;
                    btnInfoUpdate.Visible = false;
                }
            }
            else
            {
                if (infoEmail.Text != "")
                {
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;
                    btnInfoUpdate.Visible = true;

                }
                else
                {

                    btnInfoAgree.Visible = false;
                    btnInfoDis.Visible = false;
                    btnInfoUpdate.Visible = false;
                }
            }

        }

        private void infoEmail_ModifiedChanged(object sender, EventArgs e)
        {
            check = new Checker();
            if (check.tableName(IDemail) == "Student")
            {
                if (infoEmail.Text != "" && teacherAppChecker == 1)
                {
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;
                    btnInfoUpdate.Visible = true;

                }
                else
                {

                    btnInfoAgree.Visible = false;
                    btnInfoDis.Visible = false;
                    btnInfoUpdate.Visible = false;
                }
            }
            else
            {
                if (infoEmail.Text != "")
                {
                    btnInfoAgree.Visible = true;
                    btnInfoDis.Visible = true;
                    btnInfoUpdate.Visible = true;

                }
                else
                {

                    btnInfoAgree.Visible = false;
                    btnInfoDis.Visible = false;
                    btnInfoUpdate.Visible = false;
                }
            }
        }

        bool sidebarExpand,dashExpand=true;

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Left -= 37;
                if (sidebar.Left ==-703)
                {
                    sidebarTimer.Stop();
                    panel8.BackColor = Color.FromArgb(14, 42, 71);
                }
            }
            else
            {
                panel8.BackColor = Color.Transparent;
                sidebar.Left += 37;
                if (sidebar.Left >=0)
                {
                    sidebarTimer.Stop();
                }
            }
        }
        private void dashBoard_Tick(object sender, EventArgs e)
        {
            if (dashExpand)
            {
                dashPanel.Left -= 15;
                if (dashPanel.Left == 135)
                {
                    dashBoardTimer.Stop();
                }
            }
            else
            {
                dashPanel.Left += 15;
                if (dashPanel.Left == 360)
                {
                    dashBoardTimer.Stop();
                }
            }
        }
        void SearchData(string searchValue)
        {
            check = new Checker();
            con = new DataBase();
            if (check.tableName(IDemail) == "Student")
            {
                if (teacherAppChecker == 1)
                {
                    sql= "select TId,Teacher.Name,Teacher.Qualification as Qual,Teacher.PhoneNum,Teacher.Email,Teacher.Address,TimeDuration as Time,StartingDate as Date,Salary,Description,TeacherStatus,StudentStatus as Status from Info a inner join Teacher on Teacher.Id =a.Tid where Sid='" + check.getId(IDemail) + "' and (TId like '%"+searchValue+ "%' or Teacher.Name like '%"+searchValue+ "%' or Teacher.Qualification like '%" + searchValue+ "%' or Teacher.PhoneNum like '%" + searchValue+ "%' or Teacher.Email like '%" + searchValue+ "%' or Teacher.Address like '%" + searchValue+ "%' or TimeDuration like '%" + searchValue+ "%' or StartingDate like '%" + searchValue+ "%' or Salary like '%" + searchValue+ "%' or Description like '%" + searchValue+ "%' or TeacherStatus like '%" + searchValue+"%' or StudentStatus like '%"+searchValue+"%')";
                    DataTable dt = new DataTable();
                    con.GetData(sql).Fill(dt);
                    dataInfoView.DataSource = dt;
                    con.Dismiss();
                }
                else
                {
                    sql = "select Id, Name, Qualification, PhoneNum, Email, Address, Gender FROM Teacher WHERE Id LIKE '%" + searchValue + "%' OR Name LIKE '%" + searchValue + "%' OR Qualification LIKE '%" + searchValue + "%' OR PhoneNum LIKE '%" + searchValue + "%' OR Email LIKE '%" + searchValue + "%' OR Address LIKE '%" + searchValue + "%' OR Gender LIKE '%" + searchValue + "%'";
                    DataTable dt = new DataTable();
                    con.GetData(sql).Fill(dt);
                    dataInfoView.DataSource = dt;
                    con.Dismiss();
                }
                
            }
            else
            {
                sql = "select Sid,Student.Name,Student.PhoneNum,Student.Email,Student.Address,TimeDuration as Time,StartingDate as Date,Salary,Description,StudentStatus,TeacherStatus as Status from Info a inner join Student on Student.Id =a.Sid  where Tid='" + check.getId(IDemail) + "' and (SId like '%"+searchValue+"%' or Student.Name like '%"+searchValue+"%' or Student.PhoneNum like '%"+searchValue+"%' or Student.Email like '%"+searchValue+"%' or Student.Address like '%"+searchValue+"%' or TimeDuration like '%"+searchValue+"%' or StartingDate like '%"+searchValue+"%' or Salary like '%"+searchValue+"%' or Description like '%"+searchValue+"%' or StudentStatus like '%"+searchValue+"%' or TeacherStatus like '%"+searchValue+"%')";
                DataTable dt = new DataTable();
                con.GetData(sql).Fill(dt);
                dataInfoView.DataSource = dt;
                con.Dismiss();
            }
        }
    }
}
