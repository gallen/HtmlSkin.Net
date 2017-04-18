using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;


namespace HtmlSkin.Net
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class BrowserHub
    {
        public BrowserHub(WebBrowser browser, string homePath)
        {
            _webBrowser = browser;

            IESettings.DisableSecurityCheck();
#if DEBUG
            _webBrowser.ScriptErrorsSuppressed = false;
#else
            _webBrowser.ScriptErrorsSuppressed = true;
#endif
            _webBrowser.ObjectForScripting = this;

            _client = new BrowserClient(_webBrowser);

            _webBrowser.Navigate(homePath);
        }

        public dynamic Client
        {
            get
            {
                return _client;
            }
        }

       
        private WebBrowser _webBrowser;
        dynamic _client;
    }

}
