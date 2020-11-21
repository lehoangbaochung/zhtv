using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Collections;

namespace ZHTV
{
    class Youtube
    {
        public async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.YoutubeReadonly }, "user", CancellationToken.None, new FileDataStore(this.GetType().ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            LiveChatMessageSnippet mySnippet = new LiveChatMessageSnippet();
            LiveChatMessage comments = new LiveChatMessage();
            LiveChatTextMessageDetails txtDetails = new LiveChatTextMessageDetails();
            txtDetails.MessageText = "yay";
            mySnippet.TextMessageDetails = txtDetails;
            mySnippet.LiveChatId = "RQyOCpy91x0";
            mySnippet.Type = "textMessageEvent";
            comments.Snippet = mySnippet;
            comments = await youtubeService.LiveChatMessages.Insert(comments, "snippet").ExecuteAsync();

            Google.Apis.Util.Repeatable<string> st = "snipset";
            LiveChatMessagesResource.ListRequest request = youtubeService.LiveChatMessages.List("id", "snippet,authorDetail");
            LiveChatMessageListResponse response = request.Execute();
            if (response.Items != null && response.Items.Count > 0)
            {
                foreach (var row in response.Items)
                {
                    //dgv_data.Rows.Add(row[0], row[1], row[2]);
                }
            }
            else
            {
                LiveBroadcastSnippet sp = new LiveBroadcastSnippet();
                LiveBroadcastListResponse rp = new LiveBroadcastListResponse();
                string id = sp.LiveChatId;
                //
                string list = youtubeService.LiveBroadcasts.List("snippet").Id.ToString();
            }

        }
    }
}
