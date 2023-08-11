﻿using QuanLyKhachSan.ViewModels;
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

namespace QuanLyKhachSan.UserControls
{
    /// <summary>
    /// Interaction logic for PhongUC.xaml
    /// </summary>
    public partial class PhongUC : UserControl
    {
        public PhongUC()
        {
            InitializeComponent();
            this.DataContext = new PhongVM();
        }
    }
}
