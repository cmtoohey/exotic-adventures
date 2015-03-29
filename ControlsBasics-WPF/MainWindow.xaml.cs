//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
   
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;


            //Not sure what this does
            if (Generics.LoadingStatus == 0)
            {
                //Sets MainMenu.xaml as the page for the main window
                _mainFrame.Source = new Uri("MainMenu.xaml", UriKind.Relative);
                Generics.LoadingStatus = 1;
            }

        }
       
       
    }
}
