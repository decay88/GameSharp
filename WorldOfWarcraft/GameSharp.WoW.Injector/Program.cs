using GameSharp.External;
using GameSharp.External.Injection;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GameSharp.WoW.Injector
{
    class Program
    {
        static void Main(string[] args)
        {
            // The process we are injecting into.
            GameSharpProcess process = new GameSharpProcess(Process.GetProcessesByName("wow").FirstOrDefault());

            if (process == null)
            {
                throw new Exception("Process not found.");
            }

            // A simple RemoteThreadInjector.
            IInjection injector = new RemoteThreadInjection(process);

            // Inject the DLL and executes the entrypoint.
            string pathToDll = Path.Combine(Environment.CurrentDirectory, "GameSharp.WoW.dll");
            injector.InjectAndExecute(new Injectable(pathToDll, "Main"), attach: false);
        }
    }
}
