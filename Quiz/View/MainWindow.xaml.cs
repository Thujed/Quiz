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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quiz.Supports.Extensions;
using Quiz.ViewModel;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM MainWindowVM { get; set; }

        public MainWindow()
        {
            MainWindowVM = new MainVM();
            DataContext = MainWindowVM;
            InitializeComponent();

        }

        private void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            


        }
    }
}
