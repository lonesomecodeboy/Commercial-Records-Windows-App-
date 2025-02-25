﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
namespace CommercialRecords.Converters
{
    class InvertVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (((Visibility)value).Equals(Visibility.Visible))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (((Visibility)value).Equals(Visibility.Visible))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }
    }
}