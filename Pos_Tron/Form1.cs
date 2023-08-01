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
using System.Xml.Linq;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.Style;

namespace Pos_Tron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon;
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
                if (control is TextBox && control.Name != "txtName")
                {
                    //tạo textBox chứa giá trị của textbox price trong panel order
                    TextBox priceTextBox = (TextBox)control;
                    //tạo biến int giá
                    int gia = 0;
                    //chuyển giá trị trong textbox price thành kiểu int
                    gia = Convert.ToInt32(priceTextBox.Text);// Chuyển đổi và xóa dấu phân cách hàng đơn vị
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
            Name.Name = "txtName";
            Name.Location = new Point(x, y); // Đặt vị trí của ô textbox
            Name.Width = 151;
            Name.Height = 32;
            Name.Text = Ten;
            PanelOrder.Controls.Add(Name); // Thêm ô textbox vào panel

            NumericUpDown Quantity = new NumericUpDown(); // Tạo một ô numeric mới
            Quantity.Location = new Point(x + 157, y); // Đặt vị trí của ô numeric
            Quantity.Width = 60;
            Quantity.Height = 32;
            Quantity.Minimum = 1; // Giá trị nhỏ nhất cho Numeric
            Quantity.Maximum = 1000; // Giá trị lớn nhất cho Numeric
            Quantity.Value = 1;
            Quantity.DecimalPlaces = 0; // Số chữ số sau dấu thập phân (0 là số nguyên)
            PanelOrder.Controls.Add(Quantity); // Thêm ô numeric vào panel

            TextBox Price = new TextBox(); // Tạo một ô textbox mới
            Price.Location = new Point(x + 222, y); // Đặt vị trí của ô textbox
            Price.Width = 180;
            Price.Height = 32;
            Price.Text = (Gia * Quantity.Value).ToString();
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

                    Price.Text = (Gia * quantityTextBox.Value).ToString(); // Cập nhật giá trị của ô Price
                    TotalPrice.Text = TinhTongGia().ToString(); // Cập nhật giá trị của ô TotalPrice

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

            TotalPrice.Text = TinhTongGia().ToString();

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
            // Tạo danh sách controls cần xóa
            List<Control> controlsToRemove = new List<Control>();

            // Duyệt qua từng control trong PanelOrder
            foreach (Control control in PanelOrder.Controls)
            {
                // Kiểm tra nếu control là TextBox hoặc NumericUpDown
                if (control is TextBox || control is NumericUpDown)
                {
                    // Thêm control vào danh sách controlsToRemove
                    controlsToRemove.Add(control);
                }
            }

            // Xóa các control từ danh sách controlsToRemove và giải phóng tài nguyên của chúng
            foreach (Control control in controlsToRemove)
            {
                // Giải phóng tài nguyên của control
                control.Dispose();
                // Xóa control khỏi PanelOrder
                PanelOrder.Controls.Remove(control);
            }

            // Thiết lập lại vị trí ban đầu cho việc thêm control mới vào PanelOrder
            toaDoXNewRowPanelOrder = 13;
            toaDoYNewRowPanelOrder = 62;

            // Xóa nội dung của TotalPrice
            TotalPrice.Text = "";

            // Xóa danh sách đã bán tạm thời (soldListtemp)
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
            // Duyệt qua từng cặp key-value trong soldListtemp
            foreach (var item in soldListtemp)
            {
                // Kiểm tra nếu soldList không chứa key của cặp key-value hiện tại
                if (!soldList.ContainsKey(item.Key))
                {
                    // Thêm cặp key-value vào soldList
                    soldList.Add(item.Key, item.Value);
                }
                else
                {
                    // Nếu đã tồn tại key trong soldList, cộng giá trị value của key đó với giá trị value trong soldListtemp
                    soldList[item.Key] = soldList[item.Key] + item.Value;
                }
            }
        }

        private void btnChotDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Duyệt qua từng control trong panelChotDon
                foreach (Control control in panelChotDon.Controls)
                {
                    // Nếu control không phải là Label hoặc không có tên là "labelDaBan", thực hiện hàm ClearPanelChotDon()
                    if (!(control is Label && control.Name == "labelDaBan"))
                    {
                        ClearPanelChotDon();
                    }
                }

                // Lưu dữ liệu soldList vào tệp Excel
                FileInfo file = new FileInfo("Tiền Cà Phê.xlsx");
                // Mở tệp Excel
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // Lấy tháng hiện tại - 2 và chọn sheet tương ứng
                    int month = DateTime.Today.Month - 2;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[month];

                    // Lấy ngày hiện tại + 2
                    int day = DateTime.Today.Day + 2;
                    // Duyệt qua các ngày (chỉ duyệt qua 1 ngày)
                    for (int i = day; i < day + 1; i++)
                    {
                        // Duyệt qua các item trong soldListtemp (Danh sách đã bán)
                        foreach (KeyValuePair<string, int> item in soldListtemp)
                        {
                            // Khởi tạo biến count
                            int count = 2;

                            // Xác định vị trí cột tương ứng với tên sản phẩm và cộng thêm count
                            switch (item.Key)
                            {
                                case "Cà Phê Đen":
                                    count += 0;
                                    break;
                                case "Cà Phê Sữa":
                                    count += 1;
                                    break;
                                case "Bạc Xỉu":
                                    count += 2;
                                    break;
                                case "Cà Phê Muối":
                                    count += 3;
                                    break;
                                case "Ca Cao":
                                    count += 4;
                                    break;
                                case "Ca Cao Muối":
                                    count += 5;
                                    break;
                            }

                            // Lấy giá trị hiện tại từ ô cần cập nhật
                            object cellValue = worksheet.Cells[i, count].Value;
                            int currentValue = cellValue != null ? Convert.ToInt32(cellValue) : 0;
                            int Tong = currentValue + item.Value;
                            worksheet.Cells[i, count].Value = Tong.ToString();
                            // Lấy đối tượng ExcelStyle cho ô cần căn lề bên phải.
                            var cellStyle = worksheet.Cells[i, count].Style;

                            // Đặt căn lề bên phải cho ô.
                            cellStyle.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                    }
                    // Lưu và đóng tệp Excel
                    package.Save();
                }

                // Lưu danh sách đã bán từ soldListtemp vào soldList
                saveSoldListTemp(soldListtemp, soldList);

                // Thêm dòng mới vào panelChotDon
                themDongPanelChotDon(Menu);

                // Tính tổng số ly đã bán và hiển thị lên totalSold
                totalSold.Text = TinhTongLy();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có gì đâu mà chốt !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Gọi phương thức btnClear_Click để làm sạch dữ liệu
                btnClear_Click(sender, e);
            }
        }

        public string TinhTongLy()
        {
            // Khởi tạo biến lưu tổng số ly
            int tongLy = 0;

            // Duyệt qua từng control trong panelChotDon
            foreach (Control control in panelChotDon.Controls)
            {
                // Kiểm tra nếu control là TextBox và không có tên là "Quantity"
                if (control is TextBox && control.Name != "Quantity")
                {
                    // Ép kiểu control thành TextBox để lấy giá trị số ly đã bán
                    TextBox soldTextBox = (TextBox)control;
                    // Khởi tạo biến lưu số ly đã bán
                    int ly = 0;
                    // Thử chuyển giá trị trong TextBox sang dạng số nguyên (nếu có thể)
                    int.TryParse(soldTextBox.Text, out ly);
                    // Cộng số ly đã bán vào tổng số ly
                    tongLy += ly;
                }
            }
            // Trả về tổng số ly dưới dạng chuỗi
            return tongLy.ToString();
        }

        public void themDongPanelChotDon(List<doUong> Menu)
        {
            // Khởi tạo các biến vị trí ban đầu cho label và textbox
            int xName = 14;
            int yName = 65;
            int xQuantity = 200;
            int yQuantity = 56;

            // Duyệt qua danh sách Menu (danh sách đồ uống) để thêm label và textbox tương ứng vào panel
            for (int i = 0; i < Menu.Count; i++)
            {
                // Tạo một Label mới
                Label Name = new Label();
                // Đặt vị trí cho label
                Name.Location = new Point(xName, yName);
                Name.Height = 23;
                Name.Width = 180;
                // Đặt nội dung của label là tên đồ uống từ danh sách Menu
                Name.Text = Menu[i].Ten;
                // Thêm label vào panel
                panelChotDon.Controls.Add(Name);
                // Tăng yName để đặt vị trí cho label tiếp theo
                yName += 47;

                // Tạo một ô số lượng mới (TextBox)
                TextBox Quantity = new TextBox();
                // Đặt vị trí cho ô số lượng (TextBox)
                Quantity.Location = new Point(xQuantity, yQuantity);
                Quantity.Height = 32;
                Quantity.Width = 32;
                Quantity.ReadOnly = true;
                // Thêm ô số lượng (TextBox) vào panel
                panelChotDon.Controls.Add(Quantity);
                // Tăng yQuantity để đặt vị trí cho ô số lượng tiếp theo
                yQuantity += 47;

                // Duyệt qua danh sách soldList (danh sách đã bán)
                foreach (var item in soldList)
                {
                    // Kiểm tra nếu tên đồ uống từ soldList trùng với tên đồ uống từ Menu
                    if (Name.Text == item.Key)
                    {
                        // Đặt giá trị số lượng đã bán vào ô số lượng (TextBox)
                        Quantity.Text = item.Value.ToString();
                    }
                }
            }
        }

    }
}