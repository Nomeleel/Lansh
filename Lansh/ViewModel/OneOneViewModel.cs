using AppStudio.Common.Commands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Lansh.Common.Model;
using Lansh.Common.Service;
using Lansh.Model;
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
    public class OneOneViewModel : ViewModelBase
    {
        #region Data Source
        public ObservableCollection<OneOne> OneOneList { get; set; } = new ObservableCollection<OneOne>();
        #endregion

        #region Command
        public ICommand LoadedCommand => new RelayCommand(() =>
        {
            One();
        });
        #endregion

        #region Service
        private ResultService _resultService = new ResultService();
        #endregion

        #region HomeViewModel
        public OneOneViewModel()
        {

        }
        #endregion

        #region Mothod
        public async void One()
        {
            try
            {
                Messenger.Default.Send<bool>(true, "ToggleBusy");
                List<One> oneList = await _resultService.GetOneList();
                Messenger.Default.Send<bool>(false, "ToggleBusy");
                foreach (One one in oneList)
                {
                    OneOneList.Add(OneOne.Conversion(one));
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
