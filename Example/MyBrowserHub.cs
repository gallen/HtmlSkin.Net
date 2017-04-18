using System;
using System.Windows.Forms;
using HtmlSkin.Net;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Example
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class MyBrowserHub : BrowserHub
    {
        public MyBrowserHub(System.Windows.Forms.WebBrowser browser, string homePath) : base(browser, homePath)
        {

        }

        public void Test1()
        {
            MessageBox.Show("Test1 called from .Net side.");
        }

        public void Test2(string msg)
        {
            MessageBox.Show("Test2 called from .Net side with message from javascript: " + msg);
        }

        public void TestCallBack()
        {
            MessageBox.Show("TestCallBack called from .Net side.");

            Client.callBack("Hello javascript.");
        }

        public string TestReturn()
        {
            return "Returned from .net";
        }

        public string SelectFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\" ;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
            openFileDialog1.FilterIndex = 2 ;
            openFileDialog1.RestoreDirectory = true ;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
                return "";
        }

        public void SyncProgress()
        {
            for(int i = 0; i < 6; i++)
            {
                // Process UI events.
                // js function is called in UI thread.
                Application.DoEvents();
                Client.showProgressSync(i);

                System.Threading.Thread.Sleep(1000);
            }
        }

        public async void AsyncProgress()
        {
            // Dispatcher from main thread(UI thread)
            var dispatcher = Dispatcher.CurrentDispatcher;
            await Task.Run(() =>
            {
                for (int i = 0; i < 6; i++)
                {
                    // Call js function in UI thread.
                    dispatcher.Invoke(() =>
                    {
                        Client.showProgressAsync(i);
                    });

                    System.Threading.Thread.Sleep(1000);
                }
            });
        }
    }
}
