using System;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Mvc.Facebook;
using Newtonsoft.Json;
using System.Collections.Generic;

// Add any fields you want to be saved for each user and specify the field name in the JSON coming back from Facebook
// http://go.microsoft.com/fwlink/?LinkId=301877

namespace FBHelpMe.Models
{
    public partial class MyAppUserExt
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonProperty("picture")] // This renames the property to picture.
        [FacebookFieldModifier("type(large)")] // This sets the picture size to large.
        public FacebookConnection<FacebookPicture> ProfilePicture { get; set; }

        [FacebookFieldModifier("limit(100000)")] // This sets the size of the friend list to 100000, remove it to get all friends.
        public FacebookGroupConnection<MyAppUserFriend> Friends { get; set; }

        [FacebookFieldModifier("limit(150000)")] // This sets the size of the photo list to 150000, remove it to get all photos.
        public FacebookGroupConnection<FacebookPhoto> Photos { get; set; }

        public List<MyFriendsProfessions> Professions { get; set; }

        public string ChartData { get; set; }
    }
}
