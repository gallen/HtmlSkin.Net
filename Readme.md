HtmlSkin.Net - A small utility to blend web front-end with .net
===

## Introduction
This is a small utility wrapping around [WebBrowser.ObjectForScripting](https://msdn.microsoft.com/en-us/library/system.windows.forms.webbrowser.objectforscripting)
to make the life easier for anyone who what use web-based technologies to 'draw' the UI for winform based .net application.

## What it can do?
Two simple things :)
* Call .Net methods from javascript 
* Call javascript functions from .Net

## How to use?
### Set up
  1. Copy these 3 files to your project(winform based)
     * BrowserHub.cs - Class with methods to be called from javascript.
     * BrowserClient.cs - Class as a proxy to call javascript function.
     * IESettings.cs - Take case of disabling some troublesome security check for webbrowser control.
  2. Define your own 'BrowserHub' class derived from 'BrowserHub'. e.g.
     ```c#
         [System.Runtime.InteropServices.ComVisibleAttribute(true)]
         public class MyBrowserHub : BrowserHub
         {
            public MyBrowserHub(System.Windows.Forms.WebBrowser browser, string homePath) : base(browser, homePath)
            {

            }

         }
     ```
     There're 2 things need to be noticed: 1) The derived BrowsreHub class need to be 'public'; 2) 'ComVisibleAttribute' must be specified.
  3. Connect your BrowserHub class instance with webbrowser control(surely you have to add the control by yourself) in your main form.
     Also specify the html file path your main UI logic resides.
     This normally was done in your form's load event handler. 
     ```c$
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
     ```
  4. Reference client javascript library in your main UI module(either html file ***OR*** javascript module if you use tools like webpack to build
     your front-end stuffs)
     * Reference the client library in your html file - use file "HtmlSkinHubNoModule.js"
     ```javascript
         <script src="HtmlSkinHubNoModule.js"></script>
     ```
     * Load the client library module if you use tool like webpack - use file "HtmlSkinHub.js"
      ```javascript
         import HtmlSkinHub from '../HumlSkinHub.js';
     ```     
    

       
### Call .Net methods from javascript
  1. Define the methods in your .Net BrowserHub class
     ```c#
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
     }
     ```
   2. Call the methods from javascript/html.
      ```html
        <button class="button primary" onclick="HtmlSkinHub.call('Test1')">test1</button>
        <button class="button primary" onclick="HtmlSkinHub.call('Test2', 'Hello, .Net')">test2</button>     
      ```
      You'll need call .Net method with this convention,
      ```
        // Up to 9 parameters
        HtmlSkinHub.call('methodName', parameter1, parameter2, ....)
      ```
### Call javascript functions from .Net
  1. Define the function in javascript like this,
     ```javascript
        HtmlSkinHub.methods({
            callBack: function (msg) {
                alert("callBack from .net side: " + msg);
            },     
     ```
     The function has to be wrapped inside 'HtmlSkinHub.methods' function.
  2. Call the function from your BrowserHub class in .Net
     ```c#
     [System.Runtime.InteropServices.ComVisibleAttribute(true)]
     public class MyBrowserHub : BrowserHub
        public void TestCallBack()
        {
            MessageBox.Show("TestCallBack called from .Net side.");

            Client.callBack("Hello javascript.");
        }
      }
      ```

     Within BrowserHub base class, there'a property 'Client' which is a proxy for the javascript world, so you can call
     your javascript function just like this,
     ```c#
        Client.javascriptFunction();
     ```
## MISC
Please note the javascript function call is under the main UI thread of your main program. 
This means if your main program is blocking (either running in a loop or in a blocking call),
your main UI will be frozen and the javascript function call won't got a chance to run. Here are 
2 ways to work around this:
1. Sync way using [Applicaiton.DoEvents](https://msdn.microsoft.com/en-us/library/system.windows.forms.application.doevents)
   ```c#
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
   ```
2. Async way using [Dispatcher.Invoke](https://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher.invoke(v=vs.110).aspx)
   ```c#
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
   ```

Things not tell please check the example project :).

***Leave me a message if you have any feedback.***







