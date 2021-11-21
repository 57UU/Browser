using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xyz.blockers.lowBrowser
{
    
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public static void Init()
        {
            if(instance == null)
                instance = new Form1();
        }
        private Form1()
        {
            //SetWebBrowserFeatures(11);
            InitializeComponent();

            button2_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            WebBrowser.Navigate(textBox1.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WebBrowser.Navigate(Settings1.Default.homePage);
            
        }

        private void extendedWebBrowser2_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebBrowser.ScriptErrorsSuppressed = true;
            
            WebBrowser.BeforeNewWindow += webBrowser_BeforeNewWindow;
            toolStripProgressBar1.Maximum = 100;
                
            
        }

        //使用方法:
 
 
        //ExtendedWebBrowser extendedWebBrowser2=new ExtendedWebBrowser();

        private void webBrowser_BeforeNavigate(object sender, EventArgs e)
        {

        }

        //增加了这个事件,可以替代原来的NewWindow事件.
        private void webBrowser_BeforeNewWindow(object sender, EventArgs e)
        {
            WebBrowserExtendedNavigatingEventArgs eventArgs = e as WebBrowserExtendedNavigatingEventArgs;
            WebBrowser.Navigate(eventArgs.Url);
            textBox1.Text = eventArgs.Url;  
            eventArgs.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebBrowser.GoBack();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebBrowser.GoForward();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button5, 0, button5.Height);
        }

        private void reFreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void extendedWebBrowser2_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading";
            toolStripProgressBar1.Value = 30;
            toolStripProgressBar1.Visible = true;
            textBox1.Text = e.Url.ToString();
            WebBrowser.Focus();
        }

        private void extendedWebBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loaded";
            toolStripProgressBar1.Value = 100;
            hideProgressbar();
        }
        async void  hideProgressbar()
        {
            await Task.Delay(2000);
            toolStripProgressBar1.Visible=false;
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                button1_Click(sender, e);
            }
        }

        private void setHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var input=new InputBox(text => { 
                Settings1.Default.homePage = text;
                Settings1.Default.Save();
            });
            input.BoxText = Settings1.Default.homePage;
            input.Show(this);
        }

        private void starsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        static void SetWebBrowserFeatures(int ieVersion)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            UInt32 ieMode = GeoEmulationModee(ieVersion);
            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, ieMode, RegistryValueKind.DWord);
            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);
        }

        static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. 
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. 
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                    
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11
                    break;
            }
            return mode;
        }

    }
}
