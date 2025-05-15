using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace BaseLayerLoader
{
    /// <summary>
    /// Interaction logic for BaseLayerDockpaneView.xaml
    /// </summary>
    public partial class BaseLayerDockpaneView : UserControl
    {
        public BaseLayerDockpaneView()
        {
            InitializeComponent();

            // Load saved paths into TextBoxes when UI initializes
            Path1Box.Text = Properties.Settings.Default.BaseLayer1;
            Path2Box.Text = Properties.Settings.Default.BaseLayer2;
            Path3Box.Text = Properties.Settings.Default.BaseLayer3;
        }

        private void Browse1_Click(object sender, RoutedEventArgs e)
        {
            string path = OpenLayerDialog();
            if (!string.IsNullOrEmpty(path))
                Path1Box.Text = path;
        }

        private void Browse2_Click(object sender, RoutedEventArgs e)
        {
            string path = OpenLayerDialog();
            if (!string.IsNullOrEmpty(path))
                Path2Box.Text = path;
        }

        private void Browse3_Click(object sender, RoutedEventArgs e)
        {
            string path = OpenLayerDialog();
            if (!string.IsNullOrEmpty(path))
                Path3Box.Text = path;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BaseLayer1 = Path1Box.Text;
            Properties.Settings.Default.BaseLayer2 = Path2Box.Text;
            Properties.Settings.Default.BaseLayer3 = Path3Box.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("Base layer paths saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string OpenLayerDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Base Layer File",
                Filter = "All files (*.*)|*.*",
                CheckFileExists = true
            };

            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}
