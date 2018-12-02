using StandardTools.ViewHandler;
using FactorioModEnvironnment;
using FactorioModEnvironnment.MVVM.Main;
using System.Windows;
using System;
using FactorioModEnvironnment.Helpers;
using System.Collections.Generic;

namespace FactorioModEnvironnment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
        void App_Startup(object sender, StartupEventArgs e)
        {
            WindowHandler.OpenWindow<MainWindow>(new MainViewModel(), true);
        }
        
	}
}
