using Lansh.Model;
using Microsoft.Practices.ServiceLocation;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lansh.Service
{
    public class DataService
    {
        public DbContext dbContext => ServiceLocator.Current.GetInstance<DbContext>();

        public int Insert<T>(T t)
        {
            using (var conn = dbContext.GetDbConnection())
            {
                int count = 0;
                if (conn.Update(t) == 0)
                {
                    count = conn.Insert(t);
                }
                return count;
            }
        }

        public int InsertAll(IEnumerable i)
        {
            using (var conn = dbContext.GetDbConnection())
            {
                return conn.InsertAll(i);
            }
        }

        public List<HotKeyword> FindHotKeyword()
        {
            List<HotKeyword> searchHistoryList = new List<HotKeyword>();
            using (var conn = dbContext.GetDbConnection())
            {
                TableQuery<HotKeyword> hotKeywordTable = conn.Table<HotKeyword>();
                foreach (HotKeyword hotKeyword in hotKeywordTable)
                {
                    searchHistoryList.Add(hotKeyword);
                }
                return searchHistoryList;
            }
        }

        public List<SearchHistory> FindSearchHistory()
        {
            List<SearchHistory> searchHistoryList = new List<SearchHistory>();
            using (var conn = dbContext.GetDbConnection())
            {
                TableQuery<SearchHistory> searchHistoryTable = conn.Table<SearchHistory>().OrderByDescending(s => s.LastSearch).Take(10);
                foreach (SearchHistory searchHistory in searchHistoryTable)
                {
                    searchHistoryList.Add(searchHistory);
                }
                return searchHistoryList;
            }
        }

        public int DelectedHotKeyword()
        {
            using (var conn = dbContext.GetDbConnection())
            {
                return conn.DeleteAll<HotKeyword>();
            }
        }

        public int DelectedSearchHistory()
        {
            using (var conn = dbContext.GetDbConnection())
            {
                return conn.DeleteAll<SearchHistory>();
            }
        }

    }
}
