using System.Windows;
using ZHTV.Interface;

namespace ZHTV
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Interface();
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

        private void Interface()
        {
               
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            tbxVideoID.IsEnabled = rbOn.IsChecked.Value;
            tbxSheetID.IsEnabled = rbOn.IsChecked.Value;
            tbxSheetTab.IsEnabled = rbOn.IsChecked.Value;
            tbxSheetRange.IsEnabled = rbOn.IsChecked.Value;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (cbInterface.SelectedIndex == 0)
            {
                Chat c = new Chat();
                c.Show();
                Hide();
            } 
            else if (cbInterface.SelectedIndex == 1)
            {
                Music m = new Music();
                m.Show();
                Hide();
            }    
        }
    }
}
