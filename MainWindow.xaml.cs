using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using type_converter.src;
using Path = System.IO.Path;

namespace type_converter
{
    public partial class MainWindow : Window
    {
        // Drag & Drop area style variables
        private readonly SolidColorBrush dragDropBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99BAC2DE"));
        private readonly SolidColorBrush dragDropBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF33334C"));

        // File path in which original file is in
        private string? inputFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Drag & Drop area
        private void DragDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Check if more than 1 file has been selected
                if (_files.Length > 1)
                {
                    Debug.WriteLine("ERROR: MainWindow.xaml.cs : Line 33\n  > More than 1 file selected"); // Debug
                }
                else
                {
                    string _filePath = _files[0];
                    string _fileName = Path.GetFileName(_files[0]);
                    string _fileType = Path.GetExtension(_files[0]).ToLower();

                    ImageFormat _format = _fileType switch
                    {
                        // Image
                        ".png" => ImageFormat.PNG,
                        ".jpg" or ".jpeg" => ImageFormat.JPG,
                        ".webp" => ImageFormat.WEBP,
                        // Document
                        ".pdf" => ImageFormat.PDF,
                        ".docx" => ImageFormat.DOCX,
                        _ => ImageFormat.Unknown
                    };

                    if (_format != ImageFormat.Unknown )
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
                        Debug.WriteLine("Unknown file type"); //  Debug
                    }
                }
            }
        }

        // Convert button
        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputFilePath == null || ConvertToComboBox.SelectedItem == null)
            {
                Debug.WriteLine("File or selection is null"); // Debug
                return;
            }

            ImageFormat _targetFormat = (ImageFormat)ConvertToComboBox.SelectedItem;

            string _outputFilePath = System.IO.Path.ChangeExtension(inputFilePath, _targetFormat.ToString().ToLower());

            FFmpeg.Convert(inputFilePath, _outputFilePath);
        }


        // Drag & Drop area styles
        private void DragDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                border.BorderBrush = dragDropBrush;
                border.Background = dragDropBackground;
            }
        }
        private void DragDropArea_DragLeave(object sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                border.ClearValue(Border.BorderBrushProperty);
                border.ClearValue(Border.BackgroundProperty);
            }
        }

    }
}