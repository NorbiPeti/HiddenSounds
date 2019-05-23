using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

namespace HiddenUpdater
{
    internal class Program
    {
        private const string PlaylistId = "2YDWLcBzTpDNQDfcQyy76b";

        public static async Task Main(string[] args)
        {
            var auth = new CredentialsAuth("ce11c54b88cf41149e528de5ec73aa69", File.ReadAllText("secret.txt"));
            var token = await auth.GetToken();
            var spotify = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };
            var playlist = spotify.GetPlaylist(PlaylistId);
            Console.WriteLine(playlist);
            var playlistJson = new JArray();
            var tracks = playlist.Tracks;
            Console.WriteLine("Total: " + tracks.Total);
            int C = 0;
            for (int i = 0; i < tracks.Total; i+=100)
            {
                if (i > 0)
                    tracks = spotify.GetPlaylistTracks(PlaylistId, offset: i);
                foreach (var track in tracks.Items)
                {
                    Console.WriteLine("Track: "+track.Track.Name);
                    C++;
                }
            }

            Console.WriteLine(C + " / " + tracks.Total);
        }
    }
}