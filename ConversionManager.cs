using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFImage = System.Windows.Controls.Image;
using Image = System.Drawing.Image;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using type_converter.src;


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
            // Temporary fix for .ico files
            Uri _uri;
            if (isIco)
                _uri = new Uri("default_preview.png", UriKind.Relative);
            else
                _uri = new Uri(filePath, UriKind.Absolute);

            BitmapImage _bitmap = new BitmapImage(_uri);
            AreaImage.Source = _bitmap;
            AreaImage.Visibility = Visibility.Visible;

            DefText.Visibility = Visibility.Collapsed;

            Text.Text = Path.GetFileName(filePath);
            Text.Visibility = Visibility.Visible;
        }
    }
}
