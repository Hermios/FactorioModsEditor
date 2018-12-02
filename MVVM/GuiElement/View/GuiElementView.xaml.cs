using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FactorioModEnvironnment.MVVM.GuiElement
{
    /// <summary>
    /// Logique d'interaction pour GuiElementView.xaml
    /// </summary>
    public partial class GuiElementView : UserControl
    {
        public GuiElementView()
        {
            InitializeComponent();
        }

        private void sample_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;
            DataObject data = new DataObject(typeof(ImageSource), image.Source);
            DragDrop.DoDragDrop(image, data, DragDropEffects.All);
        }

        private void canvas_Drop(object sender, DragEventArgs e)
        {
            //e.GetPosition();
            Image imageControl = (Image)sender;
            if ((e.Data.GetData(typeof(ImageSource)) != null))
            {
                ImageSource image = e.Data.GetData(typeof(ImageSource)) as ImageSource;
                imageControl = new Image() { Width = 100, Height = 100, Source = image };
                //img1 = imageControl;
            }
        }
    }
}
