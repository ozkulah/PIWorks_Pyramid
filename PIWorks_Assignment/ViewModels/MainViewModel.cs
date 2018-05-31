using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using PIWorks_Assignment.Utils;
using PIWorks_Assignment.Models;

namespace PIWorks_Assignment.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region Parameters
        private readonly IDialogService DialogService;

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title
        {
            get { return "PIWorks Assignment"; }
        }

        public int depthOfPyramid;
        public List<Pyramid> lstPyramid = new List<Pyramid>();
        #endregion

        #region Constructors
        public MainViewModel()
        {
            // DialogService is used to handle dialogs
            this.DialogService = new MvvmDialogs.DialogService();
        }




        #endregion

        #region Methods
        private void InitializeVariables()
        {
            depthOfPyramid = 0;
            lstPyramid.Clear();
        }

        public void UploadFileToList(string fileName)
        {
            InitializeVariables();
            int localDepth = 0;
            string line;
            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                string[] splitedLine = line.Split('\t',' ');
                for (int i = 0; i < splitedLine.Count(); i++)
                {
                    int.TryParse(splitedLine[i], out int value);
                    //Create a Pyramid object and check value is a prime or not
                    lstPyramid.Add(new Pyramid(localDepth, value, IsPrimeNumber(value)));
                }
                localDepth++;
            }

            file.Close();

            depthOfPyramid = localDepth; //Find the depth of pyramid
        }

        public bool IsPrimeNumber(int number)
        {
            if (number == 2) //Check the only even prime number
            {
                return true;
            }
            else if(number % 2 == 0) //Check the number, is it even or not
            {
                return false;
            }
            for (int i = 3; i < number/2; i=i+2) //Only check the odd numbers
            {
                if(number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int CalculateMaxSum(int indexOfRoot, int depth)
        {
            if(depthOfPyramid == 1)//If it is only one line than return it
            {
                return lstPyramid[0].value;
            }

            int result = 0;
            int rootNumber = lstPyramid[indexOfRoot].value;
            int leftNumber = 0;
            int rightNumber = 0;
            bool isLeftPrime = lstPyramid[indexOfRoot + depth].isPrimeNumber;//Is LeftLeaf value is a Prime
            bool isRightPrime = lstPyramid[indexOfRoot + depth + 1].isPrimeNumber;//Is RightLeaf value is a Prime
            if (depth + 1 == depthOfPyramid)//If it reach the end leaves than finish the recursive method
            {
                if(!isLeftPrime && !isRightPrime)//Both of them are not prime 
                {
                    //So choose the big one
                    result = rootNumber + Math.Max(lstPyramid[indexOfRoot + depth].value, lstPyramid[indexOfRoot + depth + 1].value);
                }
                else if (!isLeftPrime)//Otherwise leftleaf is not prime
                {
                    result = rootNumber + lstPyramid[indexOfRoot + depth].value; //Add it to result
                }
                else if (!isRightPrime)//Otherwise rightleaf is not prime
                {
                    result = rootNumber + lstPyramid[indexOfRoot + depth + 1].value; //Add it to result
                }
            }
            else
            {
                if (!isLeftPrime && !isRightPrime)//Both of them are not prime 
                {
                    leftNumber = rootNumber + CalculateMaxSum(indexOfRoot + depth, depth + 1); //Set leftLeaf as a root and call same method again
                    rightNumber = rootNumber + CalculateMaxSum(indexOfRoot + depth + 1, depth + 1);//Set rightLeaf as a root and call same method again
                    result = Math.Max(leftNumber, rightNumber);//Choose the big one
                }
                else if (!isLeftPrime)//Otherwise leftleaf is not prime
                {
                    result = rootNumber + CalculateMaxSum(indexOfRoot + depth, depth + 1);//Set leftLeaf as a root and call same method again
                }
                else if (!isRightPrime)//Otherwise rightleaf is not prime
                {
                    result = rootNumber + CalculateMaxSum(indexOfRoot + depth + 1, depth + 1);//Set rightLeaf as a root and call same method again
                }
            }

            return result;
        }

        #endregion

        #region Commands
        public RelayCommand<object> SampleCmdWithArgument { get { return new RelayCommand<object>(OnSampleCmdWithArgument); } }

        public ICommand SaveAsCmd { get { return new RelayCommand(OnSaveAsTest, AlwaysFalse); } }
        public ICommand SaveCmd { get { return new RelayCommand(OnSaveTest, AlwaysFalse); } }
        public ICommand NewCmd { get { return new RelayCommand(OnNewTest, AlwaysFalse); } }
        public ICommand OpenCmd { get { return new RelayCommand(OnOpenTest, AlwaysFalse); } }

        public ICommand ExitCmd { get { return new RelayCommand(OnExitApp, AlwaysTrue); } }

        private bool AlwaysTrue() { return true; }
        private bool AlwaysFalse() { return false; }

        private void OnSampleCmdWithArgument(object obj)
        {
            // TODO
        }

        private void OnSaveAsTest()
        {
            var settings = new SaveFileDialogSettings
            {
                Title = "Save As",
                Filter = "Sample (.xml)|*.xml",
                CheckFileExists = false,
                OverwritePrompt = true
            };

            bool? success = DialogService.ShowSaveFileDialog(this, settings);
            if (success == true)
            {
                // Do something
            }
        }
        private void OnSaveTest()
        {
            // TODO
        }
        private void OnNewTest()
        {
            // TODO
        }
        private void OnOpenTest()
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Open",
                Filter = "Sample (.xml)|*.xml",
                CheckFileExists = false
            };

            bool? success = DialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                // Do something
            }
        }

        private void OnExitApp()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }
        #endregion

        #region Events

        #endregion
    }
}
