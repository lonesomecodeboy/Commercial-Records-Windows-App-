﻿using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.ViewModels.DataVMs;
using CommercialRecords.Views;
using System;
using System.IO;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
namespace CommercialRecords.ViewModels.FrameVMs
{
    class InfoFrameVMBase<E, F> : FrameVMBase
        where E : InfoDataVMBase<F>, new()
        where F : InfoModelBase, new()
    {
        #region Properties

        private Boolean showImageLogo = true;
        public Boolean ShowImageLogo
        {
            get
            {
                return showImageLogo;
            }
            set
            {
                showImageLogo = value;
                RaisePropertyChanged("ShowImageLogo");
            }
        }

        private Boolean recorded = false;
        public Boolean Recorded
        {
            get
            {
                return recorded;
            }
            set
            {
                recorded = value;
                RaisePropertyChanged("Recorded");
            }
        }

        private string infoName;
        private double aspectRatio;

        private E currentInfo = new E();
        public E CurrentInfo
        {
            get
            {
                return currentInfo;
            }
            set
            {
                currentInfo = value;
                RaisePropertyChanged("CurrentInfo");
            }
        }

        private bool delButtonCanEnable = false;
        public bool DelButtonCanEnable
        {
            get
            {
                return delButtonCanEnable;
            }
            set
            {
                delButtonCanEnable = value;
                RaisePropertyChanged("DelButtonCanEnable");
            }
        }

        private Visibility loadingVisibility = Visibility.Collapsed;
        public Visibility LoadingVisibility
        {
            get
            {
                return loadingVisibility;
            }
            set
            {
                loadingVisibility = value;
                RaisePropertyChanged("LoadingVisibility");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand saveInfoCmd = null;
        public ICommand SaveInfoCmd
        {
            get
            {
                return saveInfoCmd;
            }
        }

        private readonly ICommand delInfoCmd = null;
        public ICommand DelInfoCmd
        {
            get
            {
                return delInfoCmd;
            }
        }

        private readonly ICommand capturePhotoFromCamCmd = null;
        public ICommand CapturePhotoFromCamCmd
        {
            get
            {
                return capturePhotoFromCamCmd;
            }
        }

        private readonly ICommand loadPhotoViaFileBrowserCmd = null;
        public ICommand LoadPhotoViaFileBrowserCmd
        {
            get
            {
                return loadPhotoViaFileBrowserCmd;
            }
        }

        private readonly ICommand createNewOneCmd = null;
        public ICommand CreateNewOneCmd
        {
            get
            {
                return createNewOneCmd;
            }
        }
        #endregion

        #region Command Handlers
        protected virtual void saveInfoCmdHandler(object parameter)
        {
            LoadingVisibility = Visibility.Visible;

            int result = CurrentInfo.save();
            string message = null;
            if (result > 0)
            {
                DelButtonCanEnable = true;
                message = CrsDictionary.getInstance().lookup("notifications", "saveRecordMessage", infoName);
            }
            else
                message = CrsDictionary.getInstance().lookup("notifications", "saveRecordFailMessage", infoName);

            var messageDialog = new MessageDialog(message, CrsDictionary.getInstance().lookup("notifications", "saveInformationHeader"));

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "okCommand"), new UICommandInvokedHandler(this.SaveInfoCommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
            //LoadingVisibility = Visibility.Collapsed;
        }

        protected virtual void delInfoCmdHandler(object parameter)
        {
            var messageDialog = new MessageDialog(
                CrsDictionary.getInstance().lookup("notifications", "recordDeleteMessage", infoName), 
                CrsDictionary.getInstance().lookup("notifications", "delRecordHeader"));

            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "noCommand"), null));
            messageDialog.Commands.Add(new UICommand(CrsDictionary.getInstance().lookup("notifications", "yesCommand"), new UICommandInvokedHandler(this.DelInfoCommandInvokedHandler)));

            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 0;
            messageDialog.ShowAsync();
        }

        protected virtual void createNewOneCmdHandler(object parameter)
        {
            CurrentInfo = new E();
            Recorded = false;
        }

        private void DelInfoCommandInvokedHandler(IUICommand command)
        {
            CurrentInfo.delete();
            Navigation.GoBack();
        }

        private void SaveInfoCommandInvokedHandler(IUICommand command)
        {
            Recorded = true;
        }

        private async void loadPhotoViaBrowserCmdHandler(object parameter)
        {
            object[] imagePickData = new object[2];
            imagePickData[0] = CurrentInfo.ImageSourceFolder;
            imagePickData[1] = aspectRatio;
            Navigation.Navigate<ImagePicker>(imagePickData);
        }

        private async void capturePhotoFromCamCmdHandler(object parameter)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.PhotoSettings.CroppedAspectRatio = new Size(240, 240 * aspectRatio);

            StorageFile capturedPhoto = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (capturedPhoto != null)
            {
                copyImgToLocalFolderNShow(capturedPhoto);
            }
        }

        private async void copyImgToLocalFolderNShow(StorageFile file)
        {
            string fileName = "photo_" + DateTime.Now.Ticks + ".jpg";
            await file.CopyAsync(CurrentInfo.ImageSourceFolder, fileName);

            CurrentInfo.ImageFileName = fileName;

            // show photo
            ShowImageLogo = false;
            CurrentInfo.ImageFileSource = new Uri(Path.Combine(CurrentInfo.ImageSourceFolder.Path, fileName));
        }
        #endregion

        public InfoFrameVMBase(FrameNavigation navigation, string infoName, double aspectRatio)
            : base(navigation)
        {
            this.infoName = infoName;
            PageTitle = infoName;
            if (CurrentInfo.Id != 0)
                Recorded = true;

            this.aspectRatio = aspectRatio;

            saveInfoCmd = new ICommandImp(saveInfoCmdHandler);
            delInfoCmd = new ICommandImp(delInfoCmdHandler);
            createNewOneCmd = new ICommandImp(createNewOneCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);

            if (null != navigation.Message)
            {
                if (null != navigation.Forward && navigation.Forward.Is<ImagePicker>())
                {
                    CurrentInfo.ImageFileName = (string)navigation.Message;
                    ShowImageLogo = false;
                    CurrentInfo.ImageFileSource = new Uri(Path.Combine(CurrentInfo.ImageSourceFolder.Path, (string)navigation.Message));
                }
                else if (navigation.Message is int)
                {
                    DelButtonCanEnable = true;

                    Recorded = true;
                    CurrentInfo.get((int)navigation.Message);

                    if (!string.IsNullOrWhiteSpace(CurrentInfo.ImageFileName))
                        ShowImageLogo = false;
                }
            }
        }
    }
}
