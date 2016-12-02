using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using Tweetinvi.Exceptions;

namespace Discord_Twitter_Bot_ReWrite
{
    class Login
    {
        public static void Navigate()
        {
            var th = new Thread(() =>
            {
                WebBrowser webBrowser1 = new WebBrowser();
                webBrowser1.ScriptErrorsSuppressed = true;

                string YesNo = ConfigInfo.YesNo;

                if (YesNo == "True" | YesNo == "true")
                {
                    webBrowser1.DocumentCompleted += ManualDocumentCompleted; //new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);

                    webBrowser1.Navigate(Authorization.GetAuthUrl());
                }
                else if (YesNo == "False" | YesNo == "false")
                {
                    webBrowser1.DocumentCompleted += AutoDocumentCompleted;

                    webBrowser1.Navigate(Authorization.GetAuthUrl());
                }
                else
                {
                    Console.WriteLine("Something went wrong please make sure that you have specified which method of authentication you wish to use in the config");
                }
                Application.Run();

            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private static void ManualDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                var webBrowser1 = sender as WebBrowser;

                Console.WriteLine("Please enter your Twitter Username");
                var username = webBrowser1.Document.GetElementById("username_or_email");
                username?.SetAttribute("value", Console.ReadLine());

                Console.WriteLine("Please enter your Twitter Password");
                var password = webBrowser1.Document.GetElementById(@"session[password]");
                password?.SetAttribute("value", Console.ReadLine());
                Console.Clear();

                var validate = webBrowser1.Document.GetElementById("allow");
                validate?.InvokeMember("click");


                string URL = webBrowser1.Document.Url.ToString();
                Convert.ToString(URL);

                if (URL == "https://api.twitter.com/oauth/authorize")
                {

                    string AuthPin = webBrowser1.Document.GetElementById("oauth_pin").InnerText;
                    if (AuthPin != null)
                    {
                        bool isNumber = Regex.IsMatch(AuthPin, @"-?\d+(\.\d+)?");

                        if (isNumber == true)
                        {
                            Authorization.Authorize(AuthPin);
                            webBrowser1.Dispose();
                        }
                    }
                }
            } catch (ArgumentException ex)
            {
                ExceptionTemplates.ArgumentException(ex);
            } catch (TwitterException ex)
            {
                ExceptionTemplates.TwitterException(ex);
            } catch (Exception ex)
            {
                ExceptionTemplates.GenericException(ex);
            }
        }

        private static void AutoDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string Username = ConfigInfo.Username;
                string Password = ConfigInfo.Password;

                var webBrowser1 = sender as WebBrowser;

                var username = webBrowser1.Document.GetElementById("username_or_email");
                username?.SetAttribute("value", Username);

                var password = webBrowser1.Document.GetElementById(@"session[password]");
                password?.SetAttribute("value", Password);

                var validate = webBrowser1.Document.GetElementById("allow");
                validate?.InvokeMember("click");


                string URL = webBrowser1.Document.Url.ToString();
                Convert.ToString(URL);
                if (URL == "https://api.twitter.com/oauth/authorize")
                {
                    try
                    {
                        string AuthPin = webBrowser1.Document.GetElementById("oauth_pin").InnerText;
                        if (AuthPin != null)
                        {
                            bool isNumber = Regex.IsMatch(AuthPin, @"-?\d+(\.\d+)?");

                            if (isNumber == true)
                            {

                                Authorization.Authorize(AuthPin);
                                Start.CreateCommands();
                                webBrowser1.Dispose();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionTemplates.GenericException(ex);
                    }
                }
            } catch (ArgumentException ex)
            {
                ExceptionTemplates.ArgumentException(ex);
            } catch (TwitterException ex)
            {
                ExceptionTemplates.TwitterException(ex);
            } catch (Exception ex)
            {
                ExceptionTemplates.GenericException(ex);
            }
        }
    }
}
