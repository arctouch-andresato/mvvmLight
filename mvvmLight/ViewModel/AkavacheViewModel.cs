using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using GalaSoft.MvvmLight;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace mvvmLight
{
    public class AkavacheViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        const string imageKey = "imageCache";

        // Image source property to display selected image from gallery:
        ImageSource _myImageSource;
        public ImageSource MyImageSource
        {
            get
            {
                if (_myImageSource == null)
                {
                    return Device.OnPlatform(
                        iOS: ImageSource.FromFile("Images/picture.png"),
                        Android: ImageSource.FromFile("picture.png"),
                        WinPhone: ImageSource.FromFile("Images/wpicture.png"));
                }
                return _myImageSource;
            }

            set
            {
                _myImageSource = value;
                RaisePropertyChanged();
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        public int Loops
        {
            get
            {
                return _loops;
            }

            set
            {
                _loops = value;
            }
        }

        string _status;

        int _loops;

        int lastPosition = 0;

        public ICommand AddNewImage { get; private set; }
        public ICommand FillOutCache { get; private set; }

        public AkavacheViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddNewImage = new Command(async () => await OnAddNewImage());
            FillOutCache = new Command(async () => await OnFillOutCache());
        }

        private async Task OnAddNewImage()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var image = await CrossMedia.Current.PickPhotoAsync();
                if (image != null)
                {
                    var stream = image.GetStream();
                    MyImageSource = ImageSource.FromStream(() =>
                    {
                        return stream;
                    });
                    Status = "Image imported";
                    BlobCache.UserAccount.InsertObject(imageKey, image);
                }
            }
        }

        private async Task OnFillOutCache()
        {
            Status = "Loading Images...";
            BlobCache.UserAccount.GetObject<MediaFile>(imageKey).Subscribe((MediaFile obj) =>
            {
                for (int i = lastPosition; i < lastPosition + Loops; i++)
                {
                    BlobCache.UserAccount.InsertObject(imageKey + i, obj);
                }
                lastPosition += Loops;
                BlobCache.UserAccount.GetObject<MediaFile>(imageKey + (lastPosition - 1))
                     .Subscribe((MediaFile mf) =>
	            {
	                var stream = mf.GetStream();
	                MyImageSource = ImageSource.FromStream(() =>
	                {
	                    return stream;
	                });
	            });
                Status = "Total Images loaded:" + lastPosition + " total size:" + App.FileSystemService.GetDBFileSize();
            });

        }
    }
}
