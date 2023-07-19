using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Pos_Tron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();

            ////Tạo danh sách Menu rỗng để chứa dữ liệu
            //List<doUong> Menu = new List<doUong>();
            ////Mở file excel
            //var package = new ExcelPackage(new FileInfo("Menu.xlsx"));

            ////Lấy sheet đầu tiên
            //ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            ////Duyệt tuần tự từ dòng 2 đến hết của file excel
            //for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
            //{
            //    //Biến j: biểu thị cho 1 column trong file
            //    int j = 1;

            //    //vị trí lấy ra đầu tiền là [i,1]. i lần đầu là 1
            //    //tăng j lên 1 đơn vị khi thực hiện xong câu lệnh.
            //    //Cà phê đen
            //    string tenDoUong = worksheet.Cells[i++, j].Value.ToString();
            //    string Gia = worksheet.Cells[i--, j++].Value.ToString();
            //    //tạo list
            //    doUong a = new doUong(tenDoUong, Convert.ToInt32(Gia));
            //    Menu.Add(a);
            //}
            //dgvList.DataSource = Menu;
            label1.Text = "Cà phê sữa";
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
        }
    }
}