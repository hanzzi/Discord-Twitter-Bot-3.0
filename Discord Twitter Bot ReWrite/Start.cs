using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace Discord_Twitter_Bot_ReWrite
{
    class Start
    {
        static void Main(string[] args) => new Start().StartBot();

        private static DiscordClient _client;
       
        public void StartBot()
        {
            // Disables suppression of Exceptions from now on all Tweetinvi Exceptions are thrown if applicable
            ExceptionHandler.SwallowWebExceptions = false;

            _client = new DiscordClient(x =>
            {
                x.AppName = "Twitter Bot";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            LoadXML.Main();

            Authorization.GetAuthUrl();

            Login.Navigate();

            

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect(ConfigInfo.DiscordToken, TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine($"[{e.Severity}] [{e.Source}] [{e.Message}] [{e.Exception}]");
            }
            else
            {
                Console.WriteLine($"[{e.Severity}] [{e.Source}] [{e.Message}]");
            }
        }

        public static void CreateCommands()
        {
            var CService = _client.GetService<CommandService>();

            ISampleStream SampleStream = Tweetinvi.Stream.CreateSampleStream();
            IFilteredStream FilteredStream = Tweetinvi.Stream.CreateFilteredStream();

            CService.CreateCommand("State")
                .Alias("StreamState", "SS")
                .Description("Displays whether the streams are running or not")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage($"Filtered Stream: {FilteredStream.StreamState} {Environment.NewLine}Sample Stream: {SampleStream.StreamState}");
                });

            CService.CreateCommand("PurgeStreams")
                .Alias("PS", "Purge")
                .Description("Purges all running streams")
                .Do(async (e) =>
                {
                    SampleStream.StopStream();
                    FilteredStream.StopStream();
                    FilteredStream.ClearTracks();
                    await e.Channel.SendMessage($"```Sample Stream: {SampleStream.StreamState} {Environment.NewLine}Filtered Stream: {FilteredStream.StreamState}```");
                });

            CService.CreateCommand("Sample")
                .Description("Initializes a socalled sample stream but instead of 1% its specified with an amount of random tweets, Neat!")
                .Parameter("Tweets", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if (Regex.IsMatch(e.GetArg("Tweets"), @"-?\d+(\.\d+)?"))
                    {
                        int Tweets = Convert.ToInt32(e.GetArg("Tweets"));
                        await e.Channel.SendMessage("Transmitting " + e.GetArg("Tweets") + " Tweets");

                        Console.WriteLine(Tweetinvi.User.GetAuthenticatedUser());
                        int i = 0;
                        SampleStream.TweetReceived += (sender, args) =>
                        {

                            if (i < Tweets)
                            {
                                e.Channel.SendMessage(args.Tweet.Text.ToString());
                                i++;
                            }
                            else
                            {
                                SampleStream.StopStream();
                            }
                        };
                        SampleStream.StreamStopped += (sender, args) =>
                        {
                            var Exception = args.Exception;
                            var DisconnectMessage = args.DisconnectMessage;
                            e.Channel.SendMessage($"```Stream ended { Environment.NewLine }Exception: { Exception } { Environment.NewLine }Disconnect message: { DisconnectMessage }```");

                        };
                        await SampleStream.StartStreamAsync();
                    }
                    else
                    {
                        await e.Channel.SendMessage("Error Invalid input");
                    }
                });

            CService.CreateCommand("Track")
                .Description("Tracks a twitter User Either from their ID or Handle")
                .Parameter("User", ParameterType.Unparsed)
                .Description("Tracks a Twitter User")
                .Do(async (e) =>
                {
                    if (Regex.IsMatch(e.GetArg("User"), @"-?\d+(\.\d+)?"))
                    {
                        long User = Convert.ToInt64(e.GetArg("User"));
                        IUser Target = Tweetinvi.User.GetUserFromId(Convert.ToInt64(e.GetArg("User")));

                        await e.Channel.SendMessage($"Tracking { Tweetinvi.User.GetUserFromId(Convert.ToInt64(e.GetArg("User"))) }");

                        FilteredStream.AddFollow(User);
                        FilteredStream.MatchingTweetReceived += (sender, args) =>
                        {
                            e.Channel.SendMessage(Target + " " + args.Tweet);
                        };
                        FilteredStream.StreamStopped += (sender, args) =>
                        {
                            e.Channel.SendMessage("Filtered Stream Ended");
                        };
                        await FilteredStream.StartStreamMatchingAllConditionsAsync();
                    }
                    else
                    {
                        var Target = Tweetinvi.User.GetUserFromScreenName(e.GetArg("User"));

                        await e.Channel.SendMessage($"Tracking { Tweetinvi.User.GetUserFromScreenName(e.GetArg("User")) }");

                        FilteredStream.AddFollow(Target);
                        FilteredStream.MatchingTweetReceived += (sender, args) =>
                        {
                            Console.WriteLine("Found Tweet");
                            e.Channel.SendMessage($"{ e.GetArg("User") } Tweeted { args.Tweet }");
                        };
                        await FilteredStream.StartStreamMatchingAllConditionsAsync();
                    }
                });

        }
    }
}

