using CefSharp;
using System.Threading.Tasks;

namespace CefSharpExample
{
    internal class JsWorker
    {
        private IJavascriptCallback _testCallback;

        public void SetCallBacks(IJavascriptCallback testCallback)
        {
            _testCallback = testCallback;
        }

        [JavascriptIgnore]
        public async Task<bool> ExecuteCallback()
        {
            if (_testCallback != null && _testCallback.CanExecute && !_testCallback.IsDisposed)
            {
                JavascriptResponse response = await _testCallback.ExecuteAsync();
                return response.Success && bool.TryParse(response.Result?.ToString(), out bool result) && result;
            }
            else
            {
                return false;
            }
        }
    }
}