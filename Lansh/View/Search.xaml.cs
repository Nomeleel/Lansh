using GalaSoft.MvvmLight.Messaging;
using Lansh.Model;
using Lansh.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Lansh.View
{
    public sealed partial class Search : Page
    {
        public SearchViewModel ViewModel = new SearchViewModel();

        public Search()
        {
            this.InitializeComponent();
            RegisterEvent();
            RegisterMessenger();
        }

        #region Event

        /// <summary>
        /// Register all event
        /// </summary>
        private void RegisterEvent()
        {
            Loaded += Search_Loaded;
            BackRequested.Click += BackRequested_Click; 
            HotKeywordGV.ItemClick += HotKeywordGV_ItemClick;
            SearchHistoryLV.ItemClick += SearchHistoryLV_ItemClick;
            SearchBox.TextChanged += SearchBox_TextChanged;
            SearchBox.QuerySubmitted += SearchBox_QuerySubmitted;
            SearchVideoSV.ViewChanged += SearchScrollViewer_ViewChanged;
            SearchBangumiSV.ViewChanged += SearchScrollViewer_ViewChanged;
            SearchSpecialSV.ViewChanged += SearchScrollViewer_ViewChanged;
            SearchUpuserSV.ViewChanged += SearchScrollViewer_ViewChanged;
        }

        private void Search_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BackRequested_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<object>(null, "BackRequested");
        }

        private void HotKeywordGV_ItemClick(object sender, ItemClickEventArgs e)
        {
            string hotWord = (e.ClickedItem as HotKeyword).Keyword;
            SearchBox.Text = hotWord;
            ViewModel.QuerySubmitted(hotWord);
        }

        private void SearchHistoryLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            string history = (e.ClickedItem as SearchHistory).Keyword;
            SearchBox.Text = history;
            ViewModel.QuerySubmitted(history);
            // TO List First
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Normol input
             if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && string.IsNullOrEmpty(sender.Text))
            {
                SearchResult.Visibility = Visibility.Collapsed;
                SearchRecommendSV.Visibility = Visibility.Visible;
            }
            // For hot search
            if (args.Reason == AutoSuggestionBoxTextChangeReason.ProgrammaticChange && !string.IsNullOrEmpty(sender.Text))
            {
                SearchRecommendSV.Visibility = Visibility.Collapsed;
                SearchResult.Visibility = Visibility.Visible;
            }
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!string.IsNullOrEmpty(sender.Text))
            {
                SearchRecommendSV.Visibility = Visibility.Collapsed;
                SearchResult.Visibility = Visibility.Visible;
                Unfocused.Focus(FocusState.Pointer);
            }
        }
        
        /// <summary>
        /// Load more
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                ViewModel.Search();
            }
        }

        #endregion

        #region Messenger

        /// <summary>
        /// Register messenger
        /// </summary>
        private void RegisterMessenger()
        {
            Messenger.Default.Register<string>(this, "ResultMessage", ResultMessage);
        }

        #region ResultMessage
        private void ResultMessage(string message)
        {
            if (message == null)
            {
                Message.Visibility = Visibility.Collapsed;
            }
            else
            {
                Message.Visibility = Visibility.Visible;
                Message.Text = message.ToString();
            }
        }
        #endregion

        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string key = e.Parameter as string;
            if (!string.IsNullOrEmpty(key))
            {
                SearchBox.Text = key;
                ViewModel.Keyword = key;
            }
        }

    }
}
