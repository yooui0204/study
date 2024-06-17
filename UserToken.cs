using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetLib
{
    public class UserToken
    {
        public Socket _socket { get; private set; }
        public int _macHash { get; private set; }

        public UserToken(Socket socket, int hash)
        {
            _socket = socket;
            _macHash = hash;
        }
    }
}
