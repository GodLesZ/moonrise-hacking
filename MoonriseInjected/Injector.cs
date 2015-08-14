using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UL.Net.Client;
using UnityEngine;

namespace MoonriseInjected {
    public class Injector {
        private const string LOG_TAG = "[GodLesZ]";
        private static string PluginLogPath;

        public Injector()
        {
            PluginLogPath = Application.dataPath + @"\godlesz.log";
            try { if (File.Exists(PluginLogPath)) File.Delete(PluginLogPath); } catch { }

            LogPlugin("Plugin loaded");

            Debug.developerConsoleVisible = true;
            LogUnity("Injection successfull");

            Thread t = new Thread(new ThreadStart(MainWorkerThread.Run));
            t.Start();
        }



        public static void LogUnity(string line) {
            Debug.Log(string.Format("{0} {1}", LOG_TAG, line));
        }

        public static void LogPlugin(string line) {
            File.AppendAllText(PluginLogPath, string.Format("{0}[{1}] {2}{3}", LOG_TAG, DateTime.Now, line, Environment.NewLine));
        }
    }
}
