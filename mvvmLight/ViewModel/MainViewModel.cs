using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;

namespace mvvmLight.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; private set; }
        public ICommand NavigateAkavacheCommand { get; private set; }


        public MainViewModel(INavigationService navigationService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            /// 
            /// 
            _navigationService = navigationService;
            NavigateCommand = new Command(() => Navigate());
            NavigateAkavacheCommand  = new Command(() =>_navigationService.NavigateTo(AppPages.AkavachePage));
        }

        private void Navigate()
        {
            var person = new Person() { Name = "Daniel" };
            _navigationService.NavigateTo(AppPages.DetailsPage, person);
        }
    }
}