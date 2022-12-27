using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ontap.Models;
using System.Text.RegularExpressions;//Dùng khi Check DL
using System.Reflection;//Dùng khi chọn dòng
namespace Ontap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Tạo đối tượng trỏ tới Model
        QLBANHANGContext db = new QLBANHANGContext();
        //Hàm Load dữ liệu lên DataGrid
        private void HienThiDuLieu()
        {
            var query = from sp in db.SanPhams
                        orderby sp.DonGia
                        select new
                        {
                            sp.MaSp,
                            sp.TenSp,
                            sp.MaLoai,
                            sp.SoLuong,
                            sp.DonGia,
                            ThanhTien = sp.SoLuong * sp.DonGia
                        };
            dgvSanPham.ItemsSource = query.ToList();
        }
        //Hàm load dữ liệu lên ComboBox
        private void HienThiCB()
        {
            var query = from lsp in db.LoaiSanPhams
                        select lsp;
            cboLoai.ItemsSource = query.ToList();
            cboLoai.DisplayMemberPath = "TenLoai";
            cboLoai.SelectedValuePath = "MaLoai";
            cboLoai.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDuLieu();
            HienThiCB();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            //Kiểm tra không cho trùng mã sản phẩm
            var query = db.SanPhams.SingleOrDefault(t => t.MaSp.Equals(txtMa.Text));
            if (query != null)
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại!", "Thong Bao");
                HienThiDuLieu();
            }
            //Kiểm tra không cho nhập thiếu dữ liệu
            else if (txtMa.Text == "" || txtTen.Text == "" || txtSoLuong.Text == "" || txtDonGia.Text == "")
            {
                MessageBox.Show("\n Bạn cần nhập đầy đủ dữ liệu");
                HienThiDuLieu();
            }
            else if (!Regex.IsMatch(txtDonGia.Text, @"\d+")|| !Regex.IsMatch(txtSoLuong.Text, @"\d+"))
            {
                MessageBox.Show("\nĐơn giá hoặc số lượng nhập phải là số dương");
                HienThiDuLieu();
            }
            else
            {
                SanPham spMoi = new SanPham();
                spMoi.MaSp = txtMa.Text;
                spMoi.TenSp = txtTen.Text;
                spMoi.DonGia = double.Parse(txtDonGia.Text);
                spMoi.SoLuong = int.Parse(txtSoLuong.Text);

                LoaiSanPham itemSelected = (LoaiSanPham)cboLoai.SelectedItem;
                spMoi.MaLoai = itemSelected.MaLoai;
                db.SanPhams.Add(spMoi);
                db.SaveChanges();//Lưu thay đổi vào CSDL
                MessageBox.Show("Thêm thành công!", "Thong Bao");
                HienThiDuLieu();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            //xác định 1 sản phẩm cần sửa theo mã
            var spSua = db.SanPhams.SingleOrDefault(t => t.MaSp.Equals(txtMa.Text));
            if (spSua != null)
            {
                spSua.TenSp = txtTen.Text;
                LoaiSanPham itemSelected = (LoaiSanPham)cboLoai.SelectedItem;
                spSua.MaLoai = itemSelected.MaLoai;
                spSua.DonGia = double.Parse(txtSoLuong.Text);
                spSua.SoLuong = int.Parse(txtSoLuong.Text);
                db.SaveChanges();
                MessageBox.Show("Sửa thành công!", "Thong bao");
                HienThiDuLieu();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            //Xác định 1 sản phẩm cần xoá theo Mã
            var spXoa = db.SanPhams.SingleOrDefault(t => t.MaSp.Equals(txtMa.Text));
            if (spXoa != null)
            {
                MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Thong bao", MessageBoxButton.YesNo);
                if (rs == MessageBoxResult.Yes)
                {
                    db.SanPhams.Remove(spXoa);
                    db.SaveChanges();
                    HienThiDuLieu();
                }
            }
            else
            {
                MessageBox.Show("Không có sản phẩm này để xoá!", "Thông báo");
            }
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {

        }
        //chọn dòng trong DataGrid
        private void dgvSanPham_SelectedCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSanPham.SelectedItem != null)
            {
                try
                {
                    Type t = dgvSanPham.SelectedItem.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    txtMa.Text = p[0].GetValue(dgvSanPham.SelectedValue).ToString();
                    txtTen.Text = p[1].GetValue(dgvSanPham.SelectedValue).ToString();
                    cboLoai.SelectedValue = p[2].GetValue(dgvSanPham.SelectedValue).ToString();
                    txtSoLuong.Text = p[3].GetValue(dgvSanPham.SelectedValue).ToString();
                    txtDonGia.Text = p[4].GetValue(dgvSanPham.SelectedValue).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi chọn hàng" + ex.Message, "Thông báo");
                }
            }
        }

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            Window2 myWindow = new Window2();
            myWindow.Show();
        }
    }    
}
