using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers
{
    public static class StreamHelpers
    {
        /// <summary>
        /// Reads all lines from string
        /// </summary>
        /// <param name="streamProvider"></param>
        /// <param name="encoding"></param>
        /// <example>ReadLines(() => someStream, Encoding.UTF8)</example>
        /// <remarks>
        /// Thanks to Jon Skeet
        /// origin: http://stackoverflow.com/questions/13312906/readalllines-for-a-stream-object/13312954#13312954
        /// </remarks>
        /// <returns></returns>
        //public IEnumerable<string> ReadLines(Func<Stream> streamProvider, Encoding encoding)
        //{
        //    using (var stream = streamProvider())
        //    using (var reader = new StreamReader(stream, encoding))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            yield return line;
        //        }
        //    }
        //}
    }
}
