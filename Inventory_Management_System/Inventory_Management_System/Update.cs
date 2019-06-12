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
    public partial class Update : Form
    {
        DataConnection db = new DataConnection();
        public int count;

        public Update()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enter_code.Text == "" || enter_quantity.Text == "")
            {
                MessageBox.Show("You must enter code and quantity");
            }
            else
            {
                string code = enter_code.Text.ToString();

                string str = enter_quantity.Text.Trim();
                double num;
                bool isnum = double.TryParse(str, out num);
                if (isnum)
                {
                    int quantity = Convert.ToInt32(enter_quantity.Text);

                    SqlConnection con = new SqlConnection(db.connectstr);
                    string query = "select count(*) from Items where ItemCode='" + code + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("The product code does not exist");
                    }
                    else
                    {

                        try
                        {
                            string query2 = "select Stock from Items where ItemCode='" + code + "'";
                            SqlCommand cmd2 = new SqlCommand(query2, con);
                            int stock = Convert.ToInt32(cmd2.ExecuteScalar()) + quantity;
                            string query3 = "update Items set Stock='" + stock + "' where ItemCode='" + code + "'";
                            SqlCommand cmd3 = new SqlCommand(query3, con);
                            cmd3.ExecuteNonQuery();


                            MessageBox.Show("Updated Successfully");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (enter_code.Text == "" || enter_quantity.Text == "")
            {
                MessageBox.Show("You must enter code and quantity");
            }
            else
            {
                string code = enter_code.Text.ToString();

                string str = enter_quantity.Text.Trim();
                double num;
                bool isnum = double.TryParse(str, out num);
                if (isnum)
                {
                    int quantity = Convert.ToInt32(enter_quantity.Text);

                    SqlConnection con = new SqlConnection(db.connectstr);
                    string query = "select count(*) from Items where ItemCode='" + code + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("The product code does not exist");
                    }
                    else
                    {

                        try
                        {
                            string query2 = "select Stock from Items where ItemCode='" + code + "'";
                            SqlCommand cmd2 = new SqlCommand(query2, con);
                            int stock = Convert.ToInt32(cmd2.ExecuteScalar()) - quantity;
                            string query3 = "update Items set Stock='" + stock + "' where ItemCode='" + code + "'";
                            SqlCommand cmd3 = new SqlCommand(query3, con);
                            cmd3.ExecuteNonQuery();


                            MessageBox.Show("Updated Successfully");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input");
                }
            }
        }
        }
    }
    
         

            

        
    

