using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            Console.Write("Download (1) or lyrics (2): ");
            if (int.Parse(Console.ReadLine()) != 1)
            {
                StoreLyrics.Store();
                return;
            }
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
            var artistJson = new JObject();
            for (int i = 0; i < tracks.Total; i += 100)
            {
                if (i > 0)
                    tracks = spotify.GetPlaylistTracks(PlaylistId, offset: i);
                foreach (var track in tracks.Items.Select(tr => tr.Track))
                {
                    var obj = new JObject();
                    obj["sname"] = track.Name;
                    /*obj["artists"] = new JArray(track.Artists.Select(artist => new JObject
                    {
                        {"name", artist.Name},
                        {"url", artist.ExternalUrls["spotify"]}
                    }));*/
                    var artJson = new JArray();
                    foreach (var artist in track.Artists)
                    {
                        if (!artistJson.ContainsKey(artist.Name))
                            artistJson[artist.Name] = new JObject
                            {
                                {"sname", artist.Name},
                                {"surl", artist.ExternalUrls["spotify"]},
                                {"sid", artist.Id}
                            };
                        artJson.Add(artist.Name);
                    }

                    obj["artists"] = artJson;
                    obj["popularity"] = track.Popularity;
                    obj["durationMs"] = track.DurationMs;
                    obj["surl"] = track.ExternUrls["spotify"];
                    obj["sid"] = track.Id;

                    playlistJson.Add(obj);
                    C++;
                }
            }

            Console.WriteLine(C + " / " + tracks.Total);

            Console.WriteLine("Getting artists...");
            C = 0;
            int max = artistJson.Count;
            foreach (var kv in artistJson)
            {
                var artist = spotify.GetArtist((string) kv.Value["sid"]);
                var artJson = kv.Value;
                artJson["followers"] = artist?.Followers?.Total;
                artJson["popularity"] = artist?.Popularity;
                artJson["genres"] = new JArray(artist?.Genres);
                C++;
                if (C % 10 == 0) Console.Write("\r" + C + "/" + max + " - " + (C / (float) max) * 100 + "%");
            }

            Console.WriteLine("\r" + C + "/" + max + " - " + (C / (float) max) * 100 + "%");

            //Console.WriteLine(artistJson.ToString(Formatting.None));
            File.WriteAllText("songs.json", new JArray(playlistJson.OrderBy(song=>song["sid"])).ToString());
            File.WriteAllText("artists.json", new JArray(artistJson.Children().Select(tk=>tk.Last)
                .OrderBy(artist=>artist["sname"])).ToString());
        }
    }
}