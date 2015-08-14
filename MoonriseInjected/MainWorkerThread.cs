using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MoonriseInjected
{
    public class MainWorkerThread
    {

        public static void Run() {
            Injector.LogPlugin("Working thread started");

            UL.Kaiju.Lobby.Common.AttemptLoginArgs loginArgs = UL.Net.Client.LoginHelper.NewAttemptLoginArgs();
            Injector.LogPlugin("Debug login args:");
            Injector.LogPlugin("version: "+loginArgs.dataVersion);
            Injector.LogPlugin("deviceId: " + loginArgs.deviceId);
            Injector.LogPlugin("deviceModel: " + loginArgs.deviceModel);
            Injector.LogPlugin("locale: " + loginArgs.locale);
            Injector.LogPlugin("platform: " + loginArgs.platform);
            Injector.LogPlugin("protocol: " + loginArgs.protocol);

            StartTimedDelegate(5000, TryDumpLocalisationData);
        }


        private static void TryDumpLocalisationData(object sender, ElapsedEventArgs args)
        {
            Timer timer = (Timer) sender;
            TextDb textDb = UL.Localization.Localizer.s_textDb;
            if (textDb == null) {
                Injector.LogPlugin("Waiting for textDb to be ready..");
                return;
            }

            timer.Stop();

            Injector.LogPlugin("Dumping textDb (" + textDb.m_ids.Length + " strings)");
            for (int i = 0; i < textDb.m_ids.Length; i++) {
                uint id = (uint)textDb.m_ids[i];
                if (!textDb.m_idToIndex.ContainsKey(id)) {
                    continue;
                }

                int index = textDb.m_idToIndex[id];
                int offStart = textDb.m_offsets[index];
                int offEnd = textDb.m_offsets[index + 1];
                int len = offEnd - offStart;
                string entry = textDb.m_strings.Substring(offStart, len);

                Injector.LogPlugin(string.Format("{0}: {1}", id, entry));
            }

            Injector.LogPlugin("Done");
        }

        private static void StartTimedDelegate(int interval, ElapsedEventHandler handler) {
            Timer t = new Timer(interval);
            t.Elapsed += handler;
            t.Start();
        }

    }

}