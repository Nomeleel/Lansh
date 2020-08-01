using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Lansh.Extend;
using Windows.UI.Xaml.Controls;
using System;
using Lansh.Common.Util;
using Lansh.Model;
using GalaSoft.MvvmLight.Messaging;
using Lansh.ViewModel;
using AppStudio.Uwp;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Lansh.View
{

    /// <summary>
    /// Main Page
    /// </summary>
    public sealed partial class MainPage
    {
        private string key = null;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            RegisterEvent();
            RegisterMessenger();
        }

        #region Event

        /// <summary>
        /// Register all event
        /// </summary>
        private void RegisterEvent()
        {
            Loaded += MainPage_Loaded;
            ToggleMenuButton.Click += ToggleMenuButton_Click;
            NavMenuList.ItemClick += NavMenuList_ItemClick;
            NavMenuList.SelectionChanged += NavMenuList_SelectionChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        /// <summary>
        /// Main Page Load : Set NavList select 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(key))
            {
                ShellFrame.Navigate(typeof(Search), key);
                NavMenuList.SelectedIndex = 1;
                key = null;
            }
            else
                if (!ShellFrame.CanGoBack)
                NavMenuList.SelectedIndex = 0;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ShellFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        /// <summary>
        /// Switch SplitView OpenPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        /// <summary>
        /// Click Nav Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.MainSplitView.IsPaneOpen && DeviceFamily.IsMobile())
            {
                this.MainSplitView.IsPaneOpen = false;
            }
        }

        /// <summary>
        /// Change NavIten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavMenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavMenuItem selectItem = (sender as ListView).SelectedItem as NavMenuItem;
            if (selectItem != null)
            {
                if (selectItem.DestinationPage != null && selectItem.DestinationPage != ShellFrame.CurrentSourcePageType)
                {
                    ShellFrame.Navigate(selectItem.DestinationPage, selectItem.Arguments);
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                }
            }
        }

        /// <summary>
        /// TitltBar Back Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            BackRequested(null);
        }

        #endregion

        #region Messenger

        /// <summary>
        /// Register messenger
        /// </summary>
        private void RegisterMessenger()
        {
            Messenger.Default.Register<object>(this, "Toast", ToastMessenger);
            Messenger.Default.Register<object>(this, "ToggleMenu", ToggleMenu);
            Messenger.Default.Register<int>(this, "MainPageNavigatedTo", MainPageNavigatedTo);
            Messenger.Default.Register<bool>(this, "ToggleBusy", ToggleBusy);
            Messenger.Default.Register<object>(this, "BackRequested", BackRequested);
            Messenger.Default.Register<object>(this, "RootFrameNavigatedTo", RootFrameNavigatedTo);
        }

        #region Toast
        private async void ToastMessenger(object o)
        {
            Toast.Visibility = Visibility.Visible;
            await Toast.AnimateDoublePropertyAsync("Opacity", 0, 1.0, 500);
            await Task.Delay(1000);
            await Toast.AnimateDoublePropertyAsync("Opacity", 1.0, 0, 500);
            Toast.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region ToggleMenu
        private void ToggleMenu(object o)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }
        #endregion

        #region MainPageNavigatedTo
        private void MainPageNavigatedTo(int index)
        {
            NavMenuList.SelectedIndex = index;
        }
        #endregion

        #region ToggleBusy
        private void ToggleBusy(bool IsActive)
        {
            Busy.IsActive = IsActive;
        }
        #endregion

        #region BackRequested //title bar navigated
        private void BackRequested(object o)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            if (navigationService.CurrentPageKey == "Play")
            {
                navigationService.GoBack();
            }
            else
            {
                if (ShellFrame.CanGoBack)
                {
                    ShellFrame.GoBack();
                    NavMenuItem currentSelectedItem = (DataContext as MainPageViewModel).NavList.Find(i => i.DestinationPage == ShellFrame.CurrentSourcePageType);
                    if (currentSelectedItem != null)
                        this.NavMenuList.SelectedItem = currentSelectedItem;
                    if (!ShellFrame.CanGoBack)
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }
        }
        #endregion

        #region RootFrameNavigatedTo
        private void RootFrameNavigatedTo(object Param)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.NavigateTo("Play", Param);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }
        #endregion

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            key = e.Parameter as string;
        }

    }
}
