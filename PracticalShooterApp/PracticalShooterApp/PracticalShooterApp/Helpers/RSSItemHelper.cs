using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static PracticalShooterApp.Models.RSSFeedModel;

namespace PracticalShooterApp.Helpers
{
    public class RSSItemHelper : IRSSItemHelper
    {
		private Regex _rssHtmlImageDescriptionRegex = new Regex(@"<img.*(?<imageUrl>((?<=src="").*?(?=""))).*\/>(?<description>.*(?=<\/p>))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private Regex _rssHtmlDescriptionOnlyRegex = new Regex(@"(?<=<p>)(?<description>.*?)(?=(<a )|(<\/p>))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public RSSFeedModel.RSSFeedItem ConvertRawToModel(RSSRawFeed.Item item)
        {
			var pubDate = (System.Xml.XmlNode[])item.PubDate;

			var itemModel = new RSSFeedItem()
			{
				Title = item.Title,
				Categories = item.Category,
				PostLink = item.Guid.Text,
				PublishDate = DateTime.Parse(pubDate[0].Value),
				LinkReference = item.Link
			};

			var htmlImageDescription = _rssHtmlImageDescriptionRegex.Match(item.Description);

			if (htmlImageDescription.Success)
            {
				itemModel.ImageUrl = htmlImageDescription.Groups["imageUrl"].Value ?? "";
				itemModel.HasImage = !string.IsNullOrWhiteSpace(itemModel.ImageUrl);
				itemModel.Description = string.IsNullOrWhiteSpace(htmlImageDescription.Groups["description"].Value)
					? string.IsNullOrWhiteSpace(item.Description)
						? "No Description Available"
						: item.Description
					: htmlImageDescription.Groups["description"].Value;
			}
			else
            {
				var htmlDescription = _rssHtmlDescriptionOnlyRegex.Match(item.Description);

				itemModel.HasImage = false;
				itemModel.Description = string.IsNullOrWhiteSpace(htmlDescription.Groups["description"].Value)
					? string.IsNullOrWhiteSpace(item.Description)
						? "No Description Available"
						: item.Description
					: htmlDescription.Groups["description"].Value;
            }				

			return itemModel;
		}
    }
}
