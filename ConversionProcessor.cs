using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace type_converter
{
    public class ConversionProcessor
    {
        public static void Convert(string inputPath, string outputPath)
        {
            using (MagickImage image = new MagickImage(inputPath))
            {
                var extension = Path.GetExtension(outputPath).ToLower();

                // Tested only PNG -> ICO
                if (extension == ".ico")
                {
                    using (var collection = new MagickImageCollection())
                    {
                        uint[] sizes = { 256, 64, 32, 16 };

                        foreach (uint size in sizes)
                        {
                            var _iconImage = image.Clone();

                            _iconImage.BackgroundColor = MagickColors.Transparent;

                            _iconImage.FilterType = FilterType.Lanczos;

                            _iconImage.Resize(size, size);

                            _iconImage.Format = MagickFormat.Ico;

                            collection.Add(_iconImage);
                        }

                        collection.Write(outputPath);
                    }
                }
                else
                {
                    image.Write(outputPath);
                }
            }
        }
    }
}
/* I thought I was testing this code and was like "That's so much simpler than FFmpeg" and then realized,
   I didn't actually change call in ConvertButton_Click, so it is untested, eventually. I hope Natans tests it... */
