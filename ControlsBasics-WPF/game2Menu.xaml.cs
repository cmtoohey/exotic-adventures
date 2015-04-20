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
    //hope this works
    /// <summary>
    /// Interaction logic for game2Menu.xaml
    /// </summary>
    public partial class game2Menu : Page
    {
        public game2Menu()
        {
            InitializeComponent();
            var regionSensorBinding = new Binding("Kinect") { Source = Intro.sensorChooser };
            BindingOperations.SetBinding(this.game2MenuRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);
        }

        private void smallClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new game2(int.Parse(this.small.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void mediumClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new game2(int.Parse(this.medium.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void largeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new game2(int.Parse(this.large.Content.ToString())));
            //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }
}
