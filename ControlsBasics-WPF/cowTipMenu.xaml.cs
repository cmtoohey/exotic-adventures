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
           var regionSensorBinding = new Binding("Kinect") { Source = MainMenu.sensorChooser };
            BindingOperations.SetBinding(this.cowMenuRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);
        }

        private void instructionsClick(object sender, RoutedEventArgs e)
        {

        }

        private void twentyClick(object sender, RoutedEventArgs e)
        {
            Page1.number_of_cows = 2;
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void fortyClick(object sender, RoutedEventArgs e)
        {
            Page1.number_of_cows = 4;
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void sixtyClick(object sender, RoutedEventArgs e)
        {
            Page1.number_of_cows = 6;
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }

}
