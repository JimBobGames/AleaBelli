using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AleaBelliScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location.;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);

            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".scn"; // Default file extension
            dialog.Filter = "Scenarios (.scn)|*.scn"; // Filter files by extension
            dialog.InitialDirectory = path;
 
            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }
        }
        private void NewCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
