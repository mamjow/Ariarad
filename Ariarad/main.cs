using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace Ariarad
{
    public partial class Main : Form
    {
        private OleDbConnection con;



        public Main()
        {
            InitializeComponent();
            string db_config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_config.txt"));
            con = new OleDbConnection(db_config);

        }
        public static void ClearBuffer()
        {
            for (sbyte i = 0; i < 63; i++)
            {
                MyClass.MiFare.databuffer[i] = 0;
            }
            for (int i = 0; i < 5; i++)
            {
                MyClass.MiFare.cardSN[i] = 0;
            }
        }
        public void faal()
        {
           
            if (this.Height == 504)
            {
                this.Height = 600;
                timerchek.Enabled = true;
                button13.Text = " غیر فعال ";
                this.CenterToScreen(); 

            }
            else if (this.Height == 600)
            {
                this.Height = 504;
                timerchek.Enabled = false;
                button13.Text = " فعال ";
                this.CenterToScreen(); 
            }

           
        }
        public void kartkhata()
        {
            // sedaye khataye 
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 1);
            MyClass.MiFareAPI.MF_ControlBuzzer(10, 15);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 0);
            MyClass.MiFareAPI.MF_ExitComm();
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 1);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 0);
            MyClass.MiFareAPI.MF_ExitComm();
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 1);
            MyClass.MiFareAPI.MF_ControlBuzzer(10, 15);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 0);
            MyClass.MiFareAPI.MF_ExitComm();
            this.BackColor = Color.Red;
        }
        private void addreshte()
        {
            comboBox2.Items.Clear();
            con.Open();
            OleDbCommand myCommand = new OleDbCommand("SELECT * FROM reshte ", con);
            OleDbDataAdapter ad = new OleDbDataAdapter(myCommand);
            DataTable table = new DataTable();
            ad.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                comboBox2.Items.Add(row["reshte"].ToString());

            }
            con.Close();
        }
        private void loadreshte()
        {
            comboreshte.Items.Clear();
            con.Open();
            OleDbCommand myCommand = new OleDbCommand("SELECT * FROM reshte ", con);
            OleDbDataAdapter ad = new OleDbDataAdapter(myCommand);
            DataTable table = new DataTable();
            ad.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                comboreshte.Items.Add(row["reshte"].ToString());

            }
            con.Close();
        }
        private void loadmorabi()
        {
            combomorabi.Items.Clear();
            con.Open();
            OleDbCommand myCommand = new OleDbCommand("SELECT * FROM morabi WHERE reshte = '" + comboreshte.SelectedItem.ToString() + "' ", con);
            OleDbDataAdapter ad = new OleDbDataAdapter(myCommand);
            DataTable table = new DataTable();
            ad.Fill(table);
            combomorabi.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                combomorabi.Items.Add(row["morabi"].ToString());

            }
            con.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 504;
            //timerchek.Enabled = true;
            // TODO: This line of code loads data into the 'dbaDataSet.amar' table. You can move, or remove it, as needed.
            this.amarTableAdapter.Fill(this.dbaDataSet.amar);
            // TODO: This line of code loads data into the 'dbaDataSet1.morabi' table. You can move, or remove it, as needed.
            this.morabiTableAdapter.Fill(this.dbaDataSet1.morabi);
            // TODO: This line of code loads data into the 'dbaDataSet.reshte' table. You can move, or remove it, as needed.
            this.reshteTableAdapter.Fill(this.dbaDataSet.reshte);
            // TODO: This line of code loads data into the 'dbaDataSet.rej' table. You can move, or remove it, as needed.
            this.rejTableAdapter.Fill(this.dbaDataSet.rej);
            refdate();
            rejyab();
            comboBox1.SelectedIndex = 0;
            rejid.KeyPress += new KeyPressEventHandler(tkp);
            shbtxt.KeyPress += new KeyPressEventHandler(tkp);
            shshtxt.KeyPress += new KeyPressEventHandler(tkp);
            shmetxt.KeyPress += new KeyPressEventHandler(tkp);
            telltxt.KeyPress += new KeyPressEventHandler(tkp);
            cashtxt.KeyPress += new KeyPressEventHandler(tkp);
            codebox.KeyPress += new KeyPressEventHandler(tkp);
            loadreshte();
            dataGridView3.Columns[0].Visible = false;
            dataGridView4.Columns[0].Visible = false;
            addreshte();
            dataGridView2.Columns[0].Visible = false;
            


        }
        public static void tkp(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;

            }

            if (e.KeyChar < '0' || e.KeyChar > '9')
                e.Handled = true;
        }
        public void arm()
        {

            bool a;
            a = namtxt.Enabled;
            if (a == true)
            {
                a = false;
                namtxt.Enabled = a;
                familtxt.Enabled = a;
                dadtxt.Enabled = a;
                comboBox1.Enabled = a;
                shshtxt.Enabled = a;
                shmetxt.Enabled = a;
                shbtxt.Enabled = a;
                telltxt.Enabled = a;
                dateburntxt.Enabled = a;
                rejdate.Enabled = a;

            }
            else
            {
                a = true;
                namtxt.Enabled = a;
                familtxt.Enabled = a;
                dadtxt.Enabled = a;
                comboBox1.Enabled = a;
                shshtxt.Enabled = a;
                shmetxt.Enabled = a;
                shbtxt.Enabled = a;
                telltxt.Enabled = a;
                dateburntxt.Enabled = a;
                rejdate.Enabled = a;

            }

        }
        public void refdate()
        {
            System.Globalization.PersianCalendar pc = new
            System.Globalization.PersianCalendar();
            int mah = pc.GetMonth(DateTime.Now);
            int ruz = pc.GetDayOfMonth(DateTime.Now);
            string maha = pc.GetMonth(DateTime.Now).ToString();
            string ruza = pc.GetDayOfMonth(DateTime.Now).ToString();
            if (mah < 10)
                maha = "0" + mah.ToString();
            if (ruz < 10)
                ruza = "0" + ruz.ToString();
            rejdate.Text = pc.GetYear(DateTime.Now) + "/" + maha + "/" + ruza;
            datelabel.Text = pc.GetYear(DateTime.Now) + "/" + maha + "/" + ruza;
            label27.Text = monthCalendarX1.GetTodayDateInPersianDateTime().ToLongDateString();
        }
        public void rejyab()
        {
            int i = 0;
            int a = 0;
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM rej", con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                i = (int)cmd.ExecuteScalar();

            }//end if

            con.Close();

            if (i > 0)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT TOP 1 * FROM rej ORDER BY ID DESC", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                a = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
            int sabtcd = a + 1001;
            rejcode.Text = sabtcd.ToString();
            rejcode.Enabled = false;
            dbref();


        }
        public void dbref()
        {
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From rej", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void btmsabt_Click(object sender, EventArgs e)
        {

            OleDbCommand myCommanda = new OleDbCommand("INSERT INTO rej (rejcode, name, famil , dad,sex,shsh,shme,shb,tell,dateburn,daterej) VALUES (@rejcode, @name, @famil , @dad,@sex,@shsh,@shme,@shb,@tell,@dateburn,@daterej)", con);
            myCommanda.Parameters.AddWithValue("@rejcode", rejcode.Text);
            myCommanda.Parameters.AddWithValue("@name", namtxt.Text);
            myCommanda.Parameters.AddWithValue("@famil", familtxt.Text);
            myCommanda.Parameters.AddWithValue("@dad", dadtxt.Text);
            myCommanda.Parameters.AddWithValue("@sex", comboBox1.SelectedItem.ToString());
            myCommanda.Parameters.AddWithValue("@shsh", shshtxt.Text);
            myCommanda.Parameters.AddWithValue("@shme", shmetxt.Text);
            myCommanda.Parameters.AddWithValue("@shb", shbtxt.Text);
            myCommanda.Parameters.AddWithValue("@tell", telltxt.Text);
            myCommanda.Parameters.AddWithValue("@dateburn", dateburntxt.Text);
            myCommanda.Parameters.AddWithValue("@daterej", rejdate.Text);
            con.Open();
            myCommanda.ExecuteNonQuery();
            con.Close();

            btmcncl_Click(sender, e);
            dbref();
            refdate();
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            namtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            familtxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dadtxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            shshtxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            shmetxt.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            shbtxt.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            telltxt.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            dateburntxt.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            rejdate.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            rejcode.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            btmedit.Visible = true;
            namtxt.Enabled = true;
            btmsabt.Enabled = false;
            arm();
        }

        private void btmedit_Click(object sender, EventArgs e)
        {
            namtxt.Enabled = false;
            arm();
            btmedit.Visible = false;
            btmsave.Visible = true;
            btmdel.Visible = true;
        }

        private void btmsave_Click(object sender, EventArgs e)
        {
            OleDbCommand myCommanda = new OleDbCommand("UPDATE rej SET name=@name, famil=@famil , dad=@dad , sex=@sex , shsh=@shsh , shme=@shme , shb=@shb , tell=@tell , dateburn=@dateburn , daterej=@daterej WHERE rejcode=@rejcode", con);

            myCommanda.Parameters.AddWithValue("@name", namtxt.Text);
            myCommanda.Parameters.AddWithValue("@famil", familtxt.Text);
            myCommanda.Parameters.AddWithValue("@dad", dadtxt.Text);
            myCommanda.Parameters.AddWithValue("@sex", comboBox1.SelectedItem.ToString());
            myCommanda.Parameters.AddWithValue("@shsh", shshtxt.Text);
            myCommanda.Parameters.AddWithValue("@shme", shmetxt.Text);
            myCommanda.Parameters.AddWithValue("@shb", shbtxt.Text);
            myCommanda.Parameters.AddWithValue("@tell", telltxt.Text);
            myCommanda.Parameters.AddWithValue("@dateburn", dateburntxt.Text);
            myCommanda.Parameters.AddWithValue("@daterej", rejdate.Text);
            myCommanda.Parameters.AddWithValue("@rejcode", rejcode.Text);
            con.Open();
            myCommanda.ExecuteNonQuery();
            con.Close();

            btmcncl_Click(sender, e);
            dbref();
            MessageBox.Show("اطلاعات با موفقیت به روز شد");

        }

        private void btmcncl_Click(object sender, EventArgs e)
        {
            btmsabt.Enabled = true;
            btmsave.Visible = false;
            btmedit.Visible = false;
            btmdel.Visible = false;
            rejyab();
            namtxt.Text = "";
            familtxt.Text = "";
            dadtxt.Text = "";
            comboBox1.SelectedIndex = 0;
            shshtxt.Text = "";
            shmetxt.Text = "";
            shbtxt.Text = "";
            telltxt.Text = "";
            dateburntxt.Text = "";
            refdate();
            namtxt.Enabled = false;
            arm();
            infotxt.Text = "";
            rejid.Text = "";
            cashtxt.Text = "";
            kart.Text = "";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM rej WHERE rejcode ='" + sbox.Text + "'", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else if (radioButton2.Checked == true)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM rej WHERE tell ='" + sbox.Text + "'", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else if (radioButton3.Checked == true)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM rej WHERE famil ='" + sbox.Text + "'", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            else if (radioButton4.Checked == true)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM rej WHERE shme ='" + sbox.Text + "'", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            if (sbox.Text == "")
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM rej", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

            }
        }





        private void comboreshte_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadmorabi();
        }

        private void btmsabt1_Click(object sender, EventArgs e)
        {
            if (rejid.Text != "" && cashtxt.Text != "" && comboreshte.SelectedItem.ToString() != "")
            {
                int m = 0;
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM rej WHERE rejcode =@code", con);
                cmd.Parameters.AddWithValue("@code", rejid.Text);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    m = (int)cmd.ExecuteScalar();
                }
                con.Close();
                if (m > 0)
                {
                    con.Open();
                    OleDbCommand camd = new OleDbCommand("SELECT * FROM rej WHERE rejcode=@cod", con);
                    camd.Parameters.AddWithValue("@cod", rejid.Text);
                    OleDbDataReader moshtari = camd.ExecuteReader();
                    moshtari.Read();
                    string nam = moshtari[2].ToString() + " " + moshtari[3].ToString();
                    con.Close();
                    DialogResult dialogResult = MessageBox.Show("تمایل دارید  اقا/خانم  " + nam + " در رشته ورزشی ثبت شود ؟ ", "تذکر", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int h = 0;
                        OleDbCommand comd = new OleDbCommand("SELECT COUNT(*) FROM amar WHERE kart=@kart", con);
                        comd.Parameters.AddWithValue("@kart", kart.Text);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                            h = (int)comd.ExecuteScalar();

                        }//end if


                        if (h > 0)
                        {
                            OleDbCommand myCo = new OleDbCommand("UPDATE amar SET kart='انتقال' WHERE kart='" + kart.Text + "'", con);
                            myCo.ExecuteNonQuery();
                        }

                        con.Close();

                        string halat = "0";
                        string ruz = "26";
                        if (radioButton6.Checked == true)
                        {
                            halat = "هر روز";
                            ruz = "26";
                        }
                        else if (radioButton7.Checked == true)
                        {
                            halat = "فرد/زوج";
                            ruz = "12";
                        }

                        OleDbCommand myCommanda = new OleDbCommand("INSERT INTO amar (rejcode, cash, dates, reshte,morabi,info,kart,ruzha,halat) VALUES (@rejcode, @cash, @date, @reshte,@morabi,@info,@krt,@ruza,@hal)", con);
                        myCommanda.Parameters.AddWithValue("@rejcode", rejid.Text);
                        myCommanda.Parameters.AddWithValue("@cash", cashtxt.Text);
                        myCommanda.Parameters.AddWithValue("@date", datelabel.Text);
                        myCommanda.Parameters.AddWithValue("@reshte", comboreshte.SelectedItem.ToString());
                        myCommanda.Parameters.AddWithValue("@morabi", combomorabi.SelectedItem.ToString());
                        myCommanda.Parameters.AddWithValue("@info", infotxt.Text);
                        myCommanda.Parameters.AddWithValue("@krt", kart.Text);
                        myCommanda.Parameters.AddWithValue("@ruza", ruz.ToString());
                        myCommanda.Parameters.AddWithValue("@hal", halat.ToString());
                        con.Open();
                        myCommanda.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("اطلاعات با موفقیت ثبت شد");
                        btmcncl_Click(sender, e);
                        dbref();
                    }
                }
                else
                    MessageBox.Show("کد اشتراک موجود نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("لطفا اطلاعات لازم را پر کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM amar ", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }

        private void filterk_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true || checkBox2.Checked == true || checkBox3.Checked == true || checkBox5.Checked == true)
            {
                string filteru = "";
                if (checkBox1.Checked == true)
                    filteru += "rejcode ='" + codebox.Text + "' AND ";
                if (checkBox2.Checked == true && checkBox4.Checked == true)
                    filteru += "dates BETWEEN'" + datetxt.Text + "' AND '" + tadatetxt.Text + "' AND ";
                if (checkBox2.Checked == true && checkBox4.Checked == false)
                    filteru += "dates ='" + datetxt.Text + "' AND ";
                if (checkBox3.Checked == true)
                    filteru += "reshte ='" + reshtebox.Text + "' AND ";
                if (checkBox5.Checked == true)
                    filteru += "morabi ='" + morabibox.Text + "' AND ";
                filteru = filteru.Remove(filteru.Length - 4);

                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM amar WHERE " + filteru, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private void datetxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            datetxt.Text = monthCalendarX1.GetSelectedDateInPersianDateTime().ToShortDateString();
            char[] del = { '/' };
            string[] tarikh = datetxt.Text.Split(del);
            int mah = Convert.ToInt32(tarikh[1]);
            if (mah < 10)
                tarikh[1] = "0" + mah.ToString();

            int ruz = Convert.ToInt32(tarikh[2]);
            if (ruz < 10)
                tarikh[2] = "0" + ruz.ToString();
            datetxt.Text = tarikh[0] + "/" + tarikh[1] + "/" + tarikh[2];
        }

        private void tadatetxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tadatetxt.Text = monthCalendarX1.GetSelectedDateInPersianDateTime().ToShortDateString();
            char[] del = { '/' };
            string[] tarikh = tadatetxt.Text.Split(del);
            int mah = Convert.ToInt32(tarikh[1]);
            if (mah < 10)
                tarikh[1] = "0" + mah.ToString();

            int ruz = Convert.ToInt32(tarikh[2]);
            if (ruz < 10)
                tarikh[2] = "0" + ruz.ToString();
            tadatetxt.Text = tarikh[0] + "/" + tarikh[1] + "/" + tarikh[2];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int cash = 0;
            string filteru = "";
            if (checkBox1.Checked == true)
                filteru += "rejcode ='" + codebox.Text + "' AND ";
            if (checkBox2.Checked == true && checkBox4.Checked == true)
                filteru += "dates BETWEEN'" + datetxt.Text + "' AND '" + tadatetxt.Text + "' AND ";
            if (checkBox2.Checked == true && checkBox4.Checked == false)
                filteru += "dates ='" + datetxt.Text + "' AND ";
            if (checkBox3.Checked == true)
                filteru += "reshte ='" + reshtebox.Text + "' AND ";
            if (checkBox5.Checked == true)
                filteru += "morabi ='" + morabibox.Text + "' AND ";
            if (filteru != "")
            {
                filteru = filteru.Remove(filteru.Length - 4);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM amar WHERE " + filteru, con);
                DataTable table = new DataTable();
                da.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    cash = cash + Convert.ToInt32(row["cash"]);
                }
                con.Close();
                label20.Text = cash.ToString();
            }

        }

        private void sabt_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("تغییرات اعمال گردد ؟", "  اخطار", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                string query = "UPDATE amar SET rejcode=@rej, cash=@cash, dates=@date, reshte=@reshte,morabi=@morabi , info=@info WHERE ID =@ID";
                cmd.Parameters.AddWithValue("@rej", textBox1.Text);
                cmd.Parameters.AddWithValue("@cash", textBox2.Text);
                cmd.Parameters.AddWithValue("@dates", textBox3.Text);
                cmd.Parameters.AddWithValue("@reshte", textBox4.Text);
                cmd.Parameters.AddWithValue("@morabi", textBox5.Text);
                cmd.Parameters.AddWithValue("@info", textBox6.Text);
                cmd.Parameters.AddWithValue("@ID", label21.Text);
                cmd.CommandText = query;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                button4_Click(sender, e);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void hazf_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("مورد انتخابی حذف گردد ؟", "اخطار", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                OleDbCommand myCommand = new OleDbCommand();
                string query = "DELETE FROM amar WHERE ID =@ID";
                myCommand.Parameters.AddWithValue("@ID", label21.Text);
                myCommand.CommandText = query;
                myCommand.Connection = con;
                con.Open();
                myCommand.ExecuteNonQuery();
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                button4_Click(sender, e);
                label21.Text = "";
            }

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            label21.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
        }

        private void btmaddreshte_Click(object sender, EventArgs e)
        {
            OleDbCommand myCommanda = new OleDbCommand("INSERT INTO reshte (reshte) VALUES (@reshte)", con);
            myCommanda.Parameters.AddWithValue("@reshte", textBox7.Text);
            con.Open();
            myCommanda.ExecuteNonQuery();
            con.Close();

            OleDbDataAdapter da = new OleDbDataAdapter("Select * From reshte", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0].DefaultView;
            addreshte();
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");
            textBox7.Text = "";

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label22.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            btmdelreshte.Enabled = true;
        }

        private void btmdelreshte_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("مورد انتخابی حذف گردد ؟", "اخطار", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                OleDbCommand myCommand = new OleDbCommand();
                string query = "DELETE FROM reshte WHERE ID =@ID";
                myCommand.Parameters.AddWithValue("@ID", label22.Text);
                myCommand.CommandText = query;
                myCommand.Connection = con;
                con.Open();
                myCommand.ExecuteNonQuery();
                con.Close();
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From reshte", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView3.DataSource = ds.Tables[0].DefaultView;
                label22.Text = "";
                btmaddreshte.Enabled = true;
                btmdelreshte.Enabled = false;
            }
            addreshte();
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            label23.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("مورد انتخابی حذف گردد ؟", "اخطار", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                OleDbCommand myCommand = new OleDbCommand();
                string query = "DELETE FROM morabi WHERE ID =@ID";
                myCommand.Parameters.AddWithValue("@ID", label23.Text);
                myCommand.CommandText = query;
                myCommand.Connection = con;
                con.Open();
                myCommand.ExecuteNonQuery();
                con.Close();
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From morabi", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView4.DataSource = ds.Tables[0].DefaultView;
                label23.Text = "";
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbCommand myCommanda = new OleDbCommand("INSERT INTO morabi (reshte,morabi) VALUES (@reshte ,@morabi)", con);
            myCommanda.Parameters.AddWithValue("@reshte", comboBox2.SelectedItem.ToString());
            myCommanda.Parameters.AddWithValue("@morabi", textBox8.Text);
            con.Open();
            myCommanda.ExecuteNonQuery();
            con.Close();

            OleDbDataAdapter da = new OleDbDataAdapter("Select * From morabi", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0].DefaultView;
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");
            textBox8.Text = "";
            button2.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            btmaddreshte.Enabled = true;
            btmdelreshte.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            button2.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                codebox.Enabled = true;
            else if (checkBox1.Checked == false)
            {
                codebox.Enabled = false;
                codebox.Text = "";
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                datetxt.Enabled = true;
                tadatetxt.Enabled = true;
                checkBox4.Enabled = true;

            }
            else if (checkBox2.Checked == false)
            {
                datetxt.Enabled = false;
                datetxt.Text = "";
                tadatetxt.Text = "";
                tadatetxt.Enabled = false;
                checkBox4.Enabled = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                reshtebox.Enabled = true;
            else if (checkBox3.Checked == false)
            {
                reshtebox.Text = "";
                reshtebox.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
                morabibox.Enabled = true;
            else if (checkBox5.Checked == false)
            {
                morabibox.Text = "";
                morabibox.Enabled = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            int i = 0;
            try
            {

                if ((usert.Text == string.Empty) || (passt.Text == string.Empty))
                {
                    MessageBox.Show("لطفا نام کاربری و رمز عبور را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM admin WHERE admin =@UserName AND  pass =@Password", con);
                cmd.Parameters.AddWithValue("@UserName", usert.Text);
                cmd.Parameters.AddWithValue("@Password", passt.Text);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    i = (int)cmd.ExecuteScalar();

                }//end if

                con.Close();

                if (i > 0)
                {
                    timer1.Start();
                    usert.Text = "";
                    passt.Text = "";
                }
                else
                    MessageBox.Show("نام کاربری و یا رمز عبور اشتباه می باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                i = 0;
                con.Close();
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //panele modiriat ghesmate admin bala baere
            int x = panel1.Height;
            x = x - 10;
            panel1.Height = x;
            if (x == 11)
            {
                panel1.Visible = false;
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //panele modiriat ghesmate admin paeen biad
            int x = panel1.Height;
            panel1.Visible = true;
            x = x + 10;
            panel1.Height = x;
            if (x == 231)
            {
                timer2.Stop();

            }
        }

        private void passt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button9_Click(sender, e);
            }
        }

        private void btmdel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("مورد انتخابی حذف گردد ؟", "اخطار", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                OleDbCommand myCommanda = new OleDbCommand("DELETE FROM rej WHERE rejcode=@rejcode", con);
                myCommanda.Parameters.AddWithValue("@rejcode", rejcode.Text);
                con.Open();
                myCommanda.ExecuteNonQuery();
                con.Close();
                btmcncl_Click(sender, e);
                dbref();

            }
            btmcncl_Click(sender, e);
        }



        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }




        private void timer3_Tick(object sender, EventArgs e)
        {
            kart.Text = "";
            int BlouckNumber = 1;
            MyClass.MiFareAPI.MF_GetDeviceSNR(10, ref MyClass.MiFare.Daddress);
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            MyClass.MiFareAPI.MF_ControlLED(10, 1, 1);
            MyClass.MiFareAPI.MF_ControlBuzzer(10, 10);
            MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]);
            MyClass.MiFareAPI.MF_Anticoll(10, ref MyClass.MiFare.cardSN[0]);
            MyClass.MiFareAPI.MF_ControlLED(10, 0, 0);
            MyClass.MiFareAPI.MF_GetDLL_Ver(ref  MyClass.MiFare.DLL_version[0]).ToString();
            string strResult = null;
            for (byte i = 0; i < 4; i++)
            {
                string a = MyClass.MiFare.cardSN[i].ToString("X");
                if (a.Length == 1)
                    a = 0 + a;
                strResult += a;
            }

            if (MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]).ToString() == "0")
            {
                kart.Text = strResult;
                timer3.Enabled = false;


            }

            MyClass.MiFareAPI.MF_ExitComm();

        }

        private void timerchek_Tick(object sender, EventArgs e)
        {
            int BlouckNumber = 1;
            MyClass.MiFareAPI.MF_GetDeviceSNR(10, ref MyClass.MiFare.Daddress);
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            ClearBuffer();
            MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]);

            //kart.Text = strResult;
            if (MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]).ToString() == "0")
            {
                //MyClass.MiFareAPI.MF_GetDeviceSNR(10, ref MyClass.MiFare.Daddress);
                //MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
                MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]);
                MyClass.MiFareAPI.MF_Anticoll(10, ref MyClass.MiFare.cardSN[0]);
                string strResult = null;
                for (byte i = 0; i < 4; i++)
                {
                    string a = MyClass.MiFare.cardSN[i].ToString("X");
                    if (a.Length == 1)
                        a = 0 + a;
                    strResult += a;
                }
                //sbox.Text = strResult.ToString();
                MyClass.MiFareAPI.MF_ExitComm();
                int h = 0;
                OleDbCommand comd = new OleDbCommand("SELECT COUNT(*) FROM amar WHERE kart=@kart", con);
                comd.Parameters.AddWithValue("@kart", strResult);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    h = (int)comd.ExecuteScalar();
                }//end if
                if (h > 0)
                {
                    OleDbCommand myCo = new OleDbCommand("SELECT * FROM amar WHERE kart='" + strResult + "'", con);
                    OleDbDataReader moshtari = myCo.ExecuteReader();
                    moshtari.Read();
                    label28.Text = moshtari[1].ToString();
                    label29.Text = moshtari[4].ToString();
                    label30.Text = moshtari[5].ToString();
                    //label28.Text = moshtari[9].ToString();
                    //if (moshtari[9].ToString() == "True")
                    //{
                    string[] word = datelabel.Text.Split('/');
                    string[] words = moshtari[3].ToString().Split('/');
                    words[1] = (Convert.ToInt32(words[1]) + 1).ToString();
                    if (words[1].ToString().Length == 1)
                    {
                        words[1] = "0" + words[1].ToString();
                    }
                    if (words[1] == "13")
                    {
                        words[1] = "01";
                        words[0] = (Convert.ToInt32(words[0]) + 1).ToString();
                    }

                    string sabt = words[0] + words[1] + words[2];
                    string engheza = word[0] + word[1] + word[2];
                    label32.Text = words[0] + "/" + words[1] + "/" + words[2];
                    int engh = Convert.ToInt32(engheza);
                    int sa = Convert.ToInt32(sabt);

                        int a;
                        a = Convert.ToInt32(moshtari[8].ToString()) - 1;
                        label31.Text = a.ToString() + " نوبت باقی مانده";
                        OleDbCommand myc = new OleDbCommand("UPDATE amar SET ruzha='" + a.ToString() + "' WHERE kart='" + strResult + "'", con);
                        myc.ExecuteNonQuery();
                        if (a <= 0)
                        {
                            kartkhata();
                            goto payan;
                        }
                    //}

 
                    // jaye sabt va engheza ro ja beja check kardam ama ok hast faghat engheza tarikh sabt has va sabt tarikh engheza
                    if (sa > engh)
                    {
                        MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
                        MyClass.MiFareAPI.MF_ControlLED(10, 1, 0);
                        MyClass.MiFareAPI.MF_ControlBuzzer(10, 30);
                        MyClass.MiFareAPI.MF_ControlLED(10, 0, 0);
                        MyClass.MiFareAPI.MF_ExitComm();
                        this.BackColor = Color.Green;


                    }
                    else if (sa <= engh)
                    {
                        kartkhata();

                    }

                }
                else
                {
                    kartkhata();
                    label28.Text = "";
                    label29.Text = "";
                    label30.Text = "کارت ثبت نشده است";
                    label31.Text= "";
                    label32.Text= "";

                }
            payan :
                con.Close();
                MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            }
            MyClass.MiFareAPI.MF_ExitComm();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            this.Height = 600;
            faal();
            card_validation f1 = new card_validation();
            f1.ShowDialog();




        }

        private void button13_Click(object sender, EventArgs e)
        {
            faal();
        }

        private void ptintk_Click(object sender, EventArgs e)
        {
            OleDbCommand myCommand = new OleDbCommand("DELETE * FROM report", con);
            con.Open();
            myCommand.ExecuteNonQuery();
            con.Close();
            int rowcount = dataGridView2.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                string[] a = new string[6];
                a[0] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                a[1] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                a[2] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                a[3] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                a[4] = dataGridView2.Rows[i].Cells[5].Value.ToString();
                a[5] = dataGridView2.Rows[i].Cells[6].Value.ToString();
                OleDbCommand myCommanda = new OleDbCommand("INSERT INTO report (rejcod,cash,dates,reshte,morabi,info) VALUES (@cod,@cash,@tarikh,@reshte,@morabi,@info)", con);
                myCommanda.Parameters.AddWithValue("@cod", a[0]);
                myCommanda.Parameters.AddWithValue("@cash", a[1]);
                myCommanda.Parameters.AddWithValue("@tarikh", a[2]);
                myCommanda.Parameters.AddWithValue("@reshte", a[3]);
                myCommanda.Parameters.AddWithValue("@morabi", a[4]);
                myCommanda.Parameters.AddWithValue("@info", a[5]);
                con.Open();
                myCommanda.ExecuteNonQuery();
                con.Close();
            }
            print x = new print();
            x.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (this.Height == 600)
            {
                faal();
            }

            timer3.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
                loadreshte();
        }





       /* private void etebart_Tick(object sender, EventArgs e)
        {
            int BlouckNumber = 1;
            MyClass.MiFareAPI.MF_GetDeviceSNR(10, ref MyClass.MiFare.Daddress);
            MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
            ClearBuffer();
            MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]);

            //kart.Text = strResult;
            if (MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]).ToString() == "0")
            {
                string tedad = "";
                //MyClass.MiFareAPI.MF_GetDeviceSNR(10, ref MyClass.MiFare.Daddress);
                //MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
                MyClass.MiFareAPI.MF_Request(10, Convert.ToInt16(BlouckNumber), ref MyClass.MiFare.cardT[0]);
                MyClass.MiFareAPI.MF_Anticoll(10, ref MyClass.MiFare.cardSN[0]);
                string strResult = null;
                for (byte i = 0; i < 4; i++)
                {
                    string a = MyClass.MiFare.cardSN[i].ToString("X");
                    if (a.Length == 1)
                        a = 0 + a;
                    strResult += a;
                }
                //sbox.Text = strResult.ToString();
                MyClass.MiFareAPI.MF_ExitComm();
                int h = 0;
                OleDbCommand comd = new OleDbCommand("SELECT COUNT(*) FROM amar WHERE kart=@kart", con);
                comd.Parameters.AddWithValue("@kart", strResult);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    h = (int)comd.ExecuteScalar();
                }//end if
                if (h > 0)
                {
                    OleDbCommand myCo = new OleDbCommand("SELECT * FROM amar WHERE kart='" + strResult + "'", con);
                    OleDbDataReader moshtari = myCo.ExecuteReader();
                    moshtari.Read();
                    if (moshtari[9].ToString() == "True")
                    {
                        int a;
                        a = Convert.ToInt32(moshtari[8].ToString());
                        button12.Text = a.ToString() + "  نوبت ";
                        tedad = a.ToString() + "  نوبت ";
                        button12.BackColor = Color.LightGreen;
                        button12.ForeColor = Color.Black;
                        if (a <= 0)
                        {
                            button12.BackColor = Color.Red;
                            button12.Text = " بی اعتبار ";
                            button12.ForeColor = Color.White;
                            goto endu;
                        }
                    }

                    string[] word = datelabel.Text.Split('/');
                    string[] words = moshtari[3].ToString().Split('/');
                    words[1] = (Convert.ToInt32(words[1]) + 1).ToString();
                    if (words[1].ToString().Length == 1)
                    {
                        words[1] = "0" + words[1].ToString();
                    }
                    if (words[1] == "13")
                    {
                        words[1] = "01";
                        words[0] = (Convert.ToInt32(words[0]) + 1).ToString();
                    }
                    string sabt = words[0] + words[1] + words[2];
                    string engheza = word[0] + word[1] + word[2];
                    int engh = Convert.ToInt32(engheza);
                    int sa = Convert.ToInt32(sabt);
                    if (sa > engh)
                    {
                        button12.BackColor = Color.LightGreen;
                        button12.Text = words[1] + "/" + words[2] + Environment.NewLine + tedad;
                        button12.ForeColor = Color.Black;



                    }
                    else if (sa <= engh)
                    {
                        button12.ForeColor = Color.White;
                        button12.BackColor = Color.Red;
                        button12.Text = " بی اعتبار ";

                    }

                }
                else
                {
                    button12.ForeColor = Color.White;
                    button12.BackColor = Color.Red;
                    button12.Text = " بی اعتبار ";



                }
            endu:
                con.Close();
                MyClass.MiFareAPI.MF_InitComm("COM4", 9600);
                timerchek.Enabled = true;
                etebart.Enabled = false;
                cheshmak.Enabled = false;


            }
            MyClass.MiFareAPI.MF_ExitComm();

        }*/


    }
}