using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using System.Windows.Navigation;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    public class NavigateButton : KinectTileButton
    {
        public Uri NavigateUri { get; set; }

        public NavigateButton()
        {
            Click += NavigateButton_Click;
        }

        void NavigateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
                navigationService.Navigate(NavigateUri);
        }
    }
}
