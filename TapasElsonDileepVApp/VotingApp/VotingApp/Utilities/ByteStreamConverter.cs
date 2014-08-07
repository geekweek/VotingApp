using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace VotingApp.Utilities
{
    public class ByteStreamConverter
    {
        public static byte[] StreamToByte(Stream stream)
        {
            if (stream == null)
                return null;
            MemoryStream target = new MemoryStream();
            stream.CopyTo(target);
            byte[] data = target.ToArray();
            return data;
        }

        public static Stream ByteToStream(byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}