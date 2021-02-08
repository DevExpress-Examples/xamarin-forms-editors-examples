using System;
using System.Globalization;
using Xamarin.Forms;

namespace EditorsLoginPage {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }
    }

    class StringToBoolConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType != typeof(bool)) {
                return null;
            }
            if (value == null) {
                return false;
            }
            if (!(value is string stringValue)) {
                return null;
            }
            return !string.IsNullOrEmpty(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
