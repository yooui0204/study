using System.Net.Sockets;

namespace NetLib
{
    public abstract class Session
    {
        protected List<byte> _recvBuffer = new List<byte>();

        public SocketAsyncEventArgs _recv { get; private set; }
        public SocketAsyncEventArgs _send { get; private set; }                                                 

        public void InitSession(SocketAsyncEventArgs recv, SocketAsyncEventArgs send)
        {
            _recv = recv;
            _send = send;

            OnStart();
            StartRecive(recv);
        }

        #region network part

        public void StartRecive(SocketAsyncEventArgs args)
        {
            try
            {
                var token = args.UserToken as UserToken;
     
                bool tmp = token._socket.ReceiveAsync(args);

                if (tmp == false)
                    CompleteRecive(null, args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"error start recv \n : {e}");
                OnClose();
            }
        }

        protected void CompleteRecive(object? sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                OnRecive(args);
                StartRecive(args);
            }
            else
            {
                Console.WriteLine("error complete recv : " + args.SocketError.ToString());
                OnClose();
            }
        }

        public void StartSend(SocketAsyncEventArgs args)
        {
            try
            {
                var token = args.UserToken as UserToken;

                bool tmp = token._socket.SendAsync(args);

                if (tmp == false)
                {
                    CompleteSend(null, args);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error start send : {e}");
                OnClose();
            }
        }

        protected void CompleteSend(object? sender, SocketAsyncEventArgs args)
        {
            try
            {
                if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    OnSend(args);
                }
                else
                {
                    Console.WriteLine("error complete send : " + args.SocketError.ToString());
                    OnClose();
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine(e.ToString());
                OnClose();
            }
        }

        #endregion

        #region legacy part

        protected abstract void OnClose();
        protected abstract void OnStart();
        protected abstract void OnSend(SocketAsyncEventArgs send);
        protected abstract void OnRecive(SocketAsyncEventArgs recv);

        #endregion
    }
}
