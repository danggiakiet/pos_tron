using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos_Tron
{
    public class Sold
    {
        public string Ten { get; set; }
        public int soLuong { get; set; }

        public Sold(string name, int quantity)
        {
            this.Ten = name;
            this.soLuong = quantity;
        }
    }
}