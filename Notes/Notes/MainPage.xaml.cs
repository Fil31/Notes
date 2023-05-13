using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        private readonly NoteViewModel _noteViewModel;

        public MainPage()
        {
            InitializeComponent();

            _noteViewModel = new NoteViewModel { Navigation = Navigation };
            BindingContext = _noteViewModel;
        }
    }
}