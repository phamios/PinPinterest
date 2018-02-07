using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinterestTest
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://developers.pinterest.com/tools/access_token/?");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String AccessToken = null;
            String appID = null;
            String appSecret = null;
        }



        public void saveFileConfig( String filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                sw.WriteLine("Hello World!!"); 
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
