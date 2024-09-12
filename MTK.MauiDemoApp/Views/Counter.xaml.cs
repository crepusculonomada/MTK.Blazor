using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTK.MauiDemoApp.Views;

public partial class Counter : ContentPage
{
    public Counter()
    {
        BindingContext = new MTK.DemoApp.ViewModels.CounterViewModel();
        InitializeComponent();
    }
}