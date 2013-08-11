using System;
using System.IO;
using AltSerialize;

namespace Inspire.Shared
{
    public static class SerializationHelper
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {

            if (obj == null)
                return null;


            //Serialize into the stream
            //Serializer.NonGeneric.Serialize(ms, obj);
            var buffer = Serializer.Serialize(obj);

            return buffer;
        }
        // Convert a byte array to an Object
        public static  Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
 
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            //var obj = Serializer.NonGeneric.Deserialize(type, memStream);


            byte[] buffer = new byte[memStream.Length];
            memStream.Read(buffer, 0, (int) memStream.Length);
            var obj = Serializer.Deserialize(buffer);
            
            return obj;
        }

    }
}
