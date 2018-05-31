using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using PIWorks_Assignment.ViewModels;

namespace PIWorks_Assignment.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName;
        public Dictionary<int, int> itemSourceForClienSong;
        MainViewModel mv = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*"; 
            if (openFileDialog.ShowDialog() == true)
            {
                txtEditor.Text = openFileDialog.FileName;
            }
            if (txtEditor.Text != null)
            {
                fileName = txtEditor.Text;
                btnUploadFile.IsEnabled = true;
                btnFindMaxiumumSum.IsEnabled = false;
            }
        }

        private void btnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            if (fileName == null)
                return;

            mv.UploadFileToList(fileName); //Read and fill the object list

            btnFindMaxiumumSum.IsEnabled = true;
        }

        private void btnFindMaxiumumSum_Click(object sender, RoutedEventArgs e)
        {
            lblMaximumSum.Content = "Max Sum : " + mv.CalculateMaxSum(0, 1);//Send root index and current depth
        }

        //private void btnPrimeCheck_Click(object sender, RoutedEventArgs e)
        //{
        //    int.TryParse(txtPrimeCheck.Text, out int isPrime);
        //    lblPrimeCheck.Content = isPrime +" " + mv.IsPrimeNumber(isPrime);
        //}
    }
}
