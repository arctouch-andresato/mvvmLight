using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mvvmLight {
    public partial class MainPage: ContentPage {
        public MainPage() {
            InitializeComponent();
            BindingContext = App.Locator.MainViewModel;
        }
    }
}
