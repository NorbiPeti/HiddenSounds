using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace HiddenUpdater
{
    public class StoreLyrics
    {
        public static void Store()
        {
            do
            {
                Console.Write("One artist: ");
                string artist = Console.ReadLine();
                if (artist.Length == 0) break;
                Console.Write("Title: ");
                string title = Console.ReadLine();
                if (artist.Length == 0) break;
                var songs = JArray.Parse(File.ReadAllText("songs.json"));
                foreach (var song in songs)
                {
                    if (((string) song["sname"]).Contains(title))
                    {
                        Console.WriteLine("Title matches: " + song["artists"] + " - " + song["sname"]);
                        if (song["artists"].Any(a => ((string) a).Contains(artist)))
                        {
                            Console.WriteLine("Artist matches");
                            /*Console.WriteLine("Lyrics (type \"END\" when done):");
                            string lyrics = "";
                            do
                            {
                                var line = Console.ReadLine();
                                Console.WriteLine("Line: " + line);
                                if (line == "END") break;
                                lyrics += "  " + line + "\n";
                            } while (true);*/
                            File.WriteAllText("lyrics.txt", "");
                            Console.WriteLine("Put the lyrics in lyrics.txt and press Enter");
                            Console.ReadLine();
                            
                            string lyrics = "";
                            foreach(string line in File.ReadLines("lyrics.txt")) {
                                Console.WriteLine("Line: " + line);
                                lyrics += "    " + line + "\n";
                            }

                            File.AppendAllText("songextra.yml", "- sid: " + song["sid"] + "\n  lyrics: |\n" + lyrics);
                            break;
                        }
                    }
                }
            } while (true);
        }
    }
}