using System;
using System.Windows.Forms;
using HtmlSkin.Net;
using System.IO;

namespace Example
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // html file path for main UI
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "example.html");

            // Connect hub with webbrowser Control.
            // webBrowser is the webbrowser control intance.
            _browserHub = new MyBrowserHub(webBrowser, htmlPath);

        }
        private BrowserHub _browserHub;
    }
}
