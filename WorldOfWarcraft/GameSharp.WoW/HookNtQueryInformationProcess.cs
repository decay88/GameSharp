using GameSharp.Core.Memory;
using GameSharp.Core.Services;
using GameSharp.Internal;
using GameSharp.Internal.Extensions;
using GameSharp.Internal.Memory;
using GameSharp.Internal.Module;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameSharp.WoW
{
    public class HookNtQueryInformationProcess : Hook
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        private delegate int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, IntPtr processInformation, uint processInformationLength, IntPtr returnLength);

        private int DetourMethod(IntPtr processHandle, int processInformationClass, IntPtr processInformation, uint processInformationLength, IntPtr returnLength)
        {
            LoggingService.Info("NtQueryInformationProcess called");

            LoggingService.Info("Detected...");

            int result = this.CallOriginal<int>(processHandle, processInformationClass, processInformation, processInformationLength, returnLength);

            return result;
        }

        public override Delegate GetDetourDelegate()
        {
            return new NtQueryInformationProcess(DetourMethod);
        }

        public override Delegate GetHookDelegate()
        {
            GameSharpProcess process = GameSharpProcess.Instance;

            process.RefreshModules();

            MemoryModule module = process.Modules["ntdll.dll"] as MemoryModule;

            IMemoryAddress procAddress = module.GetProcAddress("NtQueryInformationProcess");

            return procAddress.ToDelegate<NtQueryInformationProcess>();
        }
    }
}
