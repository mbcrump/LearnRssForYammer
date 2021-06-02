using System;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Configuration;

namespace LearnRssForYammer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Configure "Feed RSS URL/Days back" in App.config
            string feedurl = ConfigurationManager.AppSettings.Get("feedurl");
            string days = ConfigurationManager.AppSettings.Get("days");
            if (feedurl == null || days == null)
            {
                Console.WriteLine("Check your App.config for null values");
                Console.WriteLine("Using default values");
                feedurl = "https://mslearn-contentfeed.azurewebsites.net/";
                days = "8";
            }

            XmlReader reader = XmlReader.Create(feedurl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);


            string html = "<b>Latest MS Learn updates in the past " + days + " days:</b><br><br>";
            foreach (SyndicationItem item in feed.Items)
            {
                if ((DateTime.Now - item.PublishDate).TotalDays < int.Parse(days))
                {
                    html = html + "<a href=\"" 
                        + item.Links[0].Uri + "\">" 
                        + item.Title.Text + "</a><br>" 
                        + item.Summary.Text + "<br><br><br>";
                }
            }

            reader.Close();

            Console.WriteLine(html);
            Console.Read();

        }
    }
}
