﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ZHTV.Models
{
    class Sheet
    {
        static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static readonly string ApplicationName = "Google SpeadSheet";
        static readonly Dictionary<int, Song> Song = new Dictionary<int, Song>();
        static readonly Dictionary<string, Order> Order = new Dictionary<string, Order>();
        static IList<IList<object>> values;

        //public static void AccessSongSheet()
        //{
        //    UserCredential credential;
        //    using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        //    {
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user",
        //            CancellationToken.None, new FileDataStore(credPath, true)).Result;
        //    }

        //    var service = new SheetsService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });

        //    SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get("1ICOivODkrc4A86I1JVEQ0sVEa-8XriKoG5O4116xiKo", "A2:C");
        //    ValueRange response = request.Execute();
        //    IList<IList<object>> values = response.Values;
        //    if (values != null && values.Count > 0)
        //    {
        //        foreach (var row in values)
        //        {
        //            if (!Song.ContainsKey(Convert.ToInt32(row[0])))
        //                Song.Add(Convert.ToInt32(row[0]), new Song { ID = Convert.ToInt32(row[0]), Name = row[1].ToString(), Artist = row[2].ToString() });
        //        }
        //    }
        //}

        //public static Dictionary<int, Song> SongDict()
        //{
        //    return Song;
        //}

        //public static Dictionary<string, Order> OrderList()
        //{
        //    UserCredential credential;
        //    using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        //    {
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user",
        //            CancellationToken.None, new FileDataStore(credPath, true)).Result;
        //    }

        //    var service = new SheetsService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });

        //    SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get("1oycbgNJEVF9cqLE8l701YZyyk4fkRT5P9XbMov4UiAI", "12/2020!A3:H");
        //    //try
        //    {
        //        ValueRange response = request.Execute();
        //        IList<IList<object>> values = response.Values;
                
        //    }
        //    //catch (Exception) { MessageBox.Show("Không có kết nối. Vui lòng kiểm tra kết nối mạng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    return Order;
        //}

        public static IList<IList<object>> GetData(string id, string range)
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
                ApplicationName = ApplicationName,
            });

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(id, range);
            ValueRange response = request.Execute();
            values = response.Values;
            return values;
        }

        public static Dictionary<int, Song> SongDict(IList<IList<object>> values)
        {
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (!Song.ContainsKey(Convert.ToInt32(row[0])))
                        Song.Add(Convert.ToInt32(row[0]), new Song { ID = Convert.ToInt32(row[0]), Name = row[1].ToString(), Artist = row[2].ToString() });
                }
            }
            return Song;;
        }

        public static Dictionary<string, Order> OrderList(IList<IList<object>> values)
        {
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (!Order.ContainsKey(row[1].ToString()))
                        Order.Add(row[1].ToString(), new Order() { UserID = row[2].ToString(), SongID = Convert.ToInt32(row[4]) });
                }
            }
            return Order;
        }
    }
}
    