using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterApp.Helpers
{
    public interface IRSSItemHelper
    {
        RSSFeedModel.RSSFeedItem ConvertRawToModel(RSSRawFeed.Item item);
    }
}
