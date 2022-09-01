using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2PVersuch01
{
    class TcpSender
    {
        public void senden(string ipAdresse, string nachricht, int port)
        {
            //evtl im try catch block
            TcpClient client = new TcpClient(ipAdresse, port);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(nachricht); //Nachricht soll als ByteArray vorliegen
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length); //nachricht auf den Datenbus schreiben
            Console.WriteLine("gesendet: {0}", nachricht);
            stream.Close();
            client.Close();
        }
    }
}
