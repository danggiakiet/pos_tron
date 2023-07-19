using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pos_Tron
{
    public class Sold
    {
        public int CaPheDen { get; set; }
        public int CaPheSua { get; set; }
        public int CaPheMuoi { get; set; }

        //public int SuaTuoiCaPhe { get; set; }
        public int BacXiu { get; set; }

        //public int BacXiuMuoi { get; set; }
        public int CaCao { get; set; }

        public int CaCaoMuoi { get; set; }
        //public int CaCaoDaXay { get; set; }

        public Sold(int capheden, int caphesua, int caphemuoi, int bacxiu, int cacao, int cacaomuoi)
        {
            this.CaPheDen = capheden;
            this.CaPheSua = caphesua;
            this.CaPheMuoi = caphemuoi;
            this.BacXiu = bacxiu;
            this.CaCao = cacao;
            this.CaCaoMuoi = cacaomuoi;
        }
    }
}