using System;
using System.IO;
using System.Drawing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using ImageProcessor;

namespace serverlesslibrary
{
    public static class ImageResize
    {
        private static readonly Size size = new Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

        [FunctionName("ImageResize")]
        [return: Blob("thumbnails/{name}")]
        public static void Run([BlobTrigger("images/{name}", Connection = "ImageRepository")]Stream original, string name)
        {
            var resized = new MemoryStream();
            using (var imageFactory = new ImageFactory())
            {
                imageFactory
                    .Load(original)
                    .Resize(size)
                    .Save(resized);
            }
        }


        private static int EnvAsInt(string name) => int.Parse(Env(name));
        private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}