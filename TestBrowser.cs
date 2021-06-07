using CefSharp.Wpf;

namespace CefSharpExample
{
    internal class TestBrowser : ChromiumWebBrowser
    {
        public JsWorker JsWorker { get; set; }
    }
}