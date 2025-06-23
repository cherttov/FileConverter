using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace type_converter
{
    public enum ImageFormat
    {
        Unknown, // Default 
        // Picture
        PNG,
        JPG,
        WEBP,
        // Audio
        MP3,
        WAV
    }

    // Creates conversion table
    class ConversionSelector
    {
        private static readonly Dictionary<ImageFormat, List<ImageFormat>> ConversionMap;

        // Constructor on run
        static ConversionSelector()
        {
            // { PNG : [JPG, WEBP ] } , { JPB : [PNG, WEBP ] }
            ConversionMap = new Dictionary<ImageFormat, List<ImageFormat>>
            {
                // Picture
                { ImageFormat.PNG, new  List<ImageFormat>{ImageFormat.JPG, ImageFormat.WEBP} },
                { ImageFormat.JPG, new  List<ImageFormat>{ImageFormat.PNG, ImageFormat.WEBP} },
                { ImageFormat.WEBP, new  List<ImageFormat>{ImageFormat.JPG, ImageFormat.PNG} },
                // Audio
                { ImageFormat.MP3, new  List<ImageFormat>{ImageFormat.WAV} },
                { ImageFormat.WAV, new  List<ImageFormat>{ImageFormat.MP3} }
            };
        }
        
        // Returns available conversions for selected format
        public static List<ImageFormat> GetAllowedConversions (ImageFormat _sourceFormat)
        {
            // PNG: ConversionMap.GetValues for PNG -> Output available values
            if (ConversionMap.TryGetValue(_sourceFormat, out var _convertibleFormats))
            {
                Debug.WriteLine($"{_sourceFormat}: {string.Join(", ", _convertibleFormats)}"); // Debug
                return _convertibleFormats;
            }

            return new List<ImageFormat>();
        }
    }
}
