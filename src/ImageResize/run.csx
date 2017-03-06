#r "System.Drawing"

using System;
using System.Drawing;
using ImageProcessor;

public static void Run(Stream original, Stream resized, TraceWriter log)
{
    var width = int.Parse(Env("ImageResize-Width"));
    var height = int.Parse(Env("ImageResize-Height"));
    var size = new Size(width, height);

    using (var imageFactory = new ImageFactory())
    {
        imageFactory
            .Load(original)
            .Resize(size)
            .Save(resized);
    }
}

private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);