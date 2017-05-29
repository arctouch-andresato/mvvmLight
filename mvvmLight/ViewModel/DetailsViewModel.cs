using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Plugin.Media;
using Xamarin.Forms;

namespace mvvmLight {
    public class DetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ICognitiveClient _cognitiveClient;

        Person _person;
        public Person Person
        {
            get
            {
                return _person;
            }

            set
            {
                _person = value;
                RaisePropertyChanged();
            }
        }

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
        // Image description property to display result from Cognitive Services:
        string _imageDescription;
        public string ImageDescription
        {
            get
            {
                return _imageDescription;
            }

            set
            {
                _imageDescription = value;
                RaisePropertyChanged();
            }
        }
        // Bool property to enable and disable progress bar when loading result:
        bool _progressVisible;
        public bool ProgressVisible
        {
            get
            {
                return _progressVisible;
            }

            set
            {
                _progressVisible = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddNewImage { get; private set; }



        public DetailsViewModel(INavigationService navigationService, ICognitiveClient cognitiveClient)
        {
            _navigationService = navigationService;
            _cognitiveClient = cognitiveClient;
            AddNewImage = new Command(async () => await OnAddNewImage());

            //Messenger.Default.Register<Person>(this, person => { Person = person; });
        }
        // Add new image method where we are using CognitiveClient object and also "Person" class:
        private async Task OnAddNewImage()
        {
            ImageDescription = string.Empty;
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
                    try
                    {
                        ProgressVisible = true;
                        var result = await _cognitiveClient.GetImageDescription(image.GetStream());
                        image.Dispose();

                        Person person = new Person()
                        {
                            Name = "Bill",
                            Surname = "Gates",
                            Information = result.Description.Tags
                        };

                        foreach (string tag in person.Information)
                        {
                            ImageDescription = ImageDescription + "\n" + tag;
                        }
                    }
                    catch (Microsoft.ProjectOxford.Vision.ClientException ex)
                    {
                        ImageDescription = ex.Message;
                    }
                    ProgressVisible = false;
                }
            }
        }
    }
}
