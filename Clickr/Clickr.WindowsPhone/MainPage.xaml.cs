using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Clickr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            HideWarning();
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Count();
        }

        private void Reset_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShowWarning();
        }

        private void ResetLabel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Reset();
        }

        private void ResetLabelWarn_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Reset();
        }

        private void ResetLabelWarn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Count();
        }

        private void HideWarning()
        {
            if (ResetLabelWarn.Visibility == Visibility.Visible)
                ResetLabelWarn.Visibility = Visibility.Collapsed;
        }

        private void ShowWarning()
        {
            if (ResetLabelWarn.Visibility == Visibility.Collapsed)
                ResetLabelWarn.Visibility = Visibility.Visible;
        }

        private void Count()
        {
            CounterLabel.Text = Counter.GetInstance().Click().ToString("00000");
            HideWarning();
        }

        private void Reset()
        {
            CounterLabel.Text = Counter.GetInstance().Reset().ToString("00000");
            HideWarning();
        }
    }
}
