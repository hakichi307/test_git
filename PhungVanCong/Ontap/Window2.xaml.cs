﻿using System;
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
using System.Windows.Shapes;
using Ontap.Models;

namespace Ontap
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QLBANHANGContext db = new QLBANHANGContext();
            var query = from p in db.SanPhams
                        join k in db.LoaiSanPhams
                        on p.MaLoai equals k.MaLoai
                        where p.MaLoai == "01"
                        select new
                        {
                            p.MaSp,
                            p.TenSp,
                            p.DonGia,
                            p.SoLuong,
                            ThanhTien = p.DonGia * p.SoLuong
                        };
            dgvThongke.ItemsSource = query.ToList();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
