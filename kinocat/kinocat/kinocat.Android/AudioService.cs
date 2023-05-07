using System;
using Xamarin.Forms;
using kinocat.Droid;
using Android.Media;
using Android.Content.Res;
using kinocat;

[assembly: Dependency(typeof(AudioService))]
namespace kinocat.Droid
{
    public class AudioService : IAudio
    {
        public AudioService()
        {
        }

        public void PlayAudioFile()
        {
            MainActivity.player.Start();
        }
    }
}