using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;
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
        public Commands Commands { get { return new PhoneCommands(); } }

        private async void InitializeCounter()
        {
            CounterLabel.Text = await Persistence.ReadLast();
            var aux = int.Parse(CounterLabel.Text);
            CounterLabel.Text = aux.ToString("00000");
            Counter.GetInstance(aux);

            HideWarning();
            ShowPredictions();
            ShowMaxPace();
        }

        private void ShowMaxPace()
        {
            MaxPaceButton.Label = "Max pace: " + Counter.MaxPace;
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
            if (PredictionLabel.Visibility == Visibility.Visible)
            {
                PredictionLabel.Text = p;
                ShowMaxPace();
            }
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
            Rate();
        }

        private static void Rate()
        {
            Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:reviewapp?appid=4b4ad23b-5625-40fa-82a7-59f9e8e67f01"));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            var content = "Clickr Counter\n" +
                                   "Developed by Thiago Garcia\n\n" +
                                   "Hope you find it app helpful. It was for me " +
                                   "when I needed a counter and didn't have the " +
                                   "real counter machine.\n" +
                                   "Thanks for downloading! If you liked and have a minute" +
                                   ", please, rate it. I'd really appreciate :)\n\n" +
                                   "If you have any questions, mail me: " +
                                   Commands.Email;
            var msgbox = new MessageDialog(content, "About");
            msgbox.Commands.Add(new UICommand("Sure, rate this app!") { Invoked = command => Rate() });
            msgbox.Commands.Add(new UICommand("Mail developer") { Invoked = command => Commands.Mail() });

            msgbox.ShowAsync();
        }

        private void InstructionsButton_Click(object sender, RoutedEventArgs e)
        {
            var content = "Clickr Counter Instructions\n\n" +
                          " - Counter       -> Just tap anywhere in the image and you'll be " +
                          "counting anything you want.\n" +
                          " - Reset Counter -> Double tap that black ugly-rounded button on the middle right.\n" +
                          " - Pace          -> It shows the average speed you're counting following " +
                          "these rules: 1) If you hide predictions, it will restart the pace " +
                          "counting when you show predictions again; 2) If you close the app, " +
                          "speed counting will restart too even thought it saves the last count number.\n" +
                          " - Functionality -> The app stores the last count number every second, and when started, " +
                          "the count begins from the number it was before.\n" +
                          " - Max Pace      -> Max pace for the session. I bet you can have some fun " +
                          "competing with others, or maybe predicting what you need without counting " +
                          "until the end :)\n\n" +
                            "If you have any questions, mail me: " +
                            Commands.Email;
            var msgbox = new MessageDialog(content, "About");
            msgbox.Commands.Add(new UICommand("OK"));
            msgbox.Commands.Add(new UICommand("Mail developer") { Invoked = command => Commands.Mail() });
            msgbox.CancelCommandIndex = 0;

            msgbox.ShowAsync();
        }
    }
}
