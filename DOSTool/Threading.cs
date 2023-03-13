using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DOS
{
    public static class Threading
    {
        public static List<Thread> threadList;

        public static void GeneratePingThreads()
        {
            ToggleThreadLife();
            threadList = new List<Thread>();

            for (int i = 0; i < Network.instances; i++)
            {
                Thread thread = new Thread(new ThreadStart(Network.Ping));
                thread.Name = $"Thread - {i}";
                thread.IsBackground = true;
                threadList.Add(thread);
                thread.Start();
            }

            Action action = delegate { UI.cancelButton.IsEnabled = true; };
            UI.invokeTarget.Dispatcher.Invoke(action);

            action = delegate { UI.ChangeAppTitle("Running"); };
            UI.invokeTarget.Dispatcher.Invoke(action);
        }

        public static void GenerateSpawnThread()
        {
            UI.ChangeAppTitle("Starting");

            Thread thread = new Thread(new ThreadStart(GeneratePingThreads));
            thread.Name = "Generator Thread";
            thread.IsBackground = true;
            thread.Start();
        }

        public static void GenerateCancelThread()
        {
            Action cancelAction = delegate { UI.ChangeAppTitle("Stopping"); };
            UI.invokeTarget.Dispatcher.Invoke(cancelAction);

            cancelAction = delegate { UI.cancelButton.IsEnabled = false; };
            UI.invokeTarget.Dispatcher.Invoke(cancelAction);

            Thread thread = new Thread(new ThreadStart(ExecuteThreadCancel));
            thread.Name = "Cancel Thread";
            thread.IsBackground = true;
            thread.Start();
        }

        private static void ExecuteThreadCancel()
        {
            ToggleThreadLife();
            Thread.Sleep(Network.timeout);

            Action cancelAction = delegate { UI.ToggleElementModification(); };
            UI.invokeTarget.Dispatcher.Invoke(cancelAction);

            cancelAction = delegate { UI.ChangeAppTitle("Idle"); };
            UI.invokeTarget.Dispatcher.Invoke(cancelAction);
        }

        public static void ToggleThreadLife() { Network.stopFlag = !Network.stopFlag; }
    }
}
