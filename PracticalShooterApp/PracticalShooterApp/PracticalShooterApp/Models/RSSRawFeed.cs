using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static PracticalShooterApp.Models.RSSFeedModel;

namespace PracticalShooterApp.Models
{
	[XmlRoot(ElementName = "rss")]
	public class RSSRawFeed
	{
		[XmlRoot(ElementName = "link")]
		public class Link
		{

			[XmlAttribute(AttributeName = "href")]
			public string Href { get; set; }

			[XmlAttribute(AttributeName = "rel")]
			public string Rel { get; set; }

			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }
		}

		[XmlRoot(ElementName = "image")]
		public class Image
		{

			[XmlElement(ElementName = "url")]
			public string Url { get; set; }

			[XmlElement(ElementName = "title")]
			public string Title { get; set; }

			[XmlElement(ElementName = "link")]
			public string Link { get; set; }

			[XmlElement(ElementName = "width")]
			public int Width { get; set; }

			[XmlElement(ElementName = "height")]
			public int Height { get; set; }
		}

		[XmlRoot(ElementName = "guid")]
		public class Guid
		{

			[XmlAttribute(AttributeName = "isPermaLink")]
			public bool IsPermaLink { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "item")]
		public class Item
		{

			[XmlElement(ElementName = "category")]
			public List<string> Category { get; set; }

			[XmlElement(ElementName = "guid")]
			public Guid Guid { get; set; }

			[XmlElement(ElementName = "description")]
			public string Description { get; set; }

			[XmlElement(ElementName = "title")]
			public string Title { get; set; }

			[XmlElement(ElementName = "link")]
			public string Link { get; set; }

			[XmlElement(ElementName = "creator")]
			public string Creator { get; set; }

			[XmlElement(ElementName = "pubDate")]
			public object PubDate { get; set; }
		}

		[XmlRoot(ElementName = "channel")]
		public class RSSChannel
		{

			[XmlElement(ElementName = "title")]
			public string Title { get; set; }

			[XmlElement(ElementName = "link")]
			public List<Link> Link { get; set; }

			[XmlElement(ElementName = "description")]
			public string Description { get; set; }

			[XmlElement(ElementName = "lastBuildDate")]
			public object LastBuildDate { get; set; }

			[XmlElement(ElementName = "language")]
			public string Language { get; set; }

			[XmlElement(ElementName = "updatePeriod")]
			public string UpdatePeriod { get; set; }

			[XmlElement(ElementName = "updateFrequency")]
			public int UpdateFrequency { get; set; }

			[XmlElement(ElementName = "generator")]
			public string Generator { get; set; }

			[XmlElement(ElementName = "image")]
			public Image Image { get; set; }

			[XmlElement(ElementName = "item")]
			public List<Item> Item { get; set; }
		}



		[XmlElement(ElementName = "channel")]
		public RSSChannel Channel { get; set; }

		[XmlAttribute(AttributeName = "version")]
		public double Version { get; set; }

		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		[XmlAttribute(AttributeName = "wfw")]
		public string Wfw { get; set; }

		[XmlAttribute(AttributeName = "dc")]
		public string Dc { get; set; }

		[XmlAttribute(AttributeName = "atom")]
		public string Atom { get; set; }

		[XmlAttribute(AttributeName = "sy")]
		public string Sy { get; set; }

		[XmlAttribute(AttributeName = "slash")]
		public string Slash { get; set; }

		[XmlText]
		public string Text { get; set; }

		public RSSFeedModel ConvertToModel()
        {
			var model = new RSSFeedModel()
            {
				Title = this.Channel.Title,
				Description = this.Channel.Description,
				ImageUrl = this.Channel.Image.Url,
				Items = new List<RSSFeedModel.RSSFeedItem>()
            };

            foreach (var item in this.Channel.Item)
            {

				var pubDate = (System.Xml.XmlNode[])item.PubDate;

				var itemModel = new RSSFeedItem()
				{
					Title = item.Title,
					Description = item.Description,
					Categories = item.Category,
					PostLink = item.Guid.Text,
					PublishDate = DateTime.Parse(pubDate[0].Value),
					LinkReference = item.Link
				};

				model.Items.Add(itemModel);
            }	

			return model;
        }
	}
}
