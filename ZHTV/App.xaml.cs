using System.Windows;

namespace ZHTV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var settings = new CefSharp.Wpf.CefSettings();
            settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";
            CefSharp.Cef.Initialize(settings, true, browserProcessHandler: null);
        }
    }
}
