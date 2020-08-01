using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Lansh.Common.Service;
using Lansh.Model;
using Lansh.Service;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lansh.ViewModel
{
    public class MusicVideoViewModel : ViewModelBase
    {
        #region Data Source

        public ObservableCollection<Mv> MvList { get; set; } = new ObservableCollection<Mv>();

        #endregion

        #region Command
        public ICommand LoadedCommand => new RelayCommand(() =>
        {
            Mv();
        });
        #endregion

        #region Service
        private ResultService _resultService = new ResultService();
        private LocalService _localService = ServiceLocator.Current.GetInstance<LocalService>();
        #endregion

        #region Mothod
        public async void Mv()
        {
            try
            {
                Messenger.Default.Send<bool>(true, "ToggleBusy");
                List<Mv> mvList = await _localService.GetMvList();
                Messenger.Default.Send<bool>(false, "ToggleBusy");
                foreach (Mv mv in mvList)
                {
                    MvList.Add(mv);
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
        #endregion

    }
}
