using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ZHTV.Models
{
    class Youtube
    {
        public static readonly Dictionary<string, Order> MessageList = new Dictionary<string, Order>();

        public async Task Run(string videoId)
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube }, "user", CancellationToken.None, new FileDataStore(GetType().ToString()));
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });

            var videoListRequest = youtubeService.Videos.List("liveStreamingDetails");
            videoListRequest.Id = videoId;
            var videoListResponse = videoListRequest.Execute();
            var liveChatMessageListRequest = youtubeService.LiveChatMessages.List(videoListResponse.Items[0].LiveStreamingDetails.ActiveLiveChatId, "snippet,authorDetails");
            var liveChatMessageListResponse = liveChatMessageListRequest.Execute();
            foreach (var item in liveChatMessageListResponse.Items)
            {
                if (int.TryParse(TextTrimming(item.Snippet.DisplayMessage), out int id) && !MessageList.ContainsKey(item.Id))
                {
                    MessageList.Add(item.Id, new Order()
                    {
                        UserID = item.AuthorDetails.ChannelId,
                        UserName = item.AuthorDetails.DisplayName,
                        SongID = id
                    });
                }
            }
        }
        
        private string TextTrimming(string text)
        {
            var regexItem = new Regex("^ZM [1-9][0-9]*$");
            string substr = null;

            if (regexItem.IsMatch(text) && Manage.SongDict.ContainsKey(Convert.ToInt32(text.Substring(3)))) // đảm bảo đúng cú pháp vote và tồn tại id trong list
            {
                substr = text.Substring(3);
            }
            return substr;
        }   
    }
}
