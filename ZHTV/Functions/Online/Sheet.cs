using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ZHTV.Models.Objects;
using ZHTV.Models.Windows;

namespace ZHTV.Functions.Online
{
    class Sheet
    {
        static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static readonly string ApplicationName = "Google SpeadSheet";

        public static IList<IList<object>> GetValue(MainWindowElement element)
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user",
                    CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            SpreadsheetsResource.ValuesResource.GetRequest request;

            if (element.SheetTab == null) request = service.Spreadsheets.Values.Get(element.SheetId, element.SheetRange);
            else request = service.Spreadsheets.Values.Get(element.SheetId, element.SheetTab + "!" + element.SheetRange);

            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;

            return values;
        }

        private static void Song(IList<IList<object>> values)
        {
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Manage.Songlist.Add(new Song 
                    { 
                        ID = Convert.ToInt32(row[0]), 
                        Name = row[1].ToString(), 
                        Artist = row[2].ToString(),
                        AlbumUri = row[3].ToString(),
                        ArtistUri = row[4].ToString()
                    });
                }
            }
        }

        private static void Theme(IList<IList<object>> values)
        {
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Display.ThemeList.Add(new Theme
                    {
                        Name = row[0].ToString(),
                        OrderCount = row[2].ToString(),
                        Song = row[3].ToString(),
                        Playlist = row[4].ToString()
                    });
                }
            }
        }

        private static void Info(IList<IList<object>> values)
        {
            if (values != null && values.Count > 0) foreach (var row in values) Display.InfoList.Add(row[1].ToString());
        }

        public static void Bind(MainWindowElement element)
        {
            Song(GetValue(element));
            Info(GetValue(element));
            Theme(GetValue(element));
        }
    }
}
    