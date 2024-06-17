using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetLib
{
    public class Listener
    {
        public Socket _listenSocket { get; private set; }

        IPEndPoint _endPoint;
        Thread _workerThread;

        bool _tirgger = false;
        int _limitUserNum;

        public Action<Socket> _enterClient;

        public Listener(IPEndPoint endPoint, int limitUserNum)
        {
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _workerThread = new Thread(Listening);

            _endPoint = endPoint;
            _limitUserNum = limitUserNum;
        }

        public void StartListener()
        {
            _tirgger = true;

            try
            {
                _listenSocket.Bind(_endPoint);
                _listenSocket.Listen(_limitUserNum);

                _workerThread.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("Listener Start Error {0}", e);
            }
        }

        public void StopListener()
        {
            _tirgger = false;
            _workerThread.Join();

            _listenSocket.Shutdown(SocketShutdown.Both);
            _listenSocket.Close();
        }

        private void Listening()
        {
            Console.WriteLine("start listening");

            while (_tirgger)
            {
                try
                {
                    Socket newClient = _listenSocket.Accept();
                    _enterClient(newClient);
                    Console.WriteLine("connect");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Listening error {0}", e);
                    break;
                }
            }

            Console.WriteLine("end listening");
        }
    }
}
