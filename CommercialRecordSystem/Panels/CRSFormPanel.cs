﻿using CommercialRecordSystem.Controls;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Panels
{
    class CRSFormPanel : CRSPanel
    {
        #region Properties
        private List<CRSTextBox> inputElements = null;
        #endregion
        public CRSFormPanel()
        {
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            inputElements = new List<CRSTextBox>();
            foreach (UIElement element in this.Children)
            {
                if (element is Panel)
                {
                    Panel panel = (Panel)element;
                    foreach (UIElement panelElement in panel.Children)
                    {
                        addInputElements(panelElement);
                    }
                }
                else
                {
                    addInputElements(element);
                }
            }
        }

        private void addInputElements(UIElement element)
        {
            Type elementType = element.GetType();
            if (elementType.Equals(typeof(CRSTextBox)))
            {
                inputElements.Add(element as CRSTextBox);
            }
            if (elementType.Equals(typeof(CSRButton)))
            {
                CSRButton buttonBuff = element as CSRButton;
                if (buttonBuff.Validation)
                    buttonBuff.Click += new RoutedEventHandler(assignButtonCanExecute);
            }
        }

        private void assignButtonCanExecute(object sender, RoutedEventArgs e)
        { 
            bool validated = true;
            foreach (CRSTextBox element in inputElements)
            {
                if (element.Required)
                {
                    element.Validate();
                    if (!element.IsValid)
                    {
                        validated = false;
                    }
                }
            }

            (sender as CSRButton).Command.CanExecute(validated);
        }
    }
}