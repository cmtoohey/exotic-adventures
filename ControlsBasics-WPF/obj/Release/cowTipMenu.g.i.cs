﻿#pragma checksum "..\..\cowTipMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9E832E1D374448A04529163686E7043"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Samples.Kinect.ControlsBasics;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Microsoft.Samples.Kinect.ControlsBasics {
    
    
    /// <summary>
    /// cowTipMenu
    /// </summary>
    public partial class cowTipMenu : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.KinectSensorChooserUI sensorChooserUi;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.Controls.KinectRegion cowMenuRegion;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid cowMenuGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.Controls.KinectTileButton small;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.Controls.KinectTileButton medium;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.Controls.KinectTileButton large;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\cowTipMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Kinect.Toolkit.Controls.KinectTileButton goHome;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/exotic_adventures;component/cowtipmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\cowTipMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.sensorChooserUi = ((Microsoft.Kinect.Toolkit.KinectSensorChooserUI)(target));
            return;
            case 2:
            this.cowMenuRegion = ((Microsoft.Kinect.Toolkit.Controls.KinectRegion)(target));
            return;
            case 3:
            this.cowMenuGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.small = ((Microsoft.Kinect.Toolkit.Controls.KinectTileButton)(target));
            
            #line 24 "..\..\cowTipMenu.xaml"
            this.small.Click += new System.Windows.RoutedEventHandler(this.smallClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.medium = ((Microsoft.Kinect.Toolkit.Controls.KinectTileButton)(target));
            
            #line 25 "..\..\cowTipMenu.xaml"
            this.medium.Click += new System.Windows.RoutedEventHandler(this.mediumClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.large = ((Microsoft.Kinect.Toolkit.Controls.KinectTileButton)(target));
            
            #line 26 "..\..\cowTipMenu.xaml"
            this.large.Click += new System.Windows.RoutedEventHandler(this.largeClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.goHome = ((Microsoft.Kinect.Toolkit.Controls.KinectTileButton)(target));
            
            #line 27 "..\..\cowTipMenu.xaml"
            this.goHome.Click += new System.Windows.RoutedEventHandler(this.homeClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

