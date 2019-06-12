using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Inventory_Management_System
{
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
          




        }

        private void Receipt_Load(object sender, EventArgs e)
        {

        }

        private void print_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            try
            {
                printPreviewDialog1.ShowDialog();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        Bitmap memoryImage;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);

        }
        private void CaptureScreen()
        {
            Graphics myGraphics = printpanel.CreateGraphics();
            Size s = printpanel.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            Point screenLoc = PointToScreen(printpanel.Location); // Get the location of the Panel in Screen Coordinates
            memoryGraphics.CopyFromScreen(screenLoc.X, screenLoc.Y, 0, 0, s);
        }

       
    }
}
