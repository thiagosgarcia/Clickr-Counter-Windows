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
            HideWarning();
            ShowPredictions();
        }

        public Counter Counter { get { return Counter.GetInstance(); } }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HideWarning();
            Count();
        }

        private void Reset_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShowWarning();
            Count();
        }

        private void ResetLabel_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Reset();

            Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:reviewapp?appid=4b4ad23b-5625-40fa-82a7-59f9e8e67f01"));
        }

        private void ResetLabelWarn_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            HideWarning();
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
            CounterLabel.Text = Counter.Click().ToString("00000");
            Counter.ShowPredictions = true;
            Predict();
        }

        private void Predict()
        {
            var p = Counter.Predict();
            if (PredictionLabel.Visibility == Visibility.Visible)
                PredictionLabel.Text = p;
        }

        private void Reset()
        {
            CounterLabel.Text = Counter.Reset().ToString("00000");
            HideWarning();
            Predict();
        }

        private void ShowPredictions()
        {
            if (PredictionLabel == null)
                return;
            PredictionLabel.Visibility = Visibility.Visible;
            Counter.ShowPredictions = true;
        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Count();
        }
    }
}
