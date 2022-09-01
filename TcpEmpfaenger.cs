using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2PVersuch01
{
    class TcpEmpfaenger
    {
        public void empfangen(int port)
        {
            TcpListener empfaenger = new TcpListener(System.Net.IPAddress.Any, port); //wir wollen von allen empfangen können, deshalb IPAddress.Any
            empfaenger.Start();
            Byte[] bytes = new Byte[256]; //zum Lesen der Nachricht
            string nachricht = null;
            while (true)
            {
                Console.WriteLine("Warten auf Verbindung...");
                TcpClient client = empfaenger.AcceptTcpClient();
                Console.WriteLine("Verbunden!");
                nachricht = null;
                NetworkStream stream = client.GetStream();
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    nachricht = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("empfangen: {0}", nachricht);
                }
                client.Close();
                empfaenger.Stop();
            }
        }
    }
}
