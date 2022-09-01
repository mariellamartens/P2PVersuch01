using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace P2PVersuch01
{
    class Program
    {
        static void Main(string[] args)
        {
            //setzte im Konstruktor den Port von Anfang an, da dieser fest steht
            //da wir derzeit nur textnachrichten verschicken, wird das Dateiformat auf .txt gesetzt
            Message message = new Message()
            {
                DataFormat = ".txt",
                Port = 900
            };
            Thread thread1 = new Thread(threadAufgabe);
            thread1.Start();
            TcpSender sender = new TcpSender();
            Console.WriteLine("gib eine IP-Adresse ein:");
            string ipadresse = Console.ReadLine();

            //überprüfen ob es sich um eine echte IP-Adresse handelt
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

                //setzte vom Objekt die Ip-Adresse
                message.IPEmpfaenger = ipadresse;

                //setzte vom Objekt message den Alias
                Console.WriteLine("gib den Namen des Empfängers an:");
                message.AliasSender = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("gib deine Nachricht ein:");
                    string nachricht = Console.ReadLine();

                    //die Nachricht die übermittelt werden soll wird in einem Bytearray geschrieben
                    Byte[] payload = Encoding.ASCII.GetBytes(nachricht); 

                    //setzte vom Objekt den Payload
                    message.Payload = payload;

                    //setzte vom Objekt die aktuelle Zeit
                    DateTime localDate = DateTime.Now;
                    message.TimestampUnix = localDate;

                    //versuch meine ip-Adresse zu bekommen, da wir mehrere IP-Adressen haben, muss die richtige gefunden werden
                    //wir verbinden uns hierfür mit einem UDP-Socket und lesen dann dessen lokalen Endpunkt aus
                    string localIP = string.Empty;
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        localIP = endPoint.Address.ToString();

                        //setzte vom Objekt die IP-Empfänger auf die gefundene Ip-Adresse
                        message.IPSender = localIP;
                    }

                    //erstelle aus dem Objekt einen String
                    string stringjson = JsonConvert.SerializeObject(message);

                    //starte die Methode senden mit der IP-Empfänger, dem stringjson und dem port
                    sender.senden(message.IPEmpfaenger, stringjson, message.Port);

                }
            }
        }
        public static void threadAufgabe()
        {
            TcpEmpfaenger empfaenger = new TcpEmpfaenger();
            int port = 900;
            empfaenger.empfangen(port);
        } 
    }
}
