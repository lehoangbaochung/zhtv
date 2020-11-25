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
using System.Windows.Shapes;
using ZHTV.Interface;
using ZHTV.Models;
using ZHTV.Functions;
using ZHTV.Interface.Music;

namespace ZHTV
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Chat c = new Chat();
            c.Show();
            Hide();
        }

        private void btnSelect1_Click(object sender, RoutedEventArgs e)
        {
            Music m = new Music();
            m.Show();
            Hide();
        }
    }
}
