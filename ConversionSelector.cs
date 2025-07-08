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
        BMP,
        TIFF,
        ICO,
        GIF,
        // Document
        // Tables
        // Video
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
                { ImageFormat.PNG, new  List<ImageFormat>{ ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.BMP, ImageFormat.TIFF, ImageFormat.ICO } },
                { ImageFormat.JPG, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.WEBP, ImageFormat.BMP, ImageFormat.TIFF, ImageFormat.ICO } },
                { ImageFormat.WEBP, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.JPG, ImageFormat.BMP, ImageFormat.TIFF, ImageFormat.ICO } },
                { ImageFormat.BMP, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.TIFF, ImageFormat.ICO } },
                { ImageFormat.TIFF, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.BMP, ImageFormat.ICO } },
                { ImageFormat.ICO, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.BMP, ImageFormat.TIFF } },
                { ImageFormat.GIF, new  List<ImageFormat>{ ImageFormat.PNG, ImageFormat.JPG, ImageFormat.WEBP, ImageFormat.BMP, ImageFormat.TIFF } }, // idk what to what
                // Document
                // Tables
                // Video
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
                ".bmp" => ImageFormat.BMP,
                ".tiff" or ".tif" => ImageFormat.TIFF,
                ".ico" => ImageFormat.ICO,
                ".gif" => ImageFormat.GIF,
                // Document
                // Tables
                // Video
                _ => ImageFormat.Unknown
            };
        }
    }
}
