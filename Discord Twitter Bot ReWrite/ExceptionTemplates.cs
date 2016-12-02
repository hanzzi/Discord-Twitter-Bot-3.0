using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Exceptions;

namespace Discord_Twitter_Bot_ReWrite
{
    class ExceptionTemplates
    {
        public static void ArgumentException(ArgumentException ex)
        {
            Console.WriteLine("Something Went wrong");
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
            Console.WriteLine("TargetSite: " + ex.TargetSite);
            Console.WriteLine("Parameter: " + ex.ParamName);
            Console.WriteLine("Inner Exception: " + ex.InnerException);
            Console.WriteLine("HResult: " + ex.HResult);
            Console.WriteLine("HelpLink: " + ex.HelpLink);
        }

        public static void TwitterException(TwitterException ex)
        {
            Console.WriteLine("Something Went wrong");
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
            Console.WriteLine("TargetSite: " + ex.TargetSite);
            Console.WriteLine("Inner Exception: " + ex.InnerException);
            Console.WriteLine("HResult: " + ex.HResult);
            Console.WriteLine("HelpLink: " + ex.HelpLink);
            Console.WriteLine("Status: " + ex.Status);
            Console.WriteLine("Twitter Description: " + ex.TwitterDescription);
            Console.WriteLine("WebException: " + ex.WebException);
            Console.WriteLine("Status Code: " + ex.StatusCode);
        }

        public static void GenericException(Exception ex)
        {
            Console.WriteLine("Something Went wrong");
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
            Console.WriteLine("TargetSite: " + ex.TargetSite);
            Console.WriteLine("Inner Exception: " + ex.InnerException);
            Console.WriteLine("HResult: " + ex.HResult);
            Console.WriteLine("HelpLink: " + ex.HelpLink);
            Console.WriteLine("Data: " + ex.Data);
        }
    }
}
