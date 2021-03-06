﻿using System;
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
    /// Interaction logic for game2Win.xaml
    /// </summary>
    public partial class game2Win : Page
    {
      
        private Label gameScoreLabel = new Label();
        public game2Win(int game_score)
        {
            InitializeComponent();
            var regionSensorBinding = new Binding("Kinect") { Source = Intro.sensorChooser };
            BindingOperations.SetBinding(this.game2WinRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);
           

            gameScoreLabel.Margin = new Thickness(60, 10, 0, 0);
            gameScoreLabel.Height = 250;
            gameScoreLabel.Width = 700;
            gameScoreLabel.Content = "Your score is " + game_score.ToString() + "!!";
            gameScoreLabel.FontSize = 65;
            gameScoreLabel.Foreground = System.Windows.Media.Brushes.Black;
            this.game2WinGrid.Children.Add(gameScoreLabel);
        }

        private void game2MenuClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new game2Menu());  
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());  
        }
    }
}
