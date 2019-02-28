using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Moq
{
    class VideoRepository : IVideoRepository
    {
        private IVideoContext _videoContext;

        public VideoRepository(IVideoContext videoContext = null)
        {
            _videoContext = videoContext ?? new VideoContext();
        }
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            var videos =
                (from video in _videoContext.Videos
                 where !video.IsProcessed
                 select video).ToList();

            return videos;

        }
    }
}
