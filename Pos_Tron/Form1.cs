using System;
using System.Collections;
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
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;

namespace Pos_Tron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Tọa độ x,y của dòng trong panelOrder
        private int toaDoXNewRowPanelOrder = 13;

        private int toaDoYNewRowPanelOrder = 62;

        // Tạo danh sách Menu rỗng để chứa dữ liệu
        private List<doUong> Menu = new List<doUong>();

        //tạo danh sách chứa số lượng đã bán
        private Dictionary<string, int> soldList = new Dictionary<string, int>();

        private Dictionary<string, int> soldListtemp = new Dictionary<string, int>();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mở file excel
            using (var package = new ExcelPackage(new FileInfo("Menu.xlsx")))
            {
                // Lấy sheet đầu tiên
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                // Duyệt từ dòng 1 đến hết của file excel
                for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
                {
                    // Lấy tên đồ uống từ cột A
                    string tenDoUong = worksheet.Cells[i, 1].Value.ToString();

                    // Lấy giá từ cột B
                    string gia = worksheet.Cells[i, 2].Value.ToString();

                    // Tạo đối tượng doUong và thêm vào danh sách Menu
                    doUong doUong = new doUong(tenDoUong, Convert.ToInt32(gia));
                    Menu.Add(doUong);
                }
            }
            //Khởi tạo tọa độ x, y cho button trong panel Menu
            //Khởi tạo biến đếm index để xác định thứ tự đồ uống trong menu
            //Khởi tạo biến đếm để xác định vị trí button trong panel menu
            int x = 14; int y = 79; int index = 0; int count = 0;
            //Cho duyệt từng đồ uống trong menu
            foreach (doUong a in Menu)
            {
                //Nếu vị trí button chia hết cho 3 và thứ tự lớn hơn 0 thì cho đặt lại tọa độ x,y
                if (count % 3 == 0 && index > 0)
                {
                    x = 14;
                    y = y + 270;
                    count = 0;
                }
                //Gọi hàm thêm button vào panelmenu
                addButtonToPanelMenu(x, y, a.Ten, index);
                //Tăng vị trí tọa độ x
                x = x + 200;
                //tăng biến đếm vị trí
                count++;
                //tăng thứ tự đồ uống
                index++;
            }
            //Thêm logo
            panelLogo.BackgroundImage = Image.FromFile("Img/Logo Tròn.png");
        }

        private int TinhTongGia()
        {
            //Khởi tạo giá trị tổng giá tiền
            int tongGia = 0;
            //cho duyệt tất cả các phần tử trong panel oder
            foreach (Control control in PanelOrder.Controls)
            {
                //Nếu panel là textbox và tên của textbox khác "name" thì lấy
                if (control is TextBox && control.Name != "Name")
                {
                    //tạo textBox chứa giá trị của textbox price trong panel order
                    TextBox priceTextBox = (TextBox)control;
                    //tạo biến int giá
                    int gia = 0;
                    //chuyển giá trị trong textbox price thành kiểu int
                    int.TryParse(priceTextBox.Text.Replace(".", ""), out gia); // Chuyển đổi và xóa dấu phân cách hàng đơn vị
                    //tính tổng giá tiền trong panel order
                    tongGia += gia;
                }
            }
            //Xuất giá trị tổng
            return tongGia;
        }

        private void addButtonToPanelMenu(int x, int y, string Name, int index)
        {
            //Khởi tạo textBox trong panel bao gồm text, độ rộng, chiều cao và vị trí
            TextBox textBox = new TextBox();
            textBox.Text = Name;
            //Căn chỉnh text cho textBox
            textBox.TextAlign = HorizontalAlignment.Center;
            //Chỉnh readOnly cho textBox
            textBox.ReadOnly = true;
            textBox.Width = 180;
            textBox.Height = 32;
            textBox.Location = new Point(x, y + 218);
            //Thêm textBox vào panelOrder
            PanelMenu.Controls.Add(textBox);

            //Khởi tạo đường dẫn đến vị trí ảnh đồ uống
            string path = $"Img/{Name}.png";
            //Khởi tạo button trong panel bao gồm background, name, độ rộng, chiều cao và vị trí
            Button button = new Button();
            button.BackgroundImage = Image.FromFile(path);
            button.Name = Name;
            button.Width = 180;
            button.Height = 250;
            button.Location = new Point(x, y);
            //Thêm button vào panelOrder
            PanelMenu.Controls.Add(button);

            //Thêm hàm click cho button
            button.Click += (sender, e) => buttonPanelMenu_Click(index);
        }

        private void buttonPanelMenu_Click(int index)
        {
            try
            {
                doUong doUong = Menu[index];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void themDongPanelOrder(int x, int y, string Ten, int Gia)
        {
            TextBox Name = new TextBox(); // Tạo một ô textbox mới
            Name.Location = new Point(x, y); // Đặt vị trí của ô textbox
            Name.Width = 151;
            Name.Height = 32;
            Name.Text = Ten;
            PanelOrder.Controls.Add(Name); // Thêm ô textbox vào panel

            NumericUpDown Quantity = new NumericUpDown(); // Tạo một ô numeric mới
            Quantity.Location = new Point(x + 157, y); // Đặt vị trí của ô numeric
            Quantity.Width = 60;
            Quantity.Height = 32;
            Quantity.Value = 1;
            PanelOrder.Controls.Add(Quantity); // Thêm ô numeric vào panel

            TextBox Price = new TextBox(); // Tạo một ô textbox mới
            Price.Location = new Point(x + 222, y); // Đặt vị trí của ô textbox
            Price.Width = 180;
            Price.Height = 32;
            Price.Text = (Gia * Quantity.Value).ToString("N0");
            PanelOrder.Controls.Add(Price); // Thêm ô textbox vào panel
            //Thay đổi giá trị ô price theo số lượng
            Quantity.ValueChanged += (sender, e) =>
            {
                NumericUpDown quantityTextBox = (NumericUpDown)sender;
                TextBox nameTextBox = PanelOrder.Controls
                    .OfType<TextBox>()
                    .FirstOrDefault(tb => tb.Location == new Point(x, y));

                if (nameTextBox != null)
                {
                    string itemName = nameTextBox.Text;

                    Price.Text = (Gia * quantityTextBox.Value).ToString("N0"); // Cập nhật giá trị của ô Price
                    TotalPrice.Text = TinhTongGia().ToString("N0"); // Cập nhật giá trị của ô TotalPrice

                    // Kiểm tra đã có sản phẩm đó hay chưa
                    if (!soldListtemp.ContainsKey(itemName))
                    {
                        // Chưa có thì thêm vào danh sách
                        soldListtemp.Add(itemName, Convert.ToInt32(quantityTextBox.Value));
                    }
                    else
                    {
                        // Nếu có rồi thì cập nhật lại số lượng bằng giá trị của Quantity.Value
                        soldListtemp[Name.Text] = soldListtemp[Name.Text] + Convert.ToInt32(Quantity.Value);
                    }
                }
            };

            TotalPrice.Text = TinhTongGia().ToString("N0");

            //Thêm vào danh sách số lượng đã bán
            //Kiểm tra đã có sản phảm đó hay chưa
            if (!soldListtemp.ContainsKey(Name.Text))
            {
                //Chưa có thì thêm vào danh sách
                soldListtemp.Add(Name.Text, Convert.ToInt32(Quantity.Value));
            }
            else
            {
                //Nếu có rồi thì cập nhập lại số lượng bằng giá trị của Quantity.Value
                soldListtemp[Name.Text] = soldListtemp[Name.Text] + Convert.ToInt32(Quantity.Value);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            List<Control> controlsToRemove = new List<Control>();

            // Tìm và thêm các control TextBox và NumericUpDown vào danh sách controlsToRemove
            foreach (Control control in PanelOrder.Controls)
            {
                if (control is TextBox || control is NumericUpDown)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Xóa các control từ danh sách controlsToRemove và giải phóng tài nguyên của chúng
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
                PanelOrder.Controls.Remove(control);
            }
            toaDoXNewRowPanelOrder = 13;
            toaDoYNewRowPanelOrder = 62;
            TotalPrice.Text = "";
            soldListtemp.Clear();
        }

        private void ClearPanelChotDon()
        {
            List<Control> controlsToRemove = new List<Control>();

            // Tìm và thêm các control TextBox và NumericUpDown vào danh sách controlsToRemove
            foreach (Control control in panelChotDon.Controls)
            {
                if (control is TextBox || control is Label)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Xóa các control từ danh sách controlsToRemove và giải phóng tài nguyên của chúng
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
                panelChotDon.Controls.Remove(control);
            }
        }

        public void saveSoldListTemp(Dictionary<string, int> soldListtemp, Dictionary<string, int> soldList)
        {
            foreach (var item in soldListtemp)
            {
                if (!soldList.ContainsKey(item.Key))
                {
                    soldList.Add(item.Key, item.Value);
                }
                else
                {
                    soldList[item.Key] = soldList[item.Key] + item.Value;
                }
            }
        }

        private void btnChotDon_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panelChotDon.Controls)
                {
                    if (!(control is Label && control.Name == "labelDaBan"))
                    {
                        ClearPanelChotDon();
                    }
                }
                saveSoldListTemp(soldListtemp, soldList);
                themDongPanelChotDon(Menu, soldList);
                totalSold.Text = TinhTongLy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có gì đâu mà chốt !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnClear_Click(sender, e);
            }
        }

        public string TinhTongLy()
        {
            int tongLy = 0;
            foreach (Control control in panelChotDon.Controls)
            {
                if (control is TextBox && control.Name != "Quantity")
                {
                    TextBox soldTextBox = (TextBox)control;
                    int ly = 0;
                    int.TryParse(soldTextBox.Text, out ly);
                    tongLy += ly;
                }
            }
            return tongLy.ToString();
        }

        public void themDongPanelChotDon(List<doUong> Menu, Dictionary<string, int> soldList)
        {
            int xName = 14;
            int yName = 65;
            int xQuantity = 200;
            int yQuantity = 56;
            for (int i = 0; i < Menu.Count; i++)
            {
                Label Name = new Label(); // Tạo một Label mới
                Name.Location = new Point(xName, yName); // Đặt vị trí của label
                Name.Height = 23;
                Name.Width = 180;
                Name.Text = Menu[i].Ten;
                panelChotDon.Controls.Add(Name); // Thêm label vào panel
                yName += 47;

                TextBox Quantity = new TextBox(); // Tạo một ô số lượng mới
                Quantity.Location = new Point(xQuantity, yQuantity); // Đặt vị trí của label
                Quantity.Height = 32;
                Quantity.Width = 32;
                Quantity.ReadOnly = true;
                panelChotDon.Controls.Add(Quantity); // Thêm ô textbox vào panel
                yQuantity += 47;

                foreach (var item in soldList)
                {
                    if (Name.Text == item.Key)
                    {
                        Quantity.Text = item.Value.ToString();
                    }
                }
            }
        }
    }
}