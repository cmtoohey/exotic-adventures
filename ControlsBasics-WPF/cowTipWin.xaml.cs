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
        public static Label cowTiming = new Label();

        //constructor for cowTipWin
        public cowTipWin()
        {
            InitializeComponent();
            var regionSensorBinding = new Binding("Kinect") { Source = MainMenu.sensorChooser };
            BindingOperations.SetBinding(this.cowWinRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

            //cowTiming.Height = 90;
            //cowTiming.Margin = new Thickness(322, 189, 0, 0);
            //string cowTipLine = ;
            //cowTiming.Content = cowTipLine;
            //cowTiming.Width = 595;
            //cowTiming.FontSize = 60;

            //this.cowWinGrid.Children.Add(cowTiming);

            gameStopwatch.Margin = new Thickness(30,10,0,0);
            gameStopwatch.Height= 250;
            gameStopwatch.Width = 700;
            string cowTipTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    Page1.ts.Hours, Page1.ts.Minutes, Page1.ts.Seconds,
                    Page1.ts.Milliseconds / 10);
            gameStopwatch.Content = "You tipped " + Page1.number_of_cows + " cows in\n" + cowTipTime;
            gameStopwatch.FontSize = 55;
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
