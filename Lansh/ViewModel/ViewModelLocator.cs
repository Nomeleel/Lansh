using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Lansh.Model;
using Lansh.View;
using Lansh.Service;

namespace Lansh.ViewModel
{

    public class ViewModelLocator
    {
        public const string MainVieweKey = "MainPage";
        public const string PlayVieweKey = "Play";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<PlayViewModel>();
            SimpleIoc.Default.Register<DbContext>();
            SimpleIoc.Default.Register<DataService>();
            SimpleIoc.Default.Register<LocalService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            var nav = new NavigationService();
            nav.Configure(MainVieweKey, typeof(MainPage));
            nav.Configure(PlayVieweKey, typeof(Play));
            SimpleIoc.Default.Register<INavigationService>(() => nav);

        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public MainPageViewModel Main => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        /// <summary>
        /// Gets the Search property.
        /// </summary>
        public SearchViewModel Search => ServiceLocator.Current.GetInstance<SearchViewModel>();

        /// <summary>
        /// Gets the Play property.
        /// </summary>
        public PlayViewModel Play => ServiceLocator.Current.GetInstance<PlayViewModel>();
    }
}
