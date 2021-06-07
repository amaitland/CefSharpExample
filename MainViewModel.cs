using CefSharp;
using CefSharp.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace CefSharpExample
{
    internal class MainViewModel
    {
        public TestBrowser Browser { get; set; }

        public MainViewModel()
        {
            CefSharpSettings.ConcurrentTaskExecution = true;
            Cef.Initialize(new CefSettings());

            Browser = new TestBrowser()
            {
                JsWorker = new JsWorker(),
                Address = "www.google.ru"
            };
            Browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            Browser.JavascriptObjectRepository.NameConverter = null;
            Browser.JavascriptObjectRepository.Register("worker", Browser.JsWorker, isAsync: false);
            Browser.FrameLoadEnd += Browser_FrameLoadEnd;
            Browser.FrameLoadStart += _browser_FrameLoadStart;
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Browser.ShowDevTools();
            Browser.JsWorker.ExecuteCallback().GetAwaiter().GetResult();
        }

        private void _browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri("pack://application:,,,/testScript.js", UriKind.Absolute));
            string end = new StreamReader(resourceStream.Stream).ReadToEnd();

            if (e.Frame.IsMain)
            {
                e.Frame.ExecuteJavaScriptAsync(end);
            }
        }
    }
}