using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace LearnRssForYammer
{
    class Program
    {
        static void Main(string[] args)
        {
            string feedurl = "https://mslearn-contentfeed.azurewebsites.net/";
            XmlReader reader = XmlReader.Create(feedurl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);


            string html = "<b>Latest MS Learn updates in the past 7 days:</b><br><br>";
            foreach (SyndicationItem item in feed.Items)
            {
                if ((DateTime.Now - item.PublishDate).TotalDays < 8)
                {
                    html = html + "<a href=\"" + item.Links[0].Uri + "\">" 
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
