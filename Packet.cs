namespace NetLib
{
    public class Packet
    {
        public short _id { get; private set; }
        public float _time { get; private set; }
        public object _data { get; private set; }

        public Packet(short id, float time, object data)
        {
            _id = id;
            _time = time;
            _data = data;
        }
    }
}
