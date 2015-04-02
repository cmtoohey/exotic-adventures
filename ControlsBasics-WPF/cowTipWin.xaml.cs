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
    /// Interaction logic for cowTipWin.xaml
    /// </summary>
    public partial class cowTipWin : Page
    {
        public static Label gameStopwatch = new Label();
        public cowTipWin()
        {
            InitializeComponent();
            var regionSensorBinding = new Binding("Kinect") { Source = MainMenu.sensorChooser };
            BindingOperations.SetBinding(this.cowWinRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

            
            gameStopwatch.Margin = new Thickness(259,314,0,0);
            gameStopwatch.Height= 250;
            gameStopwatch.Width= 700;
            gameStopwatch.FontSize = 70;
            this.cowWinGrid.Children.Add(gameStopwatch);




        }

        private void cowMenuClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("cowTipMenu.xaml", UriKind.Relative);
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }













}
