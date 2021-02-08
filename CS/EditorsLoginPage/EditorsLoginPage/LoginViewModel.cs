using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace EditorsLoginPage {
    public class LoginViewModel : INotifyPropertyChanged {
        CountryPhoneInfo selectedCountryCode;

        bool phoneNumberIsFocused;
        bool passwordIsFocused;

        string phoneNumber;
        string password;

        string phoneNumberErrorMessage;
        string passwordErrorMessage;


        public IList<CountryPhoneInfo> CountryCodes { get; } = CountryPhoneInfo.DefaultCodes;
        public CountryPhoneInfo SelectedCountryCode {
            get => selectedCountryCode;
            set => SetProperty(ref selectedCountryCode, value); }

        public string PhoneNumber {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }
        public string Password {
            get => password;
            set => SetProperty(ref password, value);
        }

        public bool PhoneNumberIsFocused {
            get => phoneNumberIsFocused;
            set => SetProperty(ref phoneNumberIsFocused, value, OnPhoneNumberFocusChanged);
        }
        public bool PasswordIsFocused {
            get => passwordIsFocused;
            set => SetProperty(ref phoneNumberIsFocused, value, OnPasswordFocusChanged);
        }

        public string PhoneNumberErrorMessage {
            get => phoneNumberErrorMessage;
            set => SetProperty(ref phoneNumberErrorMessage, value, OnErrorMessageChanged);
        }
        public string PasswordErrorMessage {
            get => passwordErrorMessage;
            set => SetProperty(ref passwordErrorMessage, value, OnErrorMessageChanged);
        }

        public Command LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel() {
            selectedCountryCode = CountryCodes.First(i => i.Country.Equals("Canada", StringComparison.InvariantCultureIgnoreCase));
            LoginCommand = new Command(Login, CanLogin);
        }

        protected void SetProperty<T>(ref T storingField, T value, Action<T> onValueChanged = null, [CallerMemberName] string propertyName = "") {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            if (comparer.Equals(storingField, value)) {
                return;
            }
            storingField = value;
            onValueChanged?.Invoke(value);
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnPhoneNumberFocusChanged(bool hasFocus) {
            if (!hasFocus) {
                if (string.IsNullOrEmpty(PhoneNumber)) {
                    PhoneNumberErrorMessage = "Should not be empty.";
                } else if (PhoneNumber.Length != SelectedCountryCode.DigitCount) {
                    PhoneNumberErrorMessage = $"Should contain {SelectedCountryCode.DigitCount} digits.";
                } else {
                    PhoneNumberErrorMessage = string.Empty;
                }
            }
        }

        void OnPasswordFocusChanged(bool hasFocus) {
            if (!hasFocus) {
                if (string.IsNullOrEmpty(Password)) {
                    PasswordErrorMessage = "Should not be empty.";
                } else if (Password.Length < 8) {
                    PasswordErrorMessage = $"Should contain at least 8 symbols.";
                } else {
                    PasswordErrorMessage = string.Empty;
                }
            }
        }

        void OnErrorMessageChanged(string newMessage) {
            if (newMessage == null) {
                return;
            }
            LoginCommand.ChangeCanExecute();
        }

        void Login() {
            // TODO: Do Some actions.
        }

        bool CanLogin() {
            return string.IsNullOrEmpty(PhoneNumberErrorMessage) && string.IsNullOrEmpty(PasswordErrorMessage);
        }
    }

    public class CountryPhoneInfo {
        public static IList<CountryPhoneInfo> DefaultCodes { get; } = new List<CountryPhoneInfo> {
            new CountryPhoneInfo { Country = "Belgium", Code = "+32", PhoneMask = "0000 000-000" },
            new CountryPhoneInfo { Country = "Canada", Code = "+1" },
            new CountryPhoneInfo { Country = "Denmark", Code = "+45", PhoneMask="00-00-00-00", DigitCount = 8 },
            new CountryPhoneInfo { Country = "France", Code = "+33", PhoneMask = "0 00 00 00 00", DigitCount = 9 },
            new CountryPhoneInfo { Country = "Germany", Code = "+49", PhoneMask = "0000 000000" },
            new CountryPhoneInfo { Country = "Greece", Code = "+30", PhoneMask = "000 0000000" },
            new CountryPhoneInfo { Country = "Italy", Code = "+39", PhoneMask = "000 0000000" },
            new CountryPhoneInfo { Country = "Netherlands", Code = "+31", PhoneMask = "00 00000000" },
            new CountryPhoneInfo { Country = "Poland", Code = "+48", PhoneMask = "000-000-000", DigitCount = 9},
            new CountryPhoneInfo { Country = "Romania", Code = "+40", PhoneMask = "000-000-00-00" },
            new CountryPhoneInfo { Country = "Russia", Code = "+7", PhoneMask = "000 000-00-00" },
            new CountryPhoneInfo { Country = "Spain", Code = "+34", PhoneMask = "000 000 000", DigitCount = 9 },
            new CountryPhoneInfo { Country = "Sweden", Code = "+46", PhoneMask = "000-000 00 00" },
            new CountryPhoneInfo { Country = "Switzerland", Code = "+41", PhoneMask = "00 000 00 00", DigitCount = 9 },
            new CountryPhoneInfo { Country = "Ukraine", Code = "+380", PhoneMask = "00 000 00 00", DigitCount = 9 },
            new CountryPhoneInfo { Country = "United Kingdom", Code = "+44", PhoneMask = "0000 000 000" },
            new CountryPhoneInfo { Country = "United States", Code = "+1" }
        };

        public string Country { get; set; }
        public string Code { get; set; }
        public string PhoneMask { get; set; } = "000-000-0000";
        public int DigitCount { get; set; } = 10;
    }
}
