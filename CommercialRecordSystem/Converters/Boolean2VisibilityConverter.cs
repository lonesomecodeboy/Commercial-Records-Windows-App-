﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Boolean2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value || !(Boolean)value)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (Visibility.Visible.Equals((Visibility)value))
                return true;
            return false;
        }
    }
}