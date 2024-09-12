using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTK.MauiDemoApp.Views;

public partial class Index : ContentPage
{
    public Index()
    {
        BindingContext = new MTK.DemoApp.ViewModels.IndexViewModel();
        InitializeComponent();
    }
}