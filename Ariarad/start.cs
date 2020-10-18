using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Management;
using System.IO;

namespace Ariarad
{
    public partial class start : Form
    {
        private OleDbConnection con;
        public start()
        {
            InitializeComponent();
            string db_config = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_config.txt"));
            con = new OleDbConnection(db_config);

        }

        public string GetCPUId()
        {
            string cpuInfo = String.Empty;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementObject obj in objCol)
            {
                if (cpuInfo == String.Empty)
                {
                    // only return cpuInfo from first CPU
                    cpuInfo = obj.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }

        public void Reg_HardWare_Id()
        {
            //// There is a hard table in database that store the hardware id of a computer and the application will only run if the hardware id is the same as the one in DB

            int a = 0;
            /// get number of rows in hard table.
            OleDbCommand camd = new OleDbCommand("SELECT COUNT(*) FROM hard", con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                a = (int)camd.ExecuteScalar();
                con.Close();
            }
            Console.WriteLine(a.ToString());
            if (a > 0)
            {
                int i = 0;
                OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM hard WHERE hardware=@hard", con);
                cmd.Parameters.AddWithValue("@hard", GetCPUId().ToString());

                Console.WriteLine(GetCPUId().ToString());

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    i = (int)cmd.ExecuteScalar();
                }
                con.Close();

                if (i > 0)
                {
                    Pass_picture.Enabled = true;
                    label1.Text = " Registered Hardware ID ";
                }
                else
                {
                    Pass_picture.Enabled = false;
                    label1.Text = " Unregistered Hardware ID ";
                    MessageBox.Show("شما مجاز به استفاده از این برنامه نمیباشید", "خطا", MessageBoxButtons.OK);
                    
                }
            }
            else if (a == 0)
            {
            
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO hard (hardware) VALUES ('" + GetCPUId().ToString() + "')";
                OleDbCommand myCommand = new OleDbCommand();
                myCommand.CommandText = query;
                myCommand.Connection = con;
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Registered to Hardware ID : " + GetCPUId().ToString());
                label1.Text = " Registered Hardware ID ";
                Pass_picture.Enabled = true;

                con.Close();
            }
        }




        private void pass_lock(object sender, EventArgs e)
        {
            Main x = new Main();
            this.Hide();
            x.Show();
            

        }

        private void start_Load(object sender, EventArgs e)
        {
             Reg_HardWare_Id();
            ///  comment top code and uncomment the bottom line code to disable the application lock
            /// Pass_picture.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
