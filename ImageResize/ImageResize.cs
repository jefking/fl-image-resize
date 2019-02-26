using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace serverlesslibrary
{
    public static class ImageResize
    {
        private static readonly Size size = new Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

        [FunctionName("ImageResize")]
        public static void Run([BlobTrigger("images/{name}", Connection = "ImageRepository")] Stream original, [Blob("thumbnails/{name}", FileAccess.Write)] Stream resized)
        {
            var image = Image.FromStream(original);
            var thumbnail = image.GetThumbnailImage(size.Width, size.Height, null, IntPtr.Zero);
            thumbnail.Save(resized, ImageFormat.Jpeg);
        }
        private static int EnvAsInt(string name) => int.Parse(Env(name));
        private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}