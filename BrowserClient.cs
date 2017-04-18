using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Forms;

namespace HtmlSkin.Net
{
    public class BrowserClient : DynamicObject
    {
        public BrowserClient(WebBrowser webBrowser)
        {
            _webBrowser = webBrowser;
        }
        
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_webBrowser == null || _webBrowser.Document == null)
            {
                result = null;
                return false;
            }

            var rawName = "##hostcall##" + binder.Name;
            result = _webBrowser.Document.InvokeScript(rawName, args);
            return true;
        }

        private WebBrowser _webBrowser;
    }
}
