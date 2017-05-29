using GalaSoft.MvvmLight.Ioc;
using mvvmLight.ViewModel;
using Xamarin.Forms;

namespace mvvmLight {
    public partial class App: Application {
        //ViewModelLocator object to handle ViewModels and bindings between them and Views (Pages):
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        public static IFileSystemService FileSystemService { get; private set; }

        public App()
        {
            InitializeComponent();

            INavigationService navigationService;

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                // Setup navigation service:
                navigationService = new NavigationService();

                // Configure pages:
                navigationService.Configure(AppPages.MainPage, typeof(MainPage));
                navigationService.Configure(AppPages.DetailsPage, typeof(DetailsPage));
                navigationService.Configure(AppPages.AkavachePage, typeof(AkavacheTestPage));

                // Register NavigationService in IoC container:
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }

            else
                navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

            // Create new Navigation Page and set MainPage as its default page:
            var firstPage = new NavigationPage(new MainPage());
            // Set Navigation page as default page for Navigation Service:
            navigationService.Initialize(firstPage);
            // You have to also set MainPage property for the app:
            MainPage = firstPage;
        }

        public static void Init(IFileSystemService fileSystemServiceImpl) 
        {
            FileSystemService = fileSystemServiceImpl;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
