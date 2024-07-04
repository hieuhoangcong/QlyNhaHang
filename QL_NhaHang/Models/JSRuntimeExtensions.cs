using Microsoft.JSInterop;

namespace QL_NhaHang.Models
{
    public static class JSRuntimeExtensions
    {
        public static bool IsBrowser(this IJSRuntime jsRuntime)
        {
            return jsRuntime is IJSInProcessRuntime;
        }
    }
}
