/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:mvvmLight"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace mvvmLight.ViewModel {
    public class ViewModelLocator {
        ///<summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            /// 
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ICognitiveClient, CognitiveClient>();
            SimpleIoc.Default.Register<DetailsViewModel>(true);
            SimpleIoc.Default.Register<AkavacheViewModel>();

        }


        public MainViewModel MainViewModel {
            get {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DetailsViewModel DetailsViewModel {
            get {
                return ServiceLocator.Current.GetInstance<DetailsViewModel>();
            }
        }

        public AkavacheViewModel AkavacheViewModel {
            get {
                return ServiceLocator.Current.GetInstance<AkavacheViewModel>();
            }
        }

        public static void Cleanup() {
            // TODO Clear the ViewModels
        }
    }
}