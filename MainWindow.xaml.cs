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
using Path = System.IO.Path;

namespace type_converter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConversionSelector.GetAllowedConversions(ImageFormat.PNG); // Placeholder
        }

        // Drag & Drop Area
        private void DragDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Check if more than 1 file has been selected
                if (_files.Length > 1)
                {
                    Debug.WriteLine("ERROR: MainWindow.xaml.cs : Line 33"); // Debug
                }
                else
                {
                    string _fileName = Path.GetFileName(_files[0]);
                    Debug.WriteLine(_fileName); // Debug
                }
            }
        }
    }
}