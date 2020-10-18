using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace Ariarad
{
    public partial class card_validation : Form
    {
        private OleDbConnection con;
        public card_validation()
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

            label2.Text = pc.GetYear(DateTime.Now) + "/" + maha + "/" + ruza;

        }
        private void timer1_Tick(object sender, EventArgs e)
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
                label3.Text = strResult;
                if (strResult == "00000000" )
                {
                     label1.Text= "خطا";
                     label5.Text = "";
                     this.BackColor = Color.DarkCyan;
                     goto endu;
                }
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
                    label5.Text = moshtari[1].ToString();

                        int a;
                        a = Convert.ToInt32(moshtari[8].ToString());
                        label1.Text = a.ToString() + "  نوبت ";
                        tedad =  "  نوبت " + a.ToString()  ;
                        this.BackColor = Color.LightGreen;
                        if (a <= 0)
                        {

                            this.BackColor = Color.Red;
                            label1.Text = " بی اعتبار ";
                            goto endu;
                        }


                    string[] word = label2.Text.Split('/');
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
                        this.BackColor = Color.LightGreen;
                        label1.Text = words[1] + "/" + words[2] + " - " + tedad;
                        



                    }
                    else if (sa <= engh)
                    {

                        this.BackColor = Color.Red;
                        label1.Text = " بی اعتبار ";

                    }

                }
                else
                {
                    this.BackColor = Color.Red;
                    label1.Text = " بی اعتبار ";
                    label5.Text = "";



                }
            endu:
                con.Close();
                MyClass.MiFareAPI.MF_InitComm("COM4", 9600);



            }
            MyClass.MiFareAPI.MF_ExitComm();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main f1 = new Main();
            f1.faal();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            refdate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
