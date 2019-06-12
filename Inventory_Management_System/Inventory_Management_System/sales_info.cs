using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    class sales_info
    {
        public int rownum;
        public double returned, payment,total;
        public string[] code = new string[50];
        public string[] name_array = new string[50];
        public string[] price_array = new string[50];
        public string[] qty_array = new string[50];
    }
}
