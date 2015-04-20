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
            var regionSensorBinding = new Binding("Kinect") { Source = Intro.sensorChooser };
            BindingOperations.SetBinding(this.kinectRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

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

            add_new_button(new KinectTileButton(),
                                new Thickness(r.Next(0,550), r.Next(0, 220),
                                    r.Next(0,550), r.Next(0, 220)));


            sw.Start();
            // Bind listner to scrollviwer scroll position change, and check scroll viewer position
            //this.UpdatePagingButtonState();
            //scrollViewer.ScrollChanged += (o, e) => this.UpdatePagingButtonState();
        }

        private void add_new_button(KinectTileButton button, Thickness thickness)
        {
            Uri resourceUri = new Uri("Cow.jpg", UriKind.Relative);
            System.Windows.Resources.StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            System.Windows.Media.Imaging.BitmapFrame img = System.Windows.Media.Imaging.BitmapFrame.Create(streamInfo.Stream);
            var brush = new System.Windows.Media.ImageBrush();

            brush.ImageSource = img;
            button.Margin = thickness;
            button.Click += this.KinectTileButtonClick;
            button.MinHeight = 0;
            button.Background = brush;
            this.kinectRegionGrid.Children.Add(button);
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
                button.IsEnabled = false;
                //button.Visibility = System.Windows.Visibility.Visible;
                tipped_tracker.Content = "Cows tipped: " + cowCounter;

                DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                myDoubleAnimation.From = button.Height;
                myDoubleAnimation.To = 0.0;
                myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                //myDoubleAnimation.AutoReverse = true;
                //myDoubleAnimation.RepeatBehavior = "1";

                var myStoryboard = new Storyboard();
                myStoryboard.Children.Add(myDoubleAnimation);
                Storyboard.SetTarget(myDoubleAnimation, button);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(KinectTileButton.HeightProperty));

                myStoryboard.Begin();
                 //   //// Use the Loaded event to start the Storyboard.
                 //var myRectangle.Loaded += new RoutedEventHandler(myRectangleLoaded);
                 //myPanel.Children.Add(myRectangle);
                 //this.Content = myPanel;

                    //var old_thickness = button.Margin;
                var r = new Random();
                double left = r.Next(0, 1100);
                double top = r.Next(0, 400);
                double right = 1100 - left;
                double bottom = 400 - top;
                //button.Margin = new Thickness(left, top, right, bottom);
                add_new_button(new KinectTileButton(), new Thickness(left, top, right, bottom));
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
            }
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
            this.NavigationService.Navigate(new MainMenu());  
        }
    }
}
    

