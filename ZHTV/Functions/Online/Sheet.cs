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
        static SpreadsheetsResource.ValuesResource.GetRequest request;
        static ValueRange response;
        static IList<IList<object>> values;

        private static SheetsService Service()
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

            return service;
        }

        private static void Song(string sheetId)
        {
            request = Service().Spreadsheets.Values.Get(sheetId, "Vietnamese!A2:F");
            response = request.Execute();
            values = response.Values;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Manage.SongList.Add(new Song
                    {
                        ID = Convert.ToInt32(row[0]),
                        Name = row[1].ToString(),
                        Artist = row[2].ToString(),
                        Duration = Convert.ToDouble(row[3]),
                        PlayerUri = row[4].ToString(),
                        AlbumUri = row[5].ToString()
                    });
                }
            }
        }

        private static void Theme(string sheetId)
        {
            request = Service().Spreadsheets.Values.Get(sheetId, "Theme!A2:B");
            response = request.Execute();
            values = response.Values;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    
                }
            }
        }

        private static void Info(string sheetId)
        {
            request = Service().Spreadsheets.Values.Get(sheetId, "Info!A2:B");
            response = request.Execute();
            values = response.Values;

            if (values != null && values.Count > 0) foreach (var row in values) Display.InfoList.Add(row[1].ToString());
        }

        public static void Bind(MainWindowElement element)
        {
            Song(element.SheetId);
            Info(element.SheetId);
            //Theme(element.SheetId);
        }
    }
}
    