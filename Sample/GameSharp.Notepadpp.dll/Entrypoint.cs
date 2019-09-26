using GameSharp.Core.Services;
using GameSharp.Internal;
using RGiesecke.DllExport;
using System;

namespace GameSharp.Notepadpp.dll
{
    public class Entrypoint
    {
        [DllExport]
        public static void Main()
        {
            LoggingService.Info("I have been injected!");

            SafeCallMessageBoxW safeMessageBoxFunction = new SafeCallMessageBoxW();
            LoggingService.Info("Calling messagebox!");
            safeMessageBoxFunction.Call<int>(IntPtr.Zero, "This is a sample of how to Call a function", "Title of the Messagebox", 0);

            HookMessageBoxW messageBoxHook = new HookMessageBoxW();
            LoggingService.Info("Enabling hook on messagebox!");
            messageBoxHook.Enable();
        }
    }
}
