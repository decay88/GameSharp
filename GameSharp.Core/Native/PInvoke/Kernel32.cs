using GameSharp.Core.Native.Enums;
using GameSharp.Core.Native.Structs;
using System;
using System.Runtime.InteropServices;

namespace GameSharp.Core.Native.PInvoke
{
    public static class Kernel32
    {
        public const string kernel32 = "kernel32.dll";

        [DllImport(kernel32, SetLastError = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, int dwSize, MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport(kernel32, SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int size, out IntPtr lpNumberOfBytesWritten);

        [DllImport(kernel32, SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int size, out IntPtr lpNumberOfBytesRead);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr baseAddress, string procName);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport(kernel32, SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport(kernel32, SetLastError = true)]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport(kernel32, SetLastError = true)]
        public static extern int SuspendThread(IntPtr hThread);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport(kernel32, SetLastError = true)]
        public static extern bool GetThreadContext(IntPtr hThread, ref ThreadContext32 lpContext);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPWStr)]string lpFileName);

        [DllImport(kernel32, SetLastError = true)]
        public static extern IntPtr LoadLibraryExW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);
    }
}