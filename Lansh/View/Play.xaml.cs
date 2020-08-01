using AppStudio.Uwp;
using GalaSoft.MvvmLight.Messaging;
using Lansh.Model;
using Lansh.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Lansh.View
{

    public sealed partial class Play : Page
    {

        public Play()
        {
            this.InitializeComponent();
            RegisterEvent();
            RegisterMessenger();
        }

        #region Event

        /// <summary>
        /// Register all event
        /// </summary>
        private void RegisterEvent()
        {
            BackRequested.Click += BackRequested_Click;
            TopBackRequested.Click += TopBackRequested_Click;
        }

        private void BackRequested_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<object>(null, "BackRequested");
        }

        private void TopBackRequested_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<object>(null, "BackRequested");
        }

        #endregion

        #region Messenger

        /// <summary>
        /// Register messenger
        /// </summary>
        private void RegisterMessenger()
        {
            Messenger.Default.Register<MediaPlayer>(this, "ReSetMediaPlayer", ReSetMediaPlayer);
            Messenger.Default.Register<bool>(this, "PlayLoaded", PlayLoaded);
            Messenger.Default.Register<bool>(this, "AreTransportContro", AreTransportContro);
            Messenger.Default.Register<object>(this, "Error", ErrorMessenger);
        }

        #region ReSetMediaPlayer
        private void ReSetMediaPlayer(MediaPlayer mediaPlayer)
        {
            MediaPlayer.SetMediaPlayer(mediaPlayer);
            MediaPlayer.MediaPlayer.Play();
        }
        #endregion

        #region PlayLoaded
        private void PlayLoaded(bool isActive)
        {
            Load.IsActive = isActive;
        }
        #endregion

        #region AreTransportContro
        private void AreTransportContro(bool isEnabled)
        {
            MediaPlayer.AreTransportControlsEnabled = isEnabled;
        }
        #endregion

        #region Error
        private async void ErrorMessenger(object o)
        {
            Error.Visibility = Visibility.Visible;
            await Error.AnimateDoublePropertyAsync("Opacity", 0, 1.0, 500);
            await Task.Delay(1000);
            await Error.AnimateDoublePropertyAsync("Opacity", 1.0, 0, 500);
            Error.Visibility = Visibility.Collapsed;
        }
        #endregion

        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((PlayViewModel)DataContext).Param = e.Parameter;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (MediaPlayer.MediaPlayer != null)
                MediaPlayer.MediaPlayer.Pause();
        }
    }
}
