using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace type_converter
{
    // List of formats
    public enum ImageFormat
    {
        Unknown, // Default 
        // Image
        PNG,
        JPG,
        WEBP,
        ICO
        // Document
        // Tables
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
                { ImageFormat.PNG, new  List<ImageFormat>{ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.ICO} },
                { ImageFormat.JPG, new  List<ImageFormat>{ImageFormat.PNG, ImageFormat.WEBP, ImageFormat.ICO } },
                { ImageFormat.WEBP, new  List<ImageFormat>{ImageFormat.JPG, ImageFormat.PNG, ImageFormat.ICO } },
                { ImageFormat.ICO, new  List<ImageFormat>{ImageFormat.PNG, ImageFormat.JPG, ImageFormat.WEBP} },
                // Document
                // Tables
            };
        }
        
        // Returns available conversions for selected format
        public static List<ImageFormat> GetAllowedConversions (ImageFormat _sourceFormat)
        {
            // PNG: ConversionMap.GetValues for PNG -> Output available values
            if (ConversionMap.TryGetValue(_sourceFormat, out var _convertibleFormats))
            {
                return _convertibleFormats;
            }

            return new List<ImageFormat>();
        }

        // Format parser
        public static ImageFormat ParseFormat(string _format)
        {
            return _format.ToLower() switch
            {
                // Image
                ".png" => ImageFormat.PNG,
                ".jpg" or ".jpeg" => ImageFormat.JPG,
                ".webp" => ImageFormat.WEBP,
                ".ico" => ImageFormat.ICO,
                // Document
                // Tables
                _ => ImageFormat.Unknown
            };
        }
    }
}
