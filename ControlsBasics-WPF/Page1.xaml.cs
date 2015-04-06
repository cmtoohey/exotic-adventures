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
using System.Timers;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TipTheCow : Page
    {
        //public static readonly DependencyProperty PageLeftEnabledProperty = DependencyProperty.Register(
        //   "PageLeftEnabled", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        //public static readonly DependencyProperty PageRightEnabledProperty = DependencyProperty.Register(
        //    "PageRightEnabled", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        //private const double ScrollErrorMargin = 0.001;

        //private const int PixelScrollByAmount = 20;

        //private readonly KinectSensorChooser sensorChooser;

        //   private int count = 0;
        private System.Media.SoundPlayer startSound = new System.Media.SoundPlayer(@"Moo.wav");
        private int number_of_cows = new int();
        private int cowCounter = new int();
        private Stopwatch sw = new Stopwatch();
        private TimeSpan ts = new TimeSpan();
        private Label tipped_tracker = new Label();
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public TipTheCow(int number_of_cows_)
        {
            this.InitializeComponent();
            number_of_cows = number_of_cows_;
            cowCounter = 0;

            tipped_tracker.FontFamily = new FontFamily("Arial");
            tipped_tracker.FontWeight = FontWeights.DemiBold;
            tipped_tracker.FontSize = 32;
            tipped_tracker.Content = "Cows tipped: " + cowCounter;
            tipped_tracker.Margin = new Thickness(0, 0, 900, 0);
            tipped_tracker.Width = 260;
            tipped_tracker.Height = 50;
            this.TopGrid.Children.Add(tipped_tracker);
            //this.t.Children.Add(tipped_tracker);
            var r = new Random();

            //var backHome = new KinectTileButton { Name = "BackHome", Content = "Back Home", Height = 50, Width = 200 };
            //backHome.Margin = new Thickness(0,0,1200,580);
            // backHome.Click += this.BACKHOME_Click;
            //this.kinectRegionGrid.Children.Add(backHome);
         
            var button = new KinectTileButton { Name = "Cow" };
            //Changes the image background
            Uri resourceUri = new Uri("Cow.jpg", UriKind.Relative);
            System.Windows.Resources.StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            System.Windows.Media.Imaging.BitmapFrame temp = System.Windows.Media.Imaging.BitmapFrame.Create(streamInfo.Stream);
            var brush = new System.Windows.Media.ImageBrush();
            brush.ImageSource = temp;
            //brush.Opacity = OpacityMask.

            button.Background = brush;
            button.Margin = new Thickness(r.Next(0,550), r.Next(0, 220),r.Next(0,550), r.Next(0, 220));
            button.Click += this.KinectTileButtonClick;
            
            this.kinectRegionGrid.Children.Add(button);

            sw.Start();
            // Bind listner to scrollviwer scroll position change, and check scroll viewer position
            //this.UpdatePagingButtonState();
            //scrollViewer.ScrollChanged += (o, e) => this.UpdatePagingButtonState();
        }

     
        /// <summary>
        /// CLR Property Wrappers for PageLeftEnabledProperty
        /// </summary>
        //public bool PageLeftEnabled
        //{
        //    get
        //    {
        //        return (bool)GetValue(PageLeftEnabledProperty);
        //    }

        //    set
        //    {
        //        this.SetValue(PageLeftEnabledProperty, value);
        //    }
        //}

        ///// <summary>
        ///// CLR Property Wrappers for PageRightEnabledProperty
        ///// </summary>
        //public bool PageRightEnabled
        //{
        //    get
        //    {
        //        return (bool)GetValue(PageRightEnabledProperty);
        //    }

        //    set
        //    {
        //        this.SetValue(PageRightEnabledProperty, value);
        //    }
        //}

        /// <summary>
        /// Called when the KinectSensorChooser gets a new sensor
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="args">event arguments</param>
        private static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    }
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }

        /// <summary>
        /// Handle a button click from the wrap panel.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void KinectTileButtonClick(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(number_of_cows);
            cowCounter++;
            //The way this will work is it will decrement the number of cows after the new button is made
            if (cowCounter < number_of_cows)
                {
                    var button = (KinectTileButton)e.OriginalSource;

                    tipped_tracker.Content = "Cows tipped: " + cowCounter;
                    

                    //DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                    //myDoubleAnimation.From = 1.0;
                    //myDoubleAnimation.To = 0.0;
                    //myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
                    //myDoubleAnimation.AutoReverse = true;
                    //myDoubleAnimation.RepeatBehavior = !RepeatBehavior.Forever;

                    //myStoryboard = new Storyboard();
                    //myStoryboard.Children.Add(myDoubleAnimation);
                    //Storyboard.SetTargetName(myDoubleAnimation, myRectangle.Name);
                    //Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));

                    //// Use the Loaded event to start the Storyboard.
                    //myRectangle.Loaded += new RoutedEventHandler(myRectangleLoaded);
                    //myPanel.Children.Add(myRectangle);
                    //this.Content = myPanel;

                    //var old_thickness = button.Margin;
                    var r = new Random();
                    double left = r.Next(0, 1100);
                    double top = r.Next(0, 400);
                    double right = 1100 - left;
                    double bottom = 400 - top;
                    button.Margin = new Thickness(left, top, right, bottom);

                    startSound.Play();
                    e.Handled = true;
                }
            else
            {
                sw.Stop();
                //Console.WriteLine("YOU WIN");
                
                ts = sw.Elapsed;
                //Console.WriteLine(ts);
                this.NavigationService.Navigate(new cowTipWin(number_of_cows, ts));
                //(Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("Page1.xaml", UriKind.Relative);
            }
        }

        private void MyElapsedMethod(object sender, ElapsedEventArgs cowE, KinectTileButton flippedCow) {
            this.kinectRegionGrid.Children.Remove(flippedCow);
        }

        /// <summary>
        /// Handle paging right (next button).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PageRightButtonClick(object sender, RoutedEventArgs e)
        {
       //     scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + PixelScrollByAmount);
        }

        /// <summary>
        /// Handle paging left (previous button).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PageLeftButtonClick(object sender, RoutedEventArgs e)
        {
        //    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - PixelScrollByAmount);
        }

        /// <summary>
        /// Change button state depending on scroll viewer position
        /// </summary>
        private void UpdatePagingButtonState()
        {
       //     this.PageLeftEnabled = scrollViewer.HorizontalOffset > ScrollErrorMargin;
         //   this.PageRightEnabled = scrollViewer.HorizontalOffset < scrollViewer.ScrollableWidth - ScrollErrorMargin;
        }

        private void BackHomeButton_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();
            startSound.Stop();
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }
}
    

