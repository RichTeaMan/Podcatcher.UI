using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.BackgroundAudio;
using Podcatcher.UI.ViewModel;
using Podcatcher.UI.AudioPlayback;

namespace Podcatcher.UI
{
    public partial class PlayerPage : PhoneApplicationPage
    {
        public PodcastTrack Track { get; private set; }

        public PlayerPage()
        {
            InitializeComponent();

            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
        }

        #region Button Click Event Handlers

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            // BackgroundAudioPlayer.Instance.SkipPrevious();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                var audioTrack = new AudioTrack(new Uri(Track.Title + ".mp3", UriKind.Relative),
                    Track.Title,
                    "Podcast Artist",
                    "Podcast Album",
                    null);
                AudioPlayer.Play(audioTrack);
                //BackgroundAudioPlayer.Instance.Track = audioTrack;
                
                //BackgroundAudioPlayer.Instance.Play();
                Console.WriteLine(BackgroundAudioPlayer.Instance.Error);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }

        #endregion Button Click Event Handlers

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    playButton.Content = "pause";
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    playButton.Content = "play";
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var track = this.GetParam<PodcastTrack>();
            DataContext = track;
            Track = track;

            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                playButton.Content = "pause";
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                 " by " +
                                 BackgroundAudioPlayer.Instance.Track.Artist;
                
            }
            else
            {
                playButton.Content = "play";
                txtCurrentTrack.Text = "";
            }
        }
    }
}