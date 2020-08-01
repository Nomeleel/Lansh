using GalaSoft.MvvmLight;
using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class VideoInfoEx : ObservableObject
    {
        public string Aid { get; set; }
        private string desc;
        public string Desc
        {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                RaisePropertyChanged("Desc");
            }
        }
        public string Duration { get; set; }
        public string Owner { get; set; }
        public string OwnerFace { get; set; }
        //public List<VideoInfoPage> Pages { get; set; }
        public List<string> Cids { get; set; }
        public string Pic { get; set; }
        public string Pubdate { get; set; }
        public ObservableCollection<string> Tags { get; set; }
        public string Tid { get; set; }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        public static VideoInfoEx Conversion(VideoInfo videoInfo)
        {
            VideoInfoEx video = new VideoInfoEx();
            video.Aid = videoInfo.Aid;
            video.Desc = videoInfo.Desc;
            video.Title = videoInfo.Title;
            video.Owner = videoInfo.Owner.Name;
            video.OwnerFace = videoInfo.Owner.Face;
            video.Tags = new ObservableCollection<string>();
            foreach (string str in videoInfo.Tags)
            {
                video.Tags.Add(str);
            }
            video.Pic = videoInfo.Pic;
            video.Cids = new List<string>();
            foreach (VideoInfoPage vip in videoInfo.Pages)
            {
                string cid = vip.Cid;
                video.Cids.Add(cid.Contains(".0") ? cid.Substring(0, 8) : cid);
            }
            return video;
        }

    }
}
