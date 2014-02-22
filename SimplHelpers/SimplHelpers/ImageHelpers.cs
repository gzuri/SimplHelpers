using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SimplHelpers
{
    public static class ImageHelpers
    {
        public static bool GetImageFromBase64(string imageInBase64, out Image image)
        {
            image = null;
            try
            {
                byte[] bytes = Convert.FromBase64String(imageInBase64);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                return true;
            }
            catch 
            {
            }
            return false;
        }
    }
}
