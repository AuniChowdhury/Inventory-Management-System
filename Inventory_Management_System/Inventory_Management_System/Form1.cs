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
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;


namespace Inventory_Management_System
{
    
    public partial class Form1 : Form
    {
        DataConnection db = new DataConnection();
        DataTable dt;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();



            SqlConnection con = new SqlConnection(db.connectstr);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select * from Items";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            displayitems.DataSource = dt;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            string searchitem = searchbox.Text.ToString();
            string query2 = "select * from Items where ItemCode = '" + searchitem + "' or ItemName =  '" + searchitem + "'  or Model =  '" + searchitem + "'  or Company =  '" + searchitem + "'";
            displayitems.DataSource = db.GetDataTable(query2);
        }

       

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(db.connectstr);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select * from Items";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            displayitems.DataSource = dt;
            DataView dv = new DataView(dt);
           
            dv.RowFilter = string.Format("ItemCode Like '%{0}%' or ItemName Like '%{0}%' or Model Like '%{0}%' or Company Like '%{0}%'", searchbox.Text); ;
            displayitems.DataSource = dv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            int rowindex = displayitems.CurrentCell.RowIndex;
            string item_code=displayitems.Rows[rowindex].Cells[0].Value.ToString();
           
            try
            {
                SqlConnection con = new SqlConnection(db.connectstr);
                con.Open();
                string query = "Delete from Items where ItemCode='" + item_code + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            displayitems.Rows.RemoveAt(rowindex);
           

        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(db.connectstr);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select ItemCode as Code, ItemName as Name, Model as Model, Company as Company,Price as Price, Stock as Stock from Items";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
            ExcelLibrary.DataSetHelper.CreateWorkbook("D:\\Items.xls", ds);


        }

        private void addItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddItem to = new AddItem();
            to.Show();

        }

        private void editItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            EditIttem to = new EditIttem();
            to.Show();

        }

        private void viewAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string query = "select * from Items";
            displayitems.DataSource = db.GetDataTable(query);

        }

        private void salesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sales to = new Sales();
            to.Show();

        }

        private void updateStockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Update to = new Inventory_Management_System.Update();
            to.Show();

        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string query = "select * from Items";
            displayitems.DataSource = db.GetDataTable(query);
        }
    }
}
