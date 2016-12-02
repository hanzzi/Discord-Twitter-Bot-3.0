using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;

namespace Discord_Twitter_Bot_ReWrite
{
    class Authorization
    {
        private static IAuthenticationContext AuthenticationContext { get; set; }
        private static string AuthUrl { get; set; }

        public static string GetAuthUrl()
        {
            

            try
            {
                string ConsumerKey = ConfigInfo.ConsumerKey;
                string ConsumerSecret = ConfigInfo.ConsumerSecret;
                string AccessToken = ConfigInfo.AccessToken;
                string AccessSecret = ConfigInfo.AccessSecret;

                // Credentials for Twitter Authorization gotten by making application in apps.twitter.com
                ITwitterCredentials AppCredentials = new TwitterCredentials(ConsumerKey, ConsumerSecret, AccessToken, AccessSecret);

                // Stores twitter oauth Url and recently created tokens 
                var authenticationContext = AuthFlow.InitAuthentication(AppCredentials);

                // Generates string from authenticationContext  
                AuthUrl = authenticationContext.AuthorizationURL;

                AuthenticationContext = authenticationContext;
            }
            catch (ArgumentException ex)
            {
                ExceptionTemplates.ArgumentException(ex);
            }
            catch (TwitterException ex)
            {
                ExceptionTemplates.TwitterException(ex);
            } catch (Exception ex)
            {
                ExceptionTemplates.GenericException(ex);
            }

            return AuthUrl;
        }

        public static void Authorize(string AuthCode)
        {
            try
            {
                ITwitterCredentials UserCredentials = AuthFlow.CreateCredentialsFromVerifierCode(AuthCode, AuthenticationContext);
                Auth.SetCredentials(UserCredentials);
                Console.WriteLine("Connected to Twitter");
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
