using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    class DataConnection
    {
       public  String connectstr = "Data Source=AUNI; Initial Catalog= InventoryDb; Integrated Security=True";
        public int ExecuteQuery(string query)
        {

            SqlConnection conn = new SqlConnection(connectstr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                cmd.ExecuteNonQuery();
                return 1;

            }
            catch (SqlException ex)
            {
                return 0;
            }

        }
        public DataTable GetDataTable(string query)
        {
            SqlConnection conn = new SqlConnection(connectstr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
