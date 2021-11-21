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

namespace BrowserWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            GoHome();
            
        }
        void GoHome()
        {
            Navigate(Settings1.Default.homePage);
        }
        
        void Navigate(string web)
        {
            Uri uri;
            try
            {
                uri = new Uri(web);
            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                try
                {
                    uri=new Uri("http://"+web);
                }catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                }
                return;
            }
            
            Browser.Navigate(uri);  
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            Navigate(web.Text);
        }
    }
}
