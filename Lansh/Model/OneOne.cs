using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class OneOne
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string DisplayCategory { get; set; }
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Forward { get; set; }
        public string ImgUrl { get; set; }
        public string LikeCount { get; set; }
        public string PostDate { get; set; }
        public string LastUpdateDate { get; set; }
        //public Author author { get; set; }
        public string VideoUrl { get; set; }
        public string AudioUrl { get; set; }
        public string AudioPlatform { get; set; }
        public string Startvideo { get; set; }
        public string Volume { get; set; }
        public string PicInfo { get; set; }
        public string WordsInfo { get; set; }
        public string Subtitle { get; set; }
        public string Number { get; set; }
        public string SerialId { get; set; }
        public string[] SerialList { get; set; }
        public string MovieStoryId { get; set; }
        public string AdId { get; set; }
        public string AdType { get; set; }
        public string AdPvurl { get; set; }
        public string AdLinkurl { get; set; }
        public string AdMakettime { get; set; }
        public string AdClosetime { get; set; }
        public string AdShareCnt { get; set; }
        public string AdPvurlVendor { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string ContentBgcolor { get; set; }
        public string ShareUrl { get; set; }
        //public shareinfo shareinfo { get; set; }
        //public string sharelist { get; set; }
        public string[] TagList { get; set; }

        public static OneOne Conversion(One one)
        {
            OneOne oneOne = new OneOne();
            oneOne.ImgUrl = one.Img_Url;
            oneOne.Title = one.Title;
            oneOne.Forward = one.Forward;
            oneOne.Volume = one.Volume;
            return oneOne;
        }
    }
}
