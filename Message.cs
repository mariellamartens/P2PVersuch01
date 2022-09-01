using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PVersuch01
{
    class Message
    {
        public DateTime TimestampUnix { get; set; }
        public Byte[] Payload { get; set; }
        public string IPSender { get; set; }
        public string AliasSender { get; set; }
        public string DataFormat { get; set; }
        public string IPEmpfaenger { get; set; }
        public int Port { get; set; }
    }
}
