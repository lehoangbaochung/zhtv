using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using ZHTV.Models.Objects;
using System.Collections.Generic;

namespace ZHTV.Functions.Online
{
    class Youtube
    {
        static readonly Dictionary<string, Order> OrderDictionary = new Dictionary<string, Order>();
        static string liveChatId = null;
        static int count = 0;

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

            if (liveChatId == null)
            {
                var videoListRequest = youtubeService.Videos.List("liveStreamingDetails");
                videoListRequest.Id = videoId;

                var videoListResponse = videoListRequest.Execute();
                liveChatId = videoListResponse.Items[0].LiveStreamingDetails.ActiveLiveChatId;
            }

            var liveChatMessageListRequest = youtubeService.LiveChatMessages.List(liveChatId, "snippet,authorDetails");
            var liveChatMessageListResponse = liveChatMessageListRequest.Execute();

            foreach (var item in liveChatMessageListResponse.Items)
            {
                if (int.TryParse(TextTrimming(item.Snippet.DisplayMessage), out int id) && !OrderDictionary.ContainsKey(item.Id) && Sheet.SongDictionary.ContainsKey(id))
                {
                    OrderDictionary.Add(item.Id, new Order()
                    {
                        UserID = item.AuthorDetails.ChannelId,
                        UserName = item.AuthorDetails.DisplayName,
                        SongID = id
                    });
                }
            }

            if (OrderDictionary.Count > count)
            {
                for (int i = count; i < OrderDictionary.Count; i++)
                {
                    Manage.OrderSong(OrderDictionary.ElementAt(i).Value);
                }

                count = OrderDictionary.Count;
            }
        }

        private string ConvertToUnSign(string text)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = text.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        private string TextTrimming(string text)
        {
            var syntaxZM = new Regex("^ZM [1-9][0-9]*$");
            var syntaxZMT = new Regex("^ZMT .+$");
            string substr = null;

            if (syntaxZM.IsMatch(text))
            {
                substr = text.Substring(3);
            }
            else if (syntaxZMT.IsMatch(text))
            {
                foreach (var item in Sheet.SongDictionary.Values)
                {
                    if (string.Compare(item.Name, text.Substring(4), true) == 0)
                        substr = item.ID.ToString();
                    else if ((item.Name + " " + item.Artist).ToLower().Contains(text.Substring(4).ToLower()))
                        substr = item.ID.ToString();
                }
            } 
            
            return substr;
        }   
    }
}
