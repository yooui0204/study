using System.Collections.Concurrent;
using System.Net.Sockets;

namespace NetLib
{
    internal class BufferMananger
    {
        byte[] _buffer;
        ConcurrentBag<int> _offsetBag = new ConcurrentBag<int>();

        public BufferMananger(int limitUserNum, int sizeOfBuffer)
        {
            _buffer = new byte[limitUserNum * sizeOfBuffer];
        }

        public void GetBuffer(SocketAsyncEventArgs args, int sizeOfBuffer)
        {
            int offset;
            _offsetBag.TryTake(out offset);

            args.SetBuffer(_buffer, offset, sizeOfBuffer);
        }
    }
}