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
    public partial class AddItem : Form
    {
        DataConnection db = new DataConnection();
        public AddItem()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (n_code.Text=="" || n_name.Text==""||  n_price.Text=="" || n_stock.Text=="")
            {
                MessageBox.Show("You have to fill up all the information!");
            }
            else
            {
                string new_item_code = n_code.Text.ToString();
                string new_item_name = n_name.Text.ToString();
                string new_item_model = n_model.Text.ToString();
                string new_item_company = n_company.Text.ToString();
                double new_item_price = Double.Parse(n_price.Text.ToString());
                int new_item_quantity = int.Parse(n_stock.Text.ToString());
                try
                {
                    string query = "insert into Items (ItemCode, ItemName, Model, Company, Price, Stock) values ('" + new_item_code + "','" + new_item_name + "','" + new_item_model + "','" + new_item_company + "','" + new_item_price + "','" + new_item_quantity + "')";
                    db.ExecuteQuery(query);
                    MessageBox.Show("Item Added Successfully");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something Went Wrong");
                }



            }

            


        }
    }
}
