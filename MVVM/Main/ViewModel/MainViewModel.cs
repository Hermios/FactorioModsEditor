using FactorioModEnvironnment.Helpers;
using FactorioModEnvironnment.Helpers.LuaData;
using FactorioModEnvironnment.MVVM.GuiElement;
using StandardTools.ServiceLocator;
using StandardTools.ViewHandler;
using System;

namespace FactorioModEnvironnment.MVVM.Main
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            IServiceLocator serviceLocator = ServiceLocator.getServiceLocator();
            serviceLocator.add<ILuaData, LuaData>();
        }
        public GuiElementViewModel guiElementView
        {
            get { return new GuiElementViewModel(); }
        }        
    }
}
