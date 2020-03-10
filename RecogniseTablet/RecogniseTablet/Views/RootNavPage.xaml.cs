using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecogniseTablet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootNavPage : NavigationPage
    {
        public RootNavPage()
        {
            InitializeComponent();
        }
    }
}