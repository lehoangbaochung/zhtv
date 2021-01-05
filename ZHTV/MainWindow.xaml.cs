using OpenQA.Selenium.Chrome;
using System.Windows;
using ZHTV.Functions.Online;
using ZHTV.Interface;
using ZHTV.Models.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace ZHTV
{
    public partial class MainWindow : Window
    {
        public readonly ChromiumWebBrowser browser;

        public MainWindow()
        {
            InitializeComponent();



            //cefDriver = new ChromeDriver(options);

            //ChromeDriver chromeDriver = new ChromeDriver();

            //chromeDriver.Navigate().GoToUrl("https://www.youtube.com/embed/1yWnaD5EXgQ");

            //var button = chromeDriver.FindElementByXPath("//*[@id=\"movie_player\"]/div[4]/button");

            //button.Click();
            //MyMediaElement.MediaFailed += MyMediaElement_MediaFailed;
            //MyMediaElement.LoadedBehavior = MediaState.Play;
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
