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

    //private static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
    //    {
    //        if (args.OldSensor != null)
    //        {
    //            try
    //            {
    //                args.OldSensor.DepthStream.Range = DepthRange.Default;
    //                args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
    //                args.OldSensor.DepthStream.Disable();
    //                args.OldSensor.SkeletonStream.Disable();
    //            }
    //            catch (InvalidOperationException)
    //            {
    //                // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
    //                // E.g.: sensor might be abruptly unplugged.
    //            }
    //        }

    //        if (args.NewSensor != null)
    //        {
    //            try
    //            {
    //                args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
    //                args.NewSensor.SkeletonStream.Enable();

    //                try
    //                {
    //                    args.NewSensor.DepthStream.Range = DepthRange.Near;
    //                    args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
    //                }
    //                catch (InvalidOperationException)
    //                {
    //                    // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
    //                    args.NewSensor.DepthStream.Range = DepthRange.Default;
    //                    args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
    //                }
    //            }
    //            catch (InvalidOperationException)
    //            {
    //                // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
    //                // E.g.: sensor might be abruptly unplugged.
    //            }
    //        }
    //    }


}
