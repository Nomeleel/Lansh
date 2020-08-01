using AppStudio.Common.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Lansh.Common.Model;
using Lansh.Common.Service;
using Lansh.Model;
using Lansh.Service;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Toolkit.Uwp;
using Newtonsoft.Json;
//using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Lansh.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {

        #region Data Source
        public List<ObservableCollection<Video>> RecommendList { get; set; } = new List<ObservableCollection<Video>>();

        public ObservableCollection<CarouselItem> CarouselList { get; set; } = new ObservableCollection<CarouselItem>();

        public ObservableCollection<Mv> MvList { get; set; } = new ObservableCollection<Mv>();

        #endregion

        #region Command
        public ICommand LoadedCommand => new RelayCommand(() =>
        {
            Index();
        });
        #endregion

        #region Service
        private ResultService _resultService = new ResultService();
        private LocalService _localService = ServiceLocator.Current.GetInstance<LocalService>();
        #endregion

        #region HomeViewModel
        public HomeViewModel()
        {
            for(int i = 0;  i < 14; i++)
            {
                RecommendList.Add(new ObservableCollection<Video>());
            }
        }
        #endregion

        #region Mothod
        public async void Index()
        {
            try
            {
                Messenger.Default.Send<bool>(true, "ToggleBusy");
                Carousel();
                List<List<HomeVideo>> indexList = await _resultService.GetIndexListAsync();
                Messenger.Default.Send<bool>(false, "ToggleBusy");
                if (indexList != null && indexList.Count != 0)
                {
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        foreach (HomeVideo homeVideo in indexList[i])
                        {
                            RecommendList[i].Add(new Video(homeVideo));
                        }
                    }
                }
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
        }

        private void Carousel()
        {
            //CarouselList = new ObservableCollection<CarouselItem>();
            for (int i = 1; i < 6; i++)
            {
                CarouselList.Add(new CarouselItem()
                {
                    Title = "Title",
                    Category = "Category",
                    Image = "ms-appx:///Assets/Images/Carousel/Carousel_" + i + ".jpg"
                });
            }
            Messenger.Default.Send<double>(3000.00f, "CarouselSlideInterval");
        }

        public async Task<List<Mv>> Mv()
        {
            List<Mv> mvList = await _localService.GetMvList();
            //foreach (Mv mv in mvList)
            //{
            //    MvList.Add(mv);
            //}
            return mvList;
        }

        #endregion

        }

}
