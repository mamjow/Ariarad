using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ariarad
{
    public partial class print : Form
    {
        public print()
        {
            InitializeComponent();
        }

        private void print_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbaDataSet.report' table. You can move, or remove it, as needed.
            this.reportTableAdapter.Fill(this.dbaDataSet.report);
            
            // TODO: This line of code loads data into the 'dbaDataSet.amar' table. You can move, or remove it, as needed.
            Main x = new Main();
            
            this.amarTableAdapter.Fill(this.dbaDataSet.amar);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

 


    }
}
