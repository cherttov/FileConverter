﻿using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            Reset(); // Reset input

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
                    ConversionManager.ProcessConversion(_file, ConvertToComboBox, DragDropArea_Image, DragDropArea_ImageText, DragDropArea_Text, out inputFilePath);
                }
            }
        }

        // Drag & Drop area clicked
        private void DragDropArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Reset(); // Reset input

            OpenFileDialog _openFileDialog = new OpenFileDialog();
            bool? _isFileSelected = _openFileDialog.ShowDialog();

            if (_isFileSelected == true)
            {
                string _file = _openFileDialog.FileName;
                ConversionManager.ProcessConversion(_file, ConvertToComboBox, DragDropArea_Image, DragDropArea_ImageText, DragDropArea_Text, out inputFilePath);
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
            // Check if all selected
            if (inputFilePath == null || ConvertToComboBox.SelectedItem == null)
            {
                // Debug.WriteLine("File or selection is null"); // Debug
                MessageBox.Show("Select file to convert.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ImageFormat _targetFormat = (ImageFormat)ConvertToComboBox.SelectedItem;

            // check if conversion to .ico
            if (_targetFormat == ImageFormat.ICO)
            {
                MessageBox.Show("Make sure that the original file has supported size for this conversion.", "Note", MessageBoxButton.OK);
            }

            string _outputFilePath;

            // Save file dialog
            SaveFileDialog _saveFileDialog = new SaveFileDialog
            {
                Title = "Save Converted File",
                FileName = Path.GetFileNameWithoutExtension(inputFilePath) + "." + _targetFormat.ToString().ToLower(),
                Filter = $"{_targetFormat} files|*.{_targetFormat.ToString().ToLower()}",
                DefaultExt = _targetFormat.ToString().ToLower(),
                InitialDirectory = inputFilePath
            };

            // Show save file dialog
            bool? _isPathSelected = _saveFileDialog.ShowDialog();
            if (_isPathSelected == true)
            {
                _outputFilePath = _saveFileDialog.FileName;
                _outputFilePath = Path.ChangeExtension(_outputFilePath, _targetFormat.ToString().ToLower());
            }
            else
            {
                Debug.WriteLine("Save dialog was cancelled"); // Debug
                return;
            }

            // Pass to converter
            ConversionProcessor.Convert(inputFilePath, _outputFilePath);

            // Reset input
            Reset();
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

        // Reset input
        private void Reset()
        {
            inputFilePath = null;
            ConvertToComboBox.Items.Clear();
            DragDropArea_Image.Source = null;
            DragDropArea_Image.Visibility = Visibility.Collapsed;
            DragDropArea_ImageText.Text = "";
            DragDropArea_ImageText.Visibility = Visibility.Collapsed;
            DragDropArea_Text.Visibility = Visibility.Visible;
        }

    }
}