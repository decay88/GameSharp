using GameSharp.Core.Memory;
using GameSharp.Internal;
using GameSharp.Internal.Extensions;
using GameSharp.Internal.Memory;
using GameSharp.Internal.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameSharp.WoW
{
    public class SafeCallNtQueryInformationProcess : SafeFunction
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SafeCallNtQueryInformationProcessDelegate(IntPtr processHandle, int processInformationClass, IntPtr processInformation, uint processInformationLength, IntPtr returnLength);

        protected override Delegate ToCallDelegate()
        {
            GameSharpProcess process = GameSharpProcess.Instance;

            MemoryModule user32dll = process.Modules["ntdll.dll"] as MemoryModule;

            IMemoryAddress ntQueryInformationProcessPtr = user32dll.GetProcAddress("NtQueryInformationProcess");

            return ntQueryInformationProcessPtr.ToDelegate<SafeCallNtQueryInformationProcessDelegate>();
        }
    }
}
