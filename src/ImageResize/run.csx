#r "System.Drawing"

using System;
using System.Drawing;
using ImageProcessor;

private static Size size = new Size(Env("ImageResize-Width"), Env("ImageResize-Height"))

public static void Run(Stream original, Stream resized, TraceWriter log)
{
    using (var imageFactory = new ImageFactory())
    {
        imageFactory
            .Load(original)
            .Resize(size)
            .Save(resized);
    }
}

private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);