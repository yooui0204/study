using System.Collections.Concurrent;
using System.Net.Sockets;

namespace NetLib
{
    public class ArgsPool
    {
        BufferMananger _bufferMananger;
        ConcurrentBag<SocketAsyncEventArgs> _argsBag;

        public ArgsPool(int limitUserNum, int sizeOfBuffer)
        {
            _bufferMananger = new BufferMananger(limitUserNum, sizeOfBuffer);
            _argsBag = new ConcurrentBag<SocketAsyncEventArgs>();

            InitPool(limitUserNum, sizeOfBuffer);
        }

        private void InitPool(int limitUserNum, int sizeOfBuffer)
        {
            for(int i=0; i< limitUserNum; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                _bufferMananger.GetBuffer(args, sizeOfBuffer);
                _argsBag.Add(args);
            }
        }

        public SocketAsyncEventArgs GetArgs()
        {
            if (_argsBag.TryTake(out var args))
                return args;
            else
            {
                Console.WriteLine("pool is empty");
                return null;
            }
        }

        public void FreeArgs(SocketAsyncEventArgs args)
        {
            args.Dispose();
            args.UserToken = null;
            _argsBag.Add(args);
        }
    }
}
