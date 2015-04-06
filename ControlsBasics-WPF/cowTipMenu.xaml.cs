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

        private void smallClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.small.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void mediumClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.medium.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void largeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TipTheCow(int.Parse(this.large.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }

}
