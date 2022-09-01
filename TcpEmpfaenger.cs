using Newtonsoft.Json;
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
            //wir wollen von allen empfangen können, deshalb IPAddress.Any
            TcpListener empfaenger = new TcpListener(System.Net.IPAddress.Any, port); 

            //starten des TCPListeners
            empfaenger.Start();

            //zum Lesen der Nachricht
            Byte[] bytes = new Byte[256]; 
            string nachricht = null;

            while (true)
            {
                empfaenger.Start();

                //erstellen eines TCPClients
                TcpClient client = empfaenger.AcceptTcpClient();
                nachricht = null;

                //Daten empfangen
                NetworkStream stream = client.GetStream();
                int i;

                //den empfangenen Bytearray durchgehen
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    //alles was in dem Bytearray drin steht zu den ursprünglichen Charaktern zurück konvertieren
                    nachricht = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    //aus dem String wieder ein Objekt der Klasse Message machen
                    Message message = JsonConvert.DeserializeObject<Message>(nachricht);

                    //aus dem Bytearray des Payloads sollen auch wieder die ursprünglichen Charakter hergestellt werden
                    string payload = Encoding.UTF8.GetString(message.Payload, 0, message.Payload.Length);

                    //Nachricht bzw. Payload ausgeben
                    Console.WriteLine("Freund: {0}", payload);
                }
                client.Close();
                empfaenger.Stop();
            }
        }
    }
}
