using System;
using System.Threading;

namespace P2PVersuch01
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(threadAufgabe);
            thread1.Start();
            TcpSender sender = new TcpSender();
            string ipadresse = ("192.168.1.245");
            //string ipadresse = System.Net.IPAddress.Loopback.ToString(); //statt Loopback die IpAdresse des anderen Laptops (beide gleichzeitig das Programm laufen lassen)
            sender.senden(ipadresse, "hallo", 13000);
        }
        public static void threadAufgabe()
        {
            TcpEmpfaenger empfaenger = new TcpEmpfaenger();
            int port = 13000;
            empfaenger.empfangen(port);
        } 
    }
}
