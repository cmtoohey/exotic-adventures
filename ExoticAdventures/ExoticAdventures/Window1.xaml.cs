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
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace ExoticAdventures
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        KinectSensor sensor;
        Skeleton[] skeletons;

        //    bool isCirclesVisible = true;

        bool isForwardGestureActive = false;
        bool isBackGestureActive = false;
        SolidColorBrush activeBrush = new SolidColorBrush(Colors.Green);
        SolidColorBrush inactiveBrush = new SolidColorBrush(Colors.Red);

        public Window1()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
            
        }

         private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors.FirstOrDefault();

            if (sensor == null)
            {
                MessageBox.Show("This application requires a Kinect Sensor.");
                this.Close();
            }

            sensor.Start();

            sensor.SkeletonStream.Enable();
            sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(sensor_SkeletonFrameReady);

            Application.Current.Exit += new ExitEventHandler(Window_Closed);
        }

        //This method is used to position the ellipses on the canvas
        //according to correct movements of the tracked joints.
        private void setEllipsePosition(Ellipse ellipse, Joint joint, bool ishighlighted)
        {
            if (ishighlighted)
            {
                ellipse.Width = 60;
                ellipse.Height = 60;
                ellipse.Fill = activeBrush;
            }
            else
            {
                ellipse.Width = 20;
                ellipse.Height = 20;
                ellipse.Fill = inactiveBrush;
            }

            CoordinateMapper mapper = sensor.CoordinateMapper;

            var point = mapper.MapSkeletonPointToColorPoint(joint.Position, sensor.ColorStream.Format);

            Canvas.SetLeft(ellipse, point.X - ellipse.ActualWidth / 2);
            Canvas.SetTop(ellipse, point.Y - ellipse.ActualHeight / 2);
        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                    return;

                if (skeletons == null ||
                    skeletons.Length != skeletonFrame.SkeletonArrayLength)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                }
                skeletonFrame.CopySkeletonDataTo(skeletons);

            }
            Skeleton closestSkeleton = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked)
                .OrderBy(s => s.Position.Z * Math.Abs(s.Position.X))
                .FirstOrDefault();

            if (closestSkeleton == null)
                return;

            var head = closestSkeleton.Joints[JointType.Head];
            var rightHand = closestSkeleton.Joints[JointType.HandRight];
            var leftHand = closestSkeleton.Joints[JointType.HandLeft];

            if (head.TrackingState == JointTrackingState.NotTracked ||
                rightHand.TrackingState == JointTrackingState.NotTracked ||
                leftHand.TrackingState == JointTrackingState.NotTracked)
            {
                //Don't have a good read on the joints so we cannot process gestures
                return;
            }

            setEllipsePosition(headCircle1, head, false);
            setEllipsePosition(leftCircle1, leftHand, isBackGestureActive);
            setEllipsePosition(rightCircle1, rightHand, isForwardGestureActive);

            ProcessForwardBackGesture(head, rightHand, leftHand);
        }

        private void ProcessForwardBackGesture(Joint head, Joint rightHand, Joint leftHand)
        {
            if (rightHand.Position.X > head.Position.X + 0.45)
            {
                if (!isForwardGestureActive)
                {
                    isForwardGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                  
                }
            }
            else
            {
                isForwardGestureActive = false;
            }

            if (leftHand.Position.X < head.Position.X - 0.45)
            {
                if (!isBackGestureActive)
                {
                    isBackGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Left}");
                    //open main window
                    MainWindow main = new MainWindow();
                    this.Close();
                    main.Show();
                }
            }
            else
            {
                isBackGestureActive = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //if (sensor != null)
            //{
            //    sensor.Stop();
            //    sensor.Dispose();
            //    sensor = null;
            //}
        }
    }


    }
