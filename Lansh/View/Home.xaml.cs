using AppStudio.Uwp.Controls;
using Lansh.ViewModel;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using Lansh.Model;
using Windows.Media.Core;
using GalaSoft.MvvmLight.Messaging;

namespace Lansh.View
{

    public sealed partial class Home : Page
    {
        public HomeViewModel ViewModel = new HomeViewModel();

        public Home()
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
            this.Loaded += HomePage_Loaded;
            SearchIcon.Click += SearchIcon_Click;
            HamburgButton.Click += ToggleMenu_Click;
            HomePivot.SelectionChanged += HomePivot_SelectionChanged;
            MvGVI.Tapped += MvGVI_Tapped;
            OneGVI.Tapped += OneGVI_Tapped;
            RecommendGV.ItemClick += HomeGV_ItemClick;
            MusicGV.ItemClick += HomeGV_ItemClick;
            TechnologyGV.ItemClick += HomeGV_ItemClick;
            LifeGV.ItemClick += HomeGV_ItemClick;
            AdvertisingGV.ItemClick += HomeGV_ItemClick;
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            MvLoopingList.ItemsSource = await ViewModel.Mv();
        }

        /// <summary>
        /// Navigated to Search page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchIcon_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<int>(1, "MainPageNavigatedTo");
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<object>(null, "ToggleMenu");
        }

        private void HomePivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Pivot pivot = sender as Pivot;
            //var v = (pivot.SelectedItem as PivotItem).Tag;
            //string tag = v == null ? "empty" : v.ToString();
            //if (tag == "one")
            //{
            //    await ViewModel.GetOneList();
            //}
        }

        private void MvGVI_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePivot.SelectedIndex = 2;
        }

        private void OneGVI_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePivot.SelectedIndex = 1;
        }

        private void HomeGV_ItemClick(object sender, ItemClickEventArgs e)
        {
            Video video = e.ClickedItem as Video;
            Messenger.Default.Send<object>(video, "RootFrameNavigatedTo");
        }
        #endregion

        #region Messenger

        /// <summary>
        /// Register messenger
        /// </summary>
        private void RegisterMessenger()
        {
            Messenger.Default.Register<Double>(this, "CarouselSlideInterval", CarouselSlideInterval);
        }

        #region Carousel Slide Interval
        private void CarouselSlideInterval(double slideInterval)
        {
            Carousel.SlideInterval = slideInterval;
        }
        #endregion

        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //BusyGrid.Visibility = Visibility.Visible;
        }

    }

}
