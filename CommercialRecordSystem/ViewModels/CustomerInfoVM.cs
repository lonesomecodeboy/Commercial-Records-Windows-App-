﻿using System;
using System.Linq;
using System.Windows.Input;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using System.IO;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerInfoVM : VMBase
    {
        #region Properties
        private CustomerVM currentCustomer = new CustomerVM();
        public CustomerVM CurrentCustomer
        {
            get
            {
                return currentCustomer;
            }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("CurrentCustomer");
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
        private readonly ICommand saveCustomerCmd = null;
        public ICommand SaveCustomerCmd
        {
            get
            {
                return saveCustomerCmd;
            }
        }

        private readonly ICommand delCustomerCmd = null;
        public ICommand DelCustomerCmd
        {
            get
            {
                return delCustomerCmd;
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
        #endregion

        #region Command Handlers
        private void saveCustomerCmdHandler(object parameter)
        {
            LoadingVisibility = Visibility.Visible;
            Customer newCustomer = new Customer();
            //for updating customer with
            if (CurrentCustomer.Id > 0)
            {
                newCustomer.Id = CurrentCustomer.Id;
            }
            newCustomer.Name = CurrentCustomer.Name.ToUpper();
            newCustomer.Surname = CurrentCustomer.Surname.ToUpper();
            newCustomer.Sincerity = CurrentCustomer.Sincerity;
            newCustomer.Address = CurrentCustomer.Address.ToUpper();
            newCustomer.PhoneNumber = CurrentCustomer.PhoneNumber;
            newCustomer.MobileNumber = CurrentCustomer.MobileNumber;
            newCustomer.ProfilePhotoFileName = CurrentCustomer.ProfilePhotoFileName;
            newCustomer.LastTransactDate = CurrentCustomer.LastTransactDate;
            newCustomer.AccountCost = CurrentCustomer.AccountCost;
            newCustomer.CreatedDate = CurrentCustomer.CreatedDate;
            newCustomer.ModifiedDate = CurrentCustomer.ModifiedDate;

            CustomerVM.saveCustomer(newCustomer);

            //LoadingVisibility = Visibility.Collapsed;
        }

        private void delCustomerCmdHandler(object parameter)
        {
            var messageDialog = new MessageDialog("Seçili müşteri kalıcı olarak silmek istediğinizden emin misiniz?", "Müşteri Silme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Hayır", null));
            messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            CustomerVM.deleteCustomer(CurrentCustomer.Id);
        }

        private async void loadPhotoViaBrowserCmdHandler(object parameter)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.SettingsIdentifier = "PicturePicker";
            filePicker.CommitButtonText = "Seç";

            StorageFile selectedFile = await filePicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                copyImgToLocalFolderNShow(selectedFile);
            }
        }

        private async void capturePhotoFromCamCmdHandler(object parameter)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            Size aspectRatio = new Size(240, 300);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile capturedPhoto = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (capturedPhoto != null)
            {
                copyImgToLocalFolderNShow(capturedPhoto);
            }
        }

        private async void copyImgToLocalFolderNShow(StorageFile file)
        {
            string fileName = "photo_" + DateTime.Now.Ticks + ".jpg";

            CurrentCustomer.ProfilePhotoFileName = fileName;

            await file.CopyAsync(App.ProfileImgFolder, fileName);
            // show photo
            CurrentCustomer.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, fileName));
        }
        #endregion

        public CustomerInfoVM()
        {
            saveCustomerCmd = new ICommandImp(saveCustomerCmdHandler);
            delCustomerCmd = new ICommandImp(delCustomerCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);
        }

        public CustomerInfoVM(int customerId)
        {
            CurrentCustomer = CustomerVM.getCustomer(customerId);
            saveCustomerCmd = new ICommandImp(saveCustomerCmdHandler);
            delCustomerCmd = new ICommandImp(delCustomerCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);
        }
    }
}
