using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class Video: SearchResult
    {
        public string Play { get; set; }
        public string Description { get; set; }
        //public long Senddate { get; set; }
        //public bool Badgepay { get; set; }
        public string Pic { get; set; }
        public string Tag { get; set; }
        public string VideoReview { get; set; }
        public string Favorites { get; set; }
        public string Duration { get; set; }
        public long Id { get; set; }
        public long RankScore { get; set; }
        public string TypeId { get; set; }
        public string Pubdate { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int Review { get; set; }
        public string Author { get; set; }
        public string Mid { get; set; }
        public string Arcurl { get; set; }
        public string Typename { get; set; }
        public string Aid { get; set; }
        public string Arcrank { get; set; }
        public string Create { get; set; }
        public string Credit { get; set; }
        public string Coins { get; set; }

        /// <summary>
        /// For Index Video
        /// </summary>
        /// <param name="homeVideo"></param>
        public Video(HomeVideo homeVideo)
        {
            Pic = homeVideo.Pic;
            Title = homeVideo.Title;
            Play = homeVideo.Play;
            Favorites = homeVideo.Favorites;
            Mid = homeVideo.Mid;
            Aid = homeVideo.Aid;
        }

        public Video()
        {

        }

        public override SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            Video video = new Video();
            video.Play = searchResultInfo.Play;
            video.Pic = searchResultInfo.Pic;
            video.VideoReview = searchResultInfo.Video_Review;
            video.Favorites = searchResultInfo.Favorites;
            video.Duration = searchResultInfo.Duration;
            video.Title = searchResultInfo.Title;
            video.Author = searchResultInfo.Author;
            video.Mid = searchResultInfo.Mid;
            video.Aid = searchResultInfo.Aid;
            return video;
        }

        public static Video Conversion(HomeVideo homeVideo)
        {
            Video video = new Video();
            video.Pic = homeVideo.Pic;
            video.Title = homeVideo.Title;
            video.Play = homeVideo.Play;
            video.Favorites = homeVideo.Favorites;
            //video.Duration = homeVideo.Duration;
            //video.Author = homeVideo.Author;
            video.Mid = homeVideo.Mid;
            video.Aid = homeVideo.Aid;
            return video;
        }
    }
}
