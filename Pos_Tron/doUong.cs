using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos_Tron
{
    public class doUong
    {
        public string Ten { get; set; }
        public int Price { get; set; }

        public doUong(string ten, int price)
        {
            this.Ten = ten;
            this.Price = price;
        }
    }
}