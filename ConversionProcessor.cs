using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace type_converter
{
    class ConversionProcessor
    {
        public static void ProcessConversion(string _file, ComboBox ConvertToComboBox, out string inputFilePath)
        {
            inputFilePath = null;

            string _filePath = _file;
            string _fileType = Path.GetExtension(_file).ToLower();

            var _format = ConversionSelector.ParseFormat(_fileType);

            if (_format != ImageFormat.Unknown)
            {
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
    }
}
