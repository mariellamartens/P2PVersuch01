﻿using System;
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
            //erstelle TcpClient zum verschicken der Nachricht
            TcpClient client = new TcpClient(ipAdresse, port);

            //Nachricht soll als ByteArray vorliegen
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(nachricht); 

            //NetworkStream erstellen um ihn zu beschreiben zu können
            NetworkStream stream = client.GetStream();

            //nachricht auf den Datenbus schreiben
            stream.Write(data, 0, data.Length); 

            //gesendete Nachricht nochmal auf Konsole ausgeben
            Console.WriteLine("ich: {0}", nachricht);
            stream.Close();
            client.Close();
        }
    }
}
