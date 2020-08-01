using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Lansh.Common.Helper;
using Lansh.Common.Constant;
using Lansh.Constant;
using Lansh.Common.Service;
using Lansh.Common.Model;
using Lansh.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Runtime.InteropServices;
using Lansh.Service;
using Microsoft.Practices.ServiceLocation;
using Lansh.Common.Util;

namespace Lansh.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        #region Data Source
        public ObservableCollection<string> SuggestList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<HotKeyword> HotWordList { get; set; } = new ObservableCollection<Model.HotKeyword>();

        public ObservableCollection<SearchHistory> SearchHistoryList { get; set; } = new ObservableCollection<SearchHistory>();

        public List<ObservableCollection<SearchResult>> SearchResultList { get; set; } = new List<ObservableCollection<SearchResult>>();
        #endregion

        #region Command
        public ICommand LoadedCommand => new RelayCommand<Pivot>(pivot =>
       {
           SearchPivot = pivot;
           SearchPivot.SelectedIndex = 0;
           if (!string.IsNullOrEmpty(Keyword))
           {
               QuerySubmitted(Keyword);
           }
           HotKeyWord();
           SearchHistory();
       });

        public ICommand TextChangedCommand => new RelayCommand<string>(keyword =>
       {
           AutoSuggest(keyword);
       });

        public ICommand DeleteSearchHistoryCommand => new RelayCommand(( ) =>
        {
            SearchHistoryList.Clear();
            _dataService.DelectedSearchHistory();
        });

        public ICommand QuerySubmittedCommand => new RelayCommand<string>(keyword =>
       {
           QuerySubmitted(keyword);
       });

        public ICommand SearchPivotSelectionChangedCommand => new RelayCommand<Pivot>(pivot =>
       {
           SearchPivot = pivot;
           SelectedIndex = pivot.SelectedIndex;
           if (Page[SelectedIndex] == 0)
           {
               Search();
           }
           else
           {
               if (SearchResultList[SelectedIndex].Count == 0)
                   Messenger.Default.Send<string>(LogicConstant.NoSearchResult, "ResultMessage");
               else
                   Messenger.Default.Send<string>(null, "ResultMessage");
           }
       });

        public ICommand SearchVideoSelectionChangedCommand => new RelayCommand<int>((index) =>
        {
            if (index == -1)
                return;
            Video video = SearchResultList[SelectedIndex][index] as Video;
            Messenger.Default.Send<object>(video, "RootFrameNavigatedTo");
        });

        #endregion

        #region Service
        private ResultService _resultService = new ResultService();
        private DataService _dataService => ServiceLocator.Current.GetInstance<DataService>();
        #endregion

        #region Bind Page Attr
        public bool Busy { get; set; }
        #endregion

        #region Attr
        public string Keyword { get; set; }

        public string OldKeyword { get; set; }

        public int[] Page { get; set; } = new int[5];

        public Pivot SearchPivot { get; set; } = new Pivot();

        public int SelectedIndex { get; set; }
        #endregion

        #region SearchViewModel
        public SearchViewModel()
        {
            for (int i = 0; i < Page.Length; i++)
            {
                ObservableCollection<SearchResult> searchResultList = new ObservableCollection<SearchResult>();
                SearchResultList.Insert(i, searchResultList);
            }
        }
        #endregion

        #region Method
        private async void AutoSuggest(string keyword)
        {
            SuggestList.Clear();
            if (string.IsNullOrEmpty(keyword) || keyword == " ")
                return;

            List<String> suggestions = await _resultService.GetSuggestAsync(keyword);
             if (suggestions == null || suggestions.Count == 0)
            {
                SuggestList.Add(keyword);
            }
            else
            {
                int count = suggestions.Count;
                for (int i = 0; count >= LogicConstant.SuggestMaxLength && i < LogicConstant.SuggestMaxLength; i ++)
                {
                    SuggestList.Add(suggestions[i]);
                }
            }
        }

        private async void HotKeyWord()
        {
            HotWordList.Clear();
            List<HotKeyword> LocalHotWords = _dataService.FindHotKeyword();
            if (LocalHotWords != null)
                foreach (HotKeyword hotKeyword in LocalHotWords)
                    HotWordList.Add(hotKeyword);

            List<HotWord> hotWords = await _resultService.GetHotWordAsync();
            if (hotWords != null && hotWords.Count != 0)
            {
                List<HotKeyword> tempHotWords = new List<HotKeyword>();
                foreach (HotWord keyword in hotWords)
                {
                    //HotWordList.Add(new HotKeyword()
                    //{
                    //    Keyword = keyword.Keyword,
                    //    Status = keyword.Status
                    //});
                    tempHotWords.Add(new HotKeyword()
                    {
                        Keyword = keyword.Keyword,
                        Status = keyword.Status
                    });
                }
                if (!DataOperate.IsEqual<HotKeyword>(LocalHotWords, tempHotWords))
                {
                    HotWordList.Clear();
                    foreach (HotKeyword hotKeyword in tempHotWords)
                    {
                        HotWordList.Add(hotKeyword);
                    }
                    _dataService.DelectedHotKeyword();
                    _dataService.InsertAll(tempHotWords);
                }
            }
        }

        private void SearchHistory()
        {
            SearchHistoryList.Clear();
            var searchHistoryList = _dataService.FindSearchHistory();
            foreach (SearchHistory searchHistory in searchHistoryList)
            {
                SearchHistoryList.Add(searchHistory);
            }
        }

        public void QuerySubmitted(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return;
            Keyword = keyword;
            if (OldKeyword == Keyword)
                return;
            OldKeyword = Keyword;
            ResetToDefault();
            Search();
            _dataService.Insert<SearchHistory>(new SearchHistory()
            {
                Keyword = keyword,
                LastSearch = DateTime.Now
            });
            //SearchHistory();
        }

        public void ResetToDefault()
        {
            SearchPivot.SelectedIndex = 0;
            for (int i = 0; i < Page.Length; i ++)
            {
                SearchResultList[i].Clear();
                Page[i] = 0;
            }
        }

        public async void Search()
        {
            if (string.IsNullOrEmpty(Keyword))
                return;
            Busy = true;
            PivotItem selectedItem = SearchPivot.SelectedItem as PivotItem;
            string selectedType = selectedItem == null ? LogicConstant.DefaultSearchType : selectedItem.Tag.ToString();
            List<SearchResultInfo> SearchResult = null;
            try
            {
                Messenger.Default.Send<bool>(true, "ToggleBusy");
                SearchResult = await _resultService.GetSearchResultAsync<SearchResultInfo>(selectedType, Keyword, ++ Page[SelectedIndex]);
                Messenger.Default.Send<bool>(false, "ToggleBusy");
            }
            catch (COMException)
            {
                Messenger.Default.Send<object>(null, "Toast");
            }
            catch (Exception e)
            {
                Messenger.Default.Send<object>(e.ToString(), "Toast");
            }
            finally
            {
                Messenger.Default.Send<bool>(false, "ToggleBusy");
            }
            if (SearchResult != null && SearchResult.Count != 0)
            {
                Messenger.Default.Send<string>(null, "ResultMessage");
                foreach (SearchResultInfo searchResultInfo in SearchResult)
                {
                    SearchResultList[SelectedIndex].Add(Conversion(searchResultInfo));
                }
            }
            else
            {
                if (SearchResultList[SelectedIndex].Count == 0)
                    Messenger.Default.Send<string>(LogicConstant.NoSearchResult, "ResultMessage");
                else
                    Messenger.Default.Send<string>(null, "ResultMessage");
            }
        }

        /// <summary>
        /// Conversion to model
        /// </summary>
        /// <param name="searchResultInfo"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            switch (SelectedIndex)
            {
                case 0:
                    return new Video().Conversion(searchResultInfo);
                case 1:
                    return new Bangumi().Conversion(searchResultInfo);
                case 2:
                    return new Special().Conversion(searchResultInfo);
                case 3:
                    return new Upuser().Conversion(searchResultInfo);
                case 4:
                    return new Video().Conversion(searchResultInfo);
                default:
                    return null;
            }
        }

        public void LoadMore()
        {

        }
        #endregion

    }
}
