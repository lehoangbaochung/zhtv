using System.Windows;
using ZHTV.Functions.Online;
using ZHTV.Interface;
using ZHTV.Models.Windows;

namespace ZHTV
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void MyMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
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
            MainWindowElement element = new MainWindowElement
            {
                VideoId = tbxVideoID.Text,
                SheetId = tbxSheetID.Text,
                SheetTab = tbxSheetTab.Text,
                SheetRange = tbxSheetRange.Text
            };

            Sheet.Bind(element);
            Manage.FillNextSongs();

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

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
