using GameSharp.Core.Module;
using GameSharp.Core.Services;
using GameSharp.Internal;
using GameSharp.Internal.Memory;
using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GameSharp.WoW
{
    public class Entrypoint
    {
        [DllExport]
        public static void Main()
        {
            LoggingService.Info("Injected");

            SafeCallNtQueryInformationProcess ntQueryInformationProcess = new SafeCallNtQueryInformationProcess();
            while (true)
            {
                Thread.Sleep(1000);

                LoggingService.Info($"-------------------------------------");

                //IntPtr ptrDebugPort = new IntPtr();
                //ntQueryInformationProcess.Call<bool>(-1, 0x7, ptrDebugPort, 4, null);
                //LoggingService.Info($"DebugPort: {ptrDebugPort != IntPtr.Zero}");

                //IntPtr ptrDebuggedObject = new IntPtr();
                //ntQueryInformationProcess.Call<bool>(-1, 0x1E, ptrDebuggedObject, 4, null);
                //LoggingService.Info($"DebuggedObject: {ptrDebuggedObject != IntPtr.Zero}");

                //IntPtr ptrDebuggedFlags = new IntPtr();
                //ntQueryInformationProcess.Call<bool>(-1, 0x1F, ptrDebuggedFlags, 4, null);
                //LoggingService.Info($"DebuggedFlags: {ptrDebuggedFlags != IntPtr.Zero}");
            }
        }
    }
}
