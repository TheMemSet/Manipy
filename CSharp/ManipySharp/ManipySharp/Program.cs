using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ManipySharp
{
    class Program
    {
        static void Grayscale(ref Bitmap image)
        {
            for (int i = 0;i < image.Width;i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    Int32 luminosity = (Int32)(pixel.R * 0.2126 + pixel.G * 0.7152 + pixel.B * 0.0722);

                    pixel = Color.FromArgb(luminosity, luminosity, luminosity);

                    image.SetPixel(i, j, pixel);
                }
            } 
        }

        static void Invert(ref Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    image.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
        }

        static void Resize(ref Bitmap image, Size newSize)
        {
            image = new Bitmap(image, newSize);
        }
        
        static void Main(string[] args)
        {
            Bitmap image = new Bitmap(args[args.Length - 2]);

            Boolean end = false;

            for (int i = 0;i < args.Length && !end;i++)
            {
                switch (args[i].ToLower())
                {
                    case "grayscale":
                        Grayscale(ref image);
                        break;
                    case "invert":
                        Invert(ref image);
                        break;
                    case "resize":
                        Size newSize = new Size();
                        newSize.Width = Convert.ToInt32(args[i + 1]);
                        newSize.Height = Convert.ToInt32(args[i + 2]);
                        Resize(ref image, newSize);
                        i += 2;
                        break;
                    case "rotatecw":
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case "rotateccw":
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case "fliphorizontal":
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case "flipvertical":
                        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                    default:
                        end = true;
                        break;
                } 
            }
            image.Save(args[args.Length - 1]);
        }
    }
}
