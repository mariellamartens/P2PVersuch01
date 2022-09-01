using System;
using System.Threading;

namespace P2PVersuch01
{
    class Program
    {
        static void Main(string[] args)
        {
            Start:
            Thread thread1 = new Thread(threadAufgabe);
            thread1.Start();
            TcpSender sender = new TcpSender();
            Console.WriteLine("gib eine IP-Adresse ein:");
            string ipadresse = Console.ReadLine();
            bool IsIP(string ipadresse)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(ipadresse, @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$\b");
            }
            //string ipadresse = System.Net.IPAddress.Loopback.ToString(); //statt Loopback die IpAdresse des anderen Laptops (beide gleichzeitig das Programm laufen lassen)
            //sender.senden(ipadresse, "hallo", 13000);
            while (!IsIP(ipadresse))
            {
                Console.WriteLine("gib eine IP-Adresse ein:");
                ipadresse = Console.ReadLine();
            }
            if (IsIP(ipadresse))
            {
                while (true)
                {
                    Console.WriteLine("gib deine Nachricht ein:");
                    string nachricht = Console.ReadLine();
                    sender.senden(ipadresse, nachricht, 13000);

                }
            }
        }
        public static void threadAufgabe()
        {
            TcpEmpfaenger empfaenger = new TcpEmpfaenger();
            int port = 13000;
            empfaenger.empfangen(port);
        } 
    }
}
