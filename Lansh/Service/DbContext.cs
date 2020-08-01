using Lansh.Model;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Lansh.Service
{
    public class DbContext
    {
        private static string dbPath = string.Empty;
        public static string DbPath
        {
            get
            {
                if (string.IsNullOrEmpty(dbPath))
                {
                    dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Lansh.db");
                }
                return dbPath;
            }
        }

        public DbContext()
        {
            Init();
        }

        public void Init()
        {
            using (var db = GetDbConnection())
            {
                db.CreateTable<SearchHistory>();
                db.CreateTable<HotKeyword>();
            }
        }

        public SQLiteConnection GetDbConnection()
        {
            return new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
        }
    }
}
