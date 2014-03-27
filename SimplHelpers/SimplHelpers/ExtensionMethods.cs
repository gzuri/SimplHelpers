using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers
{
    public static class ExtensionMethods
    {
        public static void CopyTo(this Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}
