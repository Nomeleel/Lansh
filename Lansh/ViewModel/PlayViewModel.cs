using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using Windows.Media.Core;
using Windows.Media.Playback;

namespace Lansh.ViewModel
{
    public class PlayViewModel : ViewModelBase
    {
        #region Data Source

        private VideoInfoEx videoInfoEx;
        public VideoInfoEx VideoInfoEx
        {
            get
            {
                return videoInfoEx;
            }
            set
            {
                videoInfoEx = value;
                RaisePropertyChanged("VideoInfoEx");
            }
        }

        private string replyCommentCount;
        public string ReplyCommentCount
        {
            get
            {
                return replyCommentCount;
            }
            set
            {
                replyCommentCount = value;
                RaisePropertyChanged("ReplyCommentCount");
            }
        }

        public ObservableCollection<ReplyComment> HotReplyCommentList { get; set; } = new ObservableCollection<ReplyComment>();
        public ObservableCollection<ReplyComment> ReplyCommentList { get; set; } = new ObservableCollection<ReplyComment>();

        #endregion

        #region Atttr

        public object Param { get; set; }

        public string PlayUrl;

        #endregion

        #region Command

        public ICommand LoadedCommand  => new RelayCommand(() =>
        {
            Play();
            FillReplyComment();
        });

        #endregion

        #region Service

        private ResultService _resultService = new ResultService();

        #endregion

        #region PlayViewModel

        public PlayViewModel()
        {
            VideoInfoEx = new VideoInfoEx();
        }

        #endregion

        #region Method

        public async void Play()
        {
            try
            {
                Messenger.Default.Send<bool>(true, "PlayLoaded");
                if (Param is Video)
                {
                    VideoInfoEx = VideoInfoEx.Conversion(await _resultService.GetVideoInfo((Param as Video).Aid));
                    PlayUrl = await _resultService.GetPlayUrl(videoInfoEx.Cids.Last(), 3);
                }
                else
                {
                    Mv mv = (Param as Mv);
                    VideoInfoEx.Title = mv.Title;
                    VideoInfoEx.Desc = mv.Desc;
                    PlayUrl = mv.PlayUrl;
                }
                Messenger.Default.Send<bool>(true, "AreTransportContro");
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(PlayUrl));
                mediaPlayer.Volume = 0.07f;
                mediaPlayer.IsLoopingEnabled = true;
                Messenger.Default.Send<MediaPlayer>(mediaPlayer, "ReSetMediaPlayer");
                Messenger.Default.Send<bool>(false, "PlayLoaded");
            }
            catch (COMException)
            {
                Messenger.Default.Send<object>(null, "Error");
            }
            catch (Exception)
            {
                Messenger.Default.Send<object>(null, "Error");
            }
            finally
            {
                Messenger.Default.Send<bool>(false, "PlayLoaded");
            }
        }

        public void FillReplyComment()
        {
            if (Param is Video)
                Reply();
            else
                Comment();
        }

        public async void Reply()
        {
            try
            {
                ReplyResult ReplyResult = await _resultService.GetReply((Param as Video).Aid, 1);
                ReplyCommentCount = ReplyResult.Results;
                HotReplyCommentList.Clear();
                foreach (Reply reply in ReplyResult.HotList)
                    HotReplyCommentList.Add(ReplyComment.Conversion(reply));
                ReplyCommentList.Clear();
                foreach (Reply reply in ReplyResult.List)
                    ReplyCommentList.Add(ReplyComment.Conversion(reply));
            }
            catch (COMException)
            {
                Messenger.Default.Send<object>(null, "Error");
            }
            catch (Exception)
            {

            }
        }

        public async void Comment()
        {
            try
            {
                CommentResult commentResult = await _resultService.GetComment((Param as Mv).Comment);
                ReplyCommentCount = commentResult.Total;
                HotReplyCommentList.Clear();
                foreach (Comment comment in commentResult.HotComments)
                    HotReplyCommentList.Add(ReplyComment.Conversion(comment));
                ReplyCommentList.Clear();
                foreach (Comment comment in commentResult.Comments)
                    ReplyCommentList.Add(ReplyComment.Conversion(comment));
            }
            catch (COMException)
            {
                Messenger.Default.Send<object>(null, "Error");
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}
