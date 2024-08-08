using SkiaSharp;
using System.Drawing;
using System.IO;

namespace pdf_to_png_poc
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var pdf_bytes = File.ReadAllBytes("example.pdf");
            using var fs = File.OpenRead("example.pdf");
            using var sk_bitmap = PDFtoImage.Conversion.ToImage(fs, Index.Start);
            using SKImage sk_image = SKImage.FromBitmap(sk_bitmap);
            using var encoded = sk_image.Encode();
            using Stream stream = encoded.AsStream();

            using (var fileStream = File.Create("output.png"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            Console.WriteLine("All done!");
        }
    }
}
