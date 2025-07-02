using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        // Drag & Drop area dropped
        private void DragDropArea_Drop(object sender, DragEventArgs e)
        {
            inputFilePath = null; // Reset input file path

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Check if more than 1 file has been selected
                if (_files.Length > 1)
                {
                    // Debug.WriteLine("ERROR: MainWindow.xaml.cs : Line 43\n  > More than 1 file selected"); // Debug
                    MessageBox.Show("Select only 1 file to convert.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string _file = _files[0];
                    ConversionProcessor.ProcessConversion(_file, ConvertToComboBox, out inputFilePath);
                }
            }
        }

        // Drag & Drop area clicked
        private void DragDropArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputFilePath = null; // Reset input file path

            OpenFileDialog _fileDialog = new OpenFileDialog();
            bool? _isFileSelected = _fileDialog.ShowDialog();

            if (_isFileSelected == true)
            {
                string _file = _fileDialog.FileName;
                ConversionProcessor.ProcessConversion(_file, ConvertToComboBox, out inputFilePath);
            }
            else
            {
                // No file selected (null || false)
                Debug.WriteLine("No file selected"); // Debug
            }
        }

        // Convert button
        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputFilePath == null || ConvertToComboBox.SelectedItem == null)
            {
                // Debug.WriteLine("File or selection is null"); // Debug
                MessageBox.Show("Select file to convert.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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