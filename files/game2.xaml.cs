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
using Microsoft.Samples.Kinect.WpfViewers;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    /// <summary>
    /// Interaction logic for game2.xaml
    /// </summary>
    public partial class game2 : Page
    {

        private const int TimerResolution = 2;  // ms
        private const int NumIntraFrames = 3;
        private const int MaxShapes = 80;
        private const double MaxFramerate = 70;
        private const double MinFramerate = 15;
        private const double MinShapeSize = 12;
        private const double MaxShapeSize = 90;
        private const double DefaultDropRate = 2.5;
        private const double DefaultDropSize = 32.0;
        private const double DefaultDropGravity = 1.0;

        private readonly Dictionary<int, Player> players = new Dictionary<int, Player>();
        //private readonly SoundPlayer popSound = new SoundPlayer();
        //private readonly SoundPlayer hitSound = new SoundPlayer();
        //private readonly SoundPlayer squeezeSound = new SoundPlayer();
        private readonly KinectSensorChooser sensorChooser = new KinectSensorChooser();

        private double dropRate = DefaultDropRate;
        private double dropSize = DefaultDropSize;
        private double dropGravity = DefaultDropGravity;
        private DateTime lastFrameDrawn = DateTime.MinValue;
        private DateTime predNextFrame = DateTime.MinValue;
        private double actualFrameTime;

        private Skeleton[] skeletonData;

        // Player(s) placement in scene (z collapsed):
        private Rect playerBounds;
        private Rect screenRect;

        private double targetFramerate = MaxFramerate;
        private int frameCount;
        private bool runningGameThread;
        private FallingThings myFallingThings;
        private int playersAlive;

    //    private SpeechRecognizer mySpeechRecognizer;

        
        public static readonly DependencyProperty KinectSensorManagerProperty =
           DependencyProperty.Register(
               "KinectSensorManager",
               typeof(KinectSensorManager),
               typeof(game2),
               new PropertyMetadata(null));

        public game2()
        {
               this.KinectSensorManager = new KinectSensorManager();
               this.KinectSensorManager.KinectSensorChanged += this.KinectSensorChanged;
               this.DataContext = this.KinectSensorManager;

            InitializeComponent();
            this.SensorChooserUI.KinectSensorChooser = sensorChooser;
            sensorChooser.Start();
            //var regionSensorBinding = new Binding("Kinect") { Source = MainMenu.sensorChooser };
            //BindingOperations.SetBinding(this.game2PlayRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

         //   MainMenu.sensorChooser.KinectChanged -= SensorChooserOnKinectChanged;
            MainMenu.sensorChooser.Stop();

            // Bind the KinectSensor from the sensorChooser to the KinectSensor on the KinectSensorManager
            var kinectSensorBinding = new Binding("Kinect") { Source = this.sensorChooser };
            BindingOperations.SetBinding(this.KinectSensorManager, KinectSensorManager.KinectSensorProperty, kinectSensorBinding);


            playfield.ClipToBounds = true;

            this.myFallingThings = new FallingThings(MaxShapes, this.targetFramerate, NumIntraFrames);

            this.UpdatePlayfieldSize();

            this.myFallingThings.SetGravity(this.dropGravity);
            this.myFallingThings.SetDropRate(this.dropRate);
            this.myFallingThings.SetSize(this.dropSize);
        //    this.myFallingThings.SetPolies(PolyType.All);
        //    this.myFallingThings.SetGameMode(GameMode.Off);

        //    this.popSound.Stream = Properties.Resources.Pop_5;
        //    this.hitSound.Stream = Properties.Resources.Hit_2;
        //    this.squeezeSound.Stream = Properties.Resources.Squeeze;

        //    this.popSound.Play();

        //    TimeBeginPeriod(TimerResolution);
        //    var myGameThread = new Thread(this.GameThread);
        //    myGameThread.SetApartmentState(ApartmentState.STA);
        //    myGameThread.Start();

        //    FlyingText.NewFlyingText(this.screenRect.Width / 30, new Point(this.screenRect.Width / 2, this.screenRect.Height / 2), "Shapes!");

        }


        public KinectSensorManager KinectSensorManager
        {
            get { return (KinectSensorManager)GetValue(KinectSensorManagerProperty); }
            set { SetValue(KinectSensorManagerProperty, value); }
        }

        #region Kinect discovery + setup

        private void KinectSensorChanged(object sender, KinectSensorManagerEventArgs<KinectSensor> args)
        {
            if (null != args.OldValue)
            {
                this.UninitializeKinectServices(args.OldValue);
            }

            // Only enable this checkbox if we have a sensor
            //enableAec.IsEnabled = null != args.NewValue;

            if (null != args.NewValue)
            {
                this.InitializeKinectServices(this.KinectSensorManager, args.NewValue);
            }
        }

        // Kinect enabled apps should customize which Kinect services it initializes here.
        private void InitializeKinectServices(KinectSensorManager kinectSensorManager, KinectSensor sensor)
        {
            // Application should enable all streams first.
            kinectSensorManager.ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;
            kinectSensorManager.ColorStreamEnabled = true;

            sensor.SkeletonFrameReady += this.SkeletonsReady;
            kinectSensorManager.TransformSmoothParameters = new TransformSmoothParameters
            {
                Smoothing = 0.5f,
                Correction = 0.5f,
                Prediction = 0.5f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            };
            kinectSensorManager.SkeletonStreamEnabled = true;
            kinectSensorManager.KinectSensorEnabled = true;

            //if (!kinectSensorManager.KinectSensorAppConflict)
            //{
            //    // Start speech recognizer after KinectSensor started successfully.
            //    this.mySpeechRecognizer = SpeechRecognizer.Create();

            //    if (null != this.mySpeechRecognizer)
            //    {
            //        this.mySpeechRecognizer.SaidSomething += this.RecognizerSaidSomething;
            //        this.mySpeechRecognizer.Start(sensor.AudioSource);
            //    }

            //    enableAec.Visibility = Visibility.Visible;
            //    this.UpdateEchoCancellation(this.enableAec);
            //}
        }

        // Kinect enabled apps should uninitialize all Kinect services that were initialized in InitializeKinectServices() here.
        private void UninitializeKinectServices(KinectSensor sensor)
        {
            sensor.SkeletonFrameReady -= this.SkeletonsReady;

        //    if (null != this.mySpeechRecognizer)
        //    {
        //        this.mySpeechRecognizer.Stop();
        //        this.mySpeechRecognizer.SaidSomething -= this.RecognizerSaidSomething;
        //        this.mySpeechRecognizer.Dispose();
        //        this.mySpeechRecognizer = null;
        //    }

        //    enableAec.Visibility = Visibility.Collapsed;
        }

        #endregion Kinect discovery + setup


        #region Kinect Skeleton processing
        private void SkeletonsReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    int skeletonSlot = 0;

                    if ((this.skeletonData == null) || (this.skeletonData.Length != skeletonFrame.SkeletonArrayLength))
                    {
                        this.skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }

                    skeletonFrame.CopySkeletonDataTo(this.skeletonData);

                    foreach (Skeleton skeleton in this.skeletonData)
                    {
                        if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                        {
                            Player player;
                            if (this.players.ContainsKey(skeletonSlot))
                            {
                                player = this.players[skeletonSlot];
                            }
                            else
                            {
                                player = new Player(skeletonSlot);
                                player.SetBounds(this.playerBounds);
                                this.players.Add(skeletonSlot, player);
                            }

                            player.LastUpdated = DateTime.Now;

                            // Update player's bone and joint positions
                            if (skeleton.Joints.Count > 0)
                            {
                                player.IsAlive = true;

                                // Head, hands, feet (hit testing happens in order here)
                                player.UpdateJointPosition(skeleton.Joints, JointType.Head);
                                player.UpdateJointPosition(skeleton.Joints, JointType.HandLeft);
                                player.UpdateJointPosition(skeleton.Joints, JointType.HandRight);
                                player.UpdateJointPosition(skeleton.Joints, JointType.FootLeft);
                                player.UpdateJointPosition(skeleton.Joints, JointType.FootRight);

                                // Hands and arms
                                player.UpdateBonePosition(skeleton.Joints, JointType.HandRight, JointType.WristRight);
                                player.UpdateBonePosition(skeleton.Joints, JointType.WristRight, JointType.ElbowRight);
                                player.UpdateBonePosition(skeleton.Joints, JointType.ElbowRight, JointType.ShoulderRight);

                                player.UpdateBonePosition(skeleton.Joints, JointType.HandLeft, JointType.WristLeft);
                                player.UpdateBonePosition(skeleton.Joints, JointType.WristLeft, JointType.ElbowLeft);
                                player.UpdateBonePosition(skeleton.Joints, JointType.ElbowLeft, JointType.ShoulderLeft);

                                // Head and Shoulders
                                player.UpdateBonePosition(skeleton.Joints, JointType.ShoulderCenter, JointType.Head);
                                player.UpdateBonePosition(skeleton.Joints, JointType.ShoulderLeft, JointType.ShoulderCenter);
                                player.UpdateBonePosition(skeleton.Joints, JointType.ShoulderCenter, JointType.ShoulderRight);

                                // Legs
                                //player.UpdateBonePosition(skeleton.Joints, JointType.HipLeft, JointType.KneeLeft);
                                //player.UpdateBonePosition(skeleton.Joints, JointType.KneeLeft, JointType.AnkleLeft);
                                //player.UpdateBonePosition(skeleton.Joints, JointType.AnkleLeft, JointType.FootLeft);

                                //player.UpdateBonePosition(skeleton.Joints, JointType.HipRight, JointType.KneeRight);
                                //player.UpdateBonePosition(skeleton.Joints, JointType.KneeRight, JointType.AnkleRight);
                                //player.UpdateBonePosition(skeleton.Joints, JointType.AnkleRight, JointType.FootRight);

                                //player.UpdateBonePosition(skeleton.Joints, JointType.HipLeft, JointType.HipCenter);
                                //player.UpdateBonePosition(skeleton.Joints, JointType.HipCenter, JointType.HipRight);

                                // Spine
                                player.UpdateBonePosition(skeleton.Joints, JointType.HipCenter, JointType.ShoulderCenter);
                            }
                        }

                        skeletonSlot++;
                    }
                }
            }
        }

        private void CheckPlayers()
        {
            foreach (var player in this.players)
            {
                if (!player.Value.IsAlive)
                {
                    // Player left scene since we aren't tracking it anymore, so remove from dictionary
                    this.players.Remove(player.Value.GetId());
                    break;
                }
            }

            // Count alive players
            int alive = this.players.Count(player => player.Value.IsAlive);

            if (alive != this.playersAlive)
            {
                //if (alive == 2)
                //{
                //    this.myFallingThings.SetGameMode(GameMode.TwoPlayer);
                //}
                //else if (alive == 1)
                //{
                //    this.myFallingThings.SetGameMode(GameMode.Solo);
                //}
                //else if (alive == 0)
                //{
                //    this.myFallingThings.SetGameMode(GameMode.Off);
                //}

                //if ((this.playersAlive == 0) && (this.mySpeechRecognizer != null))
                //{
                //    BannerText.NewBanner(
                //        Properties.Resources.Vocabulary,
                //        this.screenRect,
                //        true,
                //        System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
                //}

                this.playersAlive = alive;
            }
        }

        private void PlayfieldSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdatePlayfieldSize();
        }

        private void UpdatePlayfieldSize()
        {
            // Size of player wrt size of playfield, putting ourselves low on the screen.
            this.screenRect.X = 0;
            this.screenRect.Y = 0;
            this.screenRect.Width = this.playfield.ActualWidth;
            this.screenRect.Height = this.playfield.ActualHeight;

            //BannerText.UpdateBounds(this.screenRect);

            this.playerBounds.X = 0;
            this.playerBounds.Width = this.playfield.ActualWidth;
            this.playerBounds.Y = this.playfield.ActualHeight * 0.2;
            this.playerBounds.Height = this.playfield.ActualHeight * 0.75;

            foreach (var player in this.players)
            {
                player.Value.SetBounds(this.playerBounds);
            }

            Rect fallingBounds = this.playerBounds;
            fallingBounds.Y = 0;
            fallingBounds.Height = playfield.ActualHeight;
            if (this.myFallingThings != null)
            {
                this.myFallingThings.SetBoundaries(fallingBounds);
            }
        }
        #endregion Kinect Skeleton processing

        private void BackHomeButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("_mainFrame") as Frame).Source = new Uri("MainMenu.xaml", UriKind.Relative);
        }
    }
}
