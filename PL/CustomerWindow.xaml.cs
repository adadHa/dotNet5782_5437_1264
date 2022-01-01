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
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private IBL BLObject { get; set; }
        //add mode
        public CustomerWindow()
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
        }

        //options mode
        public CustomerWindow(BO.Customer customer)
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
        }
    }
}