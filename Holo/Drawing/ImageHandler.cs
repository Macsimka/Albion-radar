using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;

namespace Holo.Drawing;

public static class ImageHandler
{
    // Thread safe, we only read from it
    private static readonly Dictionary<string, Bitmap> _images = [];

    public static void Load(RenderTarget target)
    {
        Type resourceType = typeof(Properties.Resources);

        PropertyInfo[] properties = resourceType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(System.Drawing.Bitmap))
            {
                using System.Drawing.Bitmap image = (System.Drawing.Bitmap)property.GetValue(null, null);

                if (image == null)
                    continue;

                _images.Add(property.Name.ToLower(), Load(target, image));
            }
        }
    }

    private static Bitmap Load(RenderTarget target, System.Drawing.Bitmap image)
    {
        using System.Drawing.Bitmap bmp = new(image, new Size(image.Width / 8, image.Height / 8));

        BitmapData bmpData = bmp.LockBits(new(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        using DataStream stream = new(bmpData.Scan0, bmpData.Stride * bmpData.Height, true, false);
        PixelFormat pFormat = new(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied);
        BitmapProperties bmpProps = new(pFormat);

        Bitmap result = new(target, new(bmp.Width, bmp.Height), stream, bmpData.Stride, bmpProps);
        bmp.UnlockBits(bmpData);

        return result;
    }

    public static Bitmap GetImage(string name)
    {
        if (_images.TryGetValue(name, out Bitmap image))
            return image;

        MainForm.Log($"Can't find image: {name}");
        return null;
    }
}
