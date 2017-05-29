using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mvvmLight {
    public partial class DetailsPage: ContentPage {
        public DetailsPage(Person person) {
            InitializeComponent();
            BindingContext = App.Locator.DetailsViewModel;
            App.Locator.DetailsViewModel.Person = person;
        }
    }
}
