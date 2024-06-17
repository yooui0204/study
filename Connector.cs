using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetLib
{
    public class Connector
    {
        public Socket _socket { get; private set; } 

        IPEndPoint _endPoint;

        public Connector(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void InitConnector(Socket s)
        {
            try
            {
                s.Connect(_endPoint);
            }
            catch(Exception e)
            {
                Console.WriteLine("init connector : {0}", e);
            }
        }
    }
}
