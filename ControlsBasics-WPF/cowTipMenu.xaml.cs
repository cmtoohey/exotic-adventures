using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    /// <summary>
    /// Interaction logic for cowTipMenu.xaml
    /// </summary>
    public partial class cowTipMenu : Page
    {
        public cowTipMenu()
        {
            InitializeComponent();
           var regionSensorBinding = new Binding("Kinect") { Source = Intro.sensorChooser };
            BindingOperations.SetBinding(this.cowMenuRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);
        }

        private void smallClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.small.Content.ToString())));
        }

        private void mediumClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.medium.Content.ToString())));
        }

        private void largeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.large.Content.ToString())));
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());  
        }
    }

}
