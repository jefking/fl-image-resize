using System;
using System.IO;
using System.Drawing;

using sharp = SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
            using (var image = sharp.Image.Load(original))
            {
                image.Mutate(x => x
                    .Resize(size.Width, size.Height)
                );
                image.Save(resized, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
            }
        }

        private static int EnvAsInt(string name) => int.Parse(Env(name));
        private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}