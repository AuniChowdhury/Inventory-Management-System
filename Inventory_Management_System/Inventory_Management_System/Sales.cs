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
    public partial class Sales : Form

    {

        DataConnection db = new DataConnection();
        public int count, stock, qty;
        public string name, model, company;
        sales_info sale = new sales_info();

        private void button2_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
           
            dataGridView1.Rows.RemoveAt(rowindex);
            double sum = 0;
            int num_row= dataGridView1.Rows.Count - 1;
           

            for (int i = 0; i < num_row; i++)
            {

                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                total_price.Text = sum.ToString();
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
           int num = dataGridView1.Rows.Count - 1;
             double sum = 0; 
            
            for(int i=0;i < num; i++)
            {

                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                total_price.Text = sum.ToString();
            }



        }

       

        private void total_price_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            string str = paid.Text.Trim();
            double num;
            bool isnum = Double.TryParse(str, out num);
            if (isnum)
            {

                sale.total = Double.Parse(total_price.Text);
                sale.payment = Double.Parse(paid.Text);
                if (sale.payment < sale.total)
                {
                    MessageBox.Show("Insufficient amount");
                }
                else
                {
                    sale.returned = sale.payment - sale.total;
                    int num_row = dataGridView1.Rows.Count - 1;
                    int stock;
                    SqlConnection con = new SqlConnection(db.connectstr);
                    con.Open();
                    for (int i = 0; i < num_row; i++)
                    {
                        int get_qty = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                        string get_code = dataGridView1.Rows[i].Cells[0].Value.ToString();


                        string query = "select Stock from Items where ItemCode='" + get_code + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        stock = Convert.ToInt32(cmd.ExecuteScalar()) - get_qty;
                        string query2 = "update Items set Stock = '" + stock + " ' where ItemCode='" + get_code + "'";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        cmd2.ExecuteNonQuery();




                    }

                    con.Close();


                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        sale.code[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        sale.name_array[i] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        sale.price_array[i] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        sale.qty_array[i] = dataGridView1.Rows[i].Cells[5].Value.ToString();


                    }

                    sale.rownum = dataGridView1.Rows.Count - 1;
                    Receipt to = new Receipt();
                    for (int i=0; i < sale.rownum; i++)
                    {
                        to.dataGridView1.Rows.Add(sale.code[i], sale.name_array[i], sale.price_array[i], sale.qty_array[i]);
                    }
                    to.dataGridView1.Rows.Add("..........", "..........", "..........", "..........");
                    to.dataGridView1.Rows.Add("", "", "Total", sale.total.ToString());
                    to.dataGridView1.Rows.Add("", "", "Paid", sale.payment.ToString());
                    to.dataGridView1.Rows.Add("", "", "Returned", sale.returned.ToString());

                    string today = DateTime.Today.ToShortDateString();
                    to.date.Text = today;
                  
                    to.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Wrong Input");

            }

        }

        private void enter_code_TextChanged(object sender, EventArgs e)
        {

        }

        double price, qty_price;
        public Sales()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(db.connectstr);
            con.Open();
            string query = "Select ItemCode, ItemName from Items";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                collection.Add(reader[0].ToString());
                collection.Add(reader[1].ToString());

            }
            enter_code.AutoCompleteCustomSource = collection;
            con.Close();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enter_code.Text == "" || enter_qty.Text == "")
            {

                MessageBox.Show("Please enter the required information");
            }
            else
            {
                string str = enter_qty.Text.Trim();
                double num;
                bool isnum = Double.TryParse(str, out num);
                if (isnum)
                {

                    string code = enter_code.Text.ToString();
                    int qty = Convert.ToInt32(enter_qty.Text);
                    SqlConnection con = new SqlConnection(db.connectstr);
                    string query = "select count(*) from Items where ItemCode='" + code + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("The product code does not exist");
                        con.Close();
                    }
                    else
                    {

                        string query2 = "select ItemName,Model,Company,Price ,Stock from Items where ItemCode='" + code + "'";

                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        SqlDataReader reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            name = reader2["ItemName"].ToString();
                            model = reader2["Model"].ToString();
                            company = reader2["Company"].ToString();
                            price = Double.Parse(reader2["Price"].ToString());
                            stock = Convert.ToInt32(reader2["Stock"].ToString());






                        }
                        if (qty <= stock)
                        {

                            qty_price = price * qty;

                            string[] row = new string[] { code.ToString(), name.ToString(), model.ToString(), company.ToString(), price.ToString(), qty.ToString(), qty_price.ToString() };
                            dataGridView1.Rows.Add(row);


                            con.Close();

                            int num_row = dataGridView1.Rows.Count - 1;
                            double sum = 0;

                            for (int i = 0; i < num_row; i++)
                            {

                                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                                total_price.Text = sum.ToString();
                            }
                        }else

                        {
                            MessageBox.Show("Desired quantity not available");
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
