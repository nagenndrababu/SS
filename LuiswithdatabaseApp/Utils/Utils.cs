using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace LuiswithdatabaseApp.Utils
{
    public sealed class Utils
    {
        public static void CheckEmptyString(ref string sValue)
        {
            if (string.IsNullOrEmpty(sValue))
            {
                sValue = "0";
            }
        }

        public static string GetAppKey(string key)
        {
            string sReturn = string.Empty;
            try
            {
                sReturn = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(sReturn))
                {
                   // Logger.Log.Info(string.Format("GetAppKey : {0} Key Misssing ", key));
                }
            }
            catch (Exception exCatch)
            {
                return exCatch.ToString();
                //Logger.Log.Error(string.Format("GetAppKey : {0} Key Misssing ", key), exCatch);
            }

            return sReturn;
        }
        //public static string GetAppCacheKey(string key)
        //{
        //    string sReturn = string.Empty;
        //    try
        //    {
        //        sReturn = GetAppKey(key);
        //        if (string.IsNullOrEmpty(sReturn))
        //        {
        //            sReturn = MobileConstants.DEFAULT_CACHE_TIME;
        //        }
        //    }
        //    catch (Exception exCatch)
        //    {
        //        Logger.Log.Error(string.Format("GetAppKey : {0} Key Misssing ", key), exCatch);
        //    }

        //    return sReturn;
        //}
        public static string GetConnectionString(string key)
        {
            string sReturn = string.Empty;
            try
            {
                sReturn = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                if (string.IsNullOrEmpty(sReturn))
                {
                   // Logger.Log.Info(string.Format("GetConnectionString : {0} Key Misssing ", key));
                }
            }
            catch (Exception exCatch)
            {
                return exCatch.ToString();
            }

            return sReturn;
        }
        public static void ReturnsOne(ref string one)
        {
            if (string.IsNullOrEmpty(one))
            {
                one = "1";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
     

      

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}