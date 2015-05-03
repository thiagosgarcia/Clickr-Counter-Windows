using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
            InitializeCounter();
        }

        public Counter Counter { get { return Counter.GetInstance(); } }
        public Persistence Persistence { get { return Persistence.GetInstance(); } }

        private async void InitializeCounter()
        {
            CounterLabel.Text = await Persistence.ReadLast();
            var aux = int.Parse(CounterLabel.Text);
            CounterLabel.Text = aux.ToString("00000");
            Counter.GetInstance(aux);

            HideWarning();
            ShowPredictions();
        }

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
            Predict();
        }

        private void Predict()
        {
            var p = Counter.Predict();
            if(PredictionLabel.Visibility == Visibility.Visible)
                PredictionLabel.Text = p;
        }

        private void Reset()
        {
            CounterLabel.Text = Counter.Reset().ToString("00000");
            HideWarning();
            Predict();
        }

        private void ShowPredictionsButton_Checked(object sender, RoutedEventArgs e)
        {
            ShowPredictions();
        }

        private void ShowPredictionsButton_Unchecked(object sender, RoutedEventArgs e)
        {
            HidePredictions();
        }

        private void HidePredictions()
        {
            if (PredictionLabel == null)
                return;
            PredictionLabel.Visibility = Visibility.Collapsed;
            PredictionLabel.Text = "Pace";
            ShowPredictionsButton.Label = "Show Predictions";
            Counter.ShowPredictions = false;
        }

        private void ShowPredictions()
        {
            if (PredictionLabel == null)
                return;
            PredictionLabel.Visibility = Visibility.Visible;
            ShowPredictionsButton.Label = "Hide Predictions";
            Counter.ShowPredictions = true;
        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Count();
        }

        private void RateAndReviewButton_Clicked(object sender, RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:reviewapp?appid=4b4ad23b-5625-40fa-82a7-59f9e8e67f01"));
        }
    }
}
