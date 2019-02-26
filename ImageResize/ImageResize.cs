using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace serverlesslibrary
{
    public static class ImageResize
    {
        private static readonly System.Drawing.Size size = new System.Drawing.Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));


        [FunctionName("ImageResize")]
        [return: Blob("thumbnails/{name}")]
        public static void Run([BlobTrigger("images/{name}", Connection = "ImageRepository")]Stream original, string name)
        {
            var resized = new MemoryStream();
            using (var image = Image.Load(original))
            {
                image
                    //.Resize(size)
                    .SaveAsJpeg(resized);
            }
        }


        private static int EnvAsInt(string name) => int.Parse(Env(name));
        private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}