using System.IO;
using System.Windows;
using System.Windows.Controls;
using WPFImage = System.Windows.Controls.Image;
using System.Windows.Media.Imaging;


namespace type_converter
{
    class ConversionManager
    {
        public static void ProcessConversion(string _file, ComboBox ConvertToComboBox, WPFImage AreaImage, TextBlock Text, TextBlock DefText, out string inputFilePath)
        {
            inputFilePath = null!;

            string _filePath = _file;
            string _fileType = Path.GetExtension(_file).ToLower();

            var _format = ConversionSelector.ParseFormat(_fileType);

            if (_format != ImageFormat.Unknown)
            {
                bool _isIco = _format == ImageFormat.ICO; // Temporary
                GetSetThumbnail(_filePath, AreaImage, Text, DefText, _isIco);

                List<ImageFormat> _availableConversions = ConversionSelector.GetAllowedConversions(_format);
                ConvertToComboBox.Items.Clear();
                foreach (ImageFormat _row in _availableConversions)
                {
                    ConvertToComboBox.Items.Add(_row);
                }

                inputFilePath = _filePath;
            }
            else
            {
                // Debug.WriteLine("Unknown file type");
                MessageBox.Show("Unknown file type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void GetSetThumbnail(string filePath, WPFImage AreaImage, TextBlock Text, TextBlock DefText, bool isIco)
        {
            BitmapImage _bitmap = new BitmapImage();

            _bitmap.BeginInit();

            // Temporary fix for .ico files
            Uri _uri;
            if (isIco)
                _uri = new Uri("assets\\default_preview.png", UriKind.Relative);
            else
                _uri = new Uri(filePath, UriKind.Absolute);

            _bitmap.UriSource = _uri;

            _bitmap.CacheOption = BitmapCacheOption.OnLoad; // So it doesn't lock the file

            _bitmap.EndInit();

            _bitmap.Freeze();

            // Set image
            AreaImage.Source = _bitmap;
            AreaImage.Visibility = Visibility.Visible;

            // Hide default text
            DefText.Visibility = Visibility.Collapsed;

            // Set new text
            Text.Text = Path.GetFileName(filePath);
            Text.Visibility = Visibility.Visible;
        }
    }
}
