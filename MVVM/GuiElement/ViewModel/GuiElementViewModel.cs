using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using StandardTools.ViewHandler;

namespace FactorioModEnvironnment.MVVM.GuiElement
{
    public class GuiElementViewModel : ViewModelBase
    {
        private List<Tuple<string, string>> _listGuiElements;
        public GuiElementViewModel()
        {
            _listGuiElements = new List<Tuple<string, string>>();
            DirectoryInfo resourcesGuiPicturesDirectory = new DirectoryInfo(@"..\\..\\Resources\\GuiElement");
            foreach (var file in resourcesGuiPicturesDirectory.GetFiles())
                _listGuiElements.Add(new Tuple<string,string>(file.Name.Replace(file.Extension,""), file.FullName));            
        }
    
        public List<Tuple<string, string>> ListImages
        {
            get { return _listGuiElements; }
        }

        public bool ImageSelected
        {
            get { return false; }
        }
    }
}
