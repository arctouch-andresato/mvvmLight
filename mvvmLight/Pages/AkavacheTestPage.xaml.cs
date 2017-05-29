using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mvvmLight {
    public partial class AkavacheTestPage: ContentPage {
        public AkavacheTestPage() {
            InitializeComponent();
            BindingContext = App.Locator.AkavacheViewModel;
        }
    }
}
