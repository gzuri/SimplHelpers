using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers
{
    public class IpHelpers
    {
        /// <summary>
        /// Checks if IP address is in IP range
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        public static bool IsInRange(IPAddress address, IPAddress lowerBound, IPAddress upperBound)
        {
            var addressBytes1 = address.GetAddressBytes();
            var addressBytes2 = lowerBound.GetAddressBytes();
            var addressBytes3 = upperBound.GetAddressBytes();
            var flag1 = true;
            var flag2 = true;
            for (var index = 0; index < addressBytes2.Length && (flag1 || flag2); ++index)
            {
                if (flag1 && (int)addressBytes1[index] < (int)addressBytes2[index] || flag2 && (int)addressBytes1[index] > (int)addressBytes3[index])
                    return false;
                flag1 &= (int)addressBytes1[index] == (int)addressBytes2[index];
                flag2 &= (int)addressBytes1[index] == (int)addressBytes3[index];
            }
            return true;
        }
    }
}
