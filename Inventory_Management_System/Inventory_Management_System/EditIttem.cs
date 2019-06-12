using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventory_Management_System
{
   
    public partial class EditIttem : Form

    {
        DataConnection db = new DataConnection();
        public int count;
        SqlDataReader reader = null;
        public EditIttem()
        {
            InitializeComponent();
        }

     
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            string search = Search_txt.Text.ToString();
            
            SqlConnection con = new SqlConnection(db.connectstr);
            con.Open();
            string query = "select count (*) from Items where ItemCode='" + search + "' or ItemName='" + search + "' or Model='" + search + "' or Company='" + search + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            if(count==0)
            {
                MessageBox.Show("The searched item does not exist");

            }
            else
            {
                string query2 = "select * from Items where ItemCode='" + search + "' or ItemName='" + search + "' or Model='" + search + "' or Company='" + search + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                reader = cmd2.ExecuteReader();
                while(reader.Read())
                {
                    string item_code = reader["ItemCode"].ToString();
                    string item_name= reader["ItemName"].ToString();
                    string model= reader["Model"].ToString();
                    string company= reader["Company"].ToString();
                    double price = double.Parse(reader["Price"].ToString());
                    int stock = Convert.ToInt32(reader["Stock"].ToString());

                    dataGridView1.Rows.Add(item_code, item_name, model, company, price, stock);
                }
               

            }


            


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==6)
            {
                string code = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string model = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string company = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                double price = Double.Parse( dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                int  stock = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());

                string query = "update Items set  ItemName='" + name + "', Model='" + model + "', Company='" + company + "', Price='" + price + "', Stock='" + stock + "' where ItemCode='" + code + "' ";
                try
                {
                    db.ExecuteQuery(query);
                    MessageBox.Show("Item " + code + " Succesfully Updated");
                }catch(SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }




            }
        }

        
    }
}
