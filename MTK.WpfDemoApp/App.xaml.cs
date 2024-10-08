﻿using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using MTK.DemoApp.ViewModels;
using MTK.WpfDemoApp.Views;
using Prism.Ioc;
using Prism.Unity;
using Index = MTK.WpfDemoApp.Views.Index;

namespace MTK.WpfDemoApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<Index, IndexViewModel>();
        containerRegistry.RegisterForNavigation<Counter, CounterViewModel>();
        
        containerRegistry.RegisterSingleton<IMessenger, WeakReferenceMessenger>();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}