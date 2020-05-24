using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HelperApplication
{
    public static class ImageConverter
    {
        public static byte[] ConvertImageToByteArray(this System.Drawing.Image image)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public static Image ConvertByteArrayToImage(this byte[] btArray)
        {
            using(MemoryStream ms = new MemoryStream(btArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
 