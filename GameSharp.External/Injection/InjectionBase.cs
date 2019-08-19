﻿using GameSharp.Core;
using GameSharp.Core.Injection;
using GameSharp.External;
using GameSharp.External.Injection;
using System;
using System.Diagnostics;
using System.IO;

namespace GameSharp.Injection
{
    public abstract class InjectionBase : IInjection
    {
        public Injectable InjectableAssembly { get; set; }

        private GameSharpProcess ExternalProcess { get; }

        public IProcess Process { get; }

        public InjectionBase(Process process)
        {
            Process = new GameSharpProcess(process) ?? throw new NullReferenceException();
            ExternalProcess = (GameSharpProcess) Process;
        }

        public void InjectAndExecute(Injectable assembly, bool attach)
        {
            InjectableAssembly = assembly;

            UpdateFiles(assembly.PathToAssemblyFile);

            // Possible anti-cheat detterence.
            ExternalProcess.SuspendThreads(true);

            Inject(assembly.PathToAssemblyFile);

            // In case we want to attach then we have to do so BEFORE we execute to give full debugging capabilities.
            if (attach && Debugger.IsAttached)
            {
                ExternalProcess.AttachDebugger();
            }

            ExternalProcess.AllocConsole();

            Execute(assembly.PathToAssemblyFile, assembly.Entrypoint);

            ExternalProcess.SuspendThreads(false);
        }

        private void UpdateFiles(string pathToDll)
        {
            string processPath = Path.GetDirectoryName(Process.Process.MainModule.FileName);
            CopyFile(pathToDll, "GameSharp.Core.dll", processPath);
            CopyFile(pathToDll, "GameSharp.Internal.dll", processPath);
        }

        private void CopyFile(string pathToDll, string dllName, string destination)
        {
            string source = Path.Combine(Path.GetDirectoryName(pathToDll), dllName);
            string destinationFullName = Path.Combine(destination, dllName);

            File.Copy(source, destinationFullName, true);
        }

        /// <summary>
        ///     DLL needs to be the same platform as the target process (e.g. x64 or x86).
        /// </summary>
        /// <param name="pathToDll"></param>
        /// <param name="entryPoint"></param>
        protected abstract void Inject(string pathToDll);

        /// <summary>
        ///     Execution after the DLL has been injected.
        /// </summary>
        /// <param name="pathToDll"></param>
        /// <param name="entryPoint"></param>
        protected abstract void Execute(string pathToDll, string entryPoint);
    }
}