using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace NetLib
{
    public class PacketManager
    {
        public static byte[] PacketToStream(Packet packet)
        {
            byte[] id = BitConverter.GetBytes(packet._id);
            byte[] time = BitConverter.GetBytes(packet._time);
            byte[] data = SerializeData(packet._data);

            byte[] packetArray = new byte[Constant.FIXED_PACKET_SIZE];

            Array.Copy(id, 0, packetArray, 0, id.Length);
            Array.Copy(time, 0, packetArray, id.Length, time.Length);
            Array.Copy(data, 0, packetArray, id.Length + time.Length, data.Length);

            return packetArray;
        }

        public static Packet StreamToPacket(byte[] packet)
        {
            short lengthOfID = 2;
            int lengthOfTime = 4;
            int lengthOfData = Constant.FIXED_PACKET_SIZE - 6;

            byte[] id = new byte[lengthOfID];
            byte[] time = new byte[lengthOfTime];
            byte[] data = new byte[lengthOfData];

            Array.Copy(packet, 0, id, 0, lengthOfID);
            Array.Copy(packet, lengthOfID, time, 0, lengthOfTime);
            Array.Copy(packet, lengthOfID + lengthOfTime, data, 0, lengthOfData);

            return new Packet(BitConverter.ToInt16(id), BitConverter.ToSingle(time), DeserializeData(data));
        }

        private static byte[] SerializeData(Object data)
        {
            byte[] byteArray;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                byteArray = ms.ToArray();
            }

            return byteArray;
        }

        private static object DeserializeData(byte[] data)
        {
            object deserializedObject;

            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                deserializedObject = bf.Deserialize(ms);
            }

            return deserializedObject;
        }
    }
}
