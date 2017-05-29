using System;
using Xamarin.Forms;

namespace mvvmLight {
    public interface INavigationService {
        void GoBack();

        void Configure(AppPages mainPage, Type type);

        void NavigateTo(AppPages pageKey);

        void Initialize(NavigationPage firstPage);

        void NavigateTo(AppPages pageKey, object parameter);
   }
}
