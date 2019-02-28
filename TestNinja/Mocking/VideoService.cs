using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Moq
{
    public class VideoService
    {
        private IVideoRepository _videoRepository;
        private IFileReader _fileReader;

        public VideoService(IVideoRepository videoRepository = null, IFileReader fileReader = null) //Poor man's dependency. Should use Ninject or another Dependy Injection Framework  -   :(
        {
            _videoRepository = videoRepository ?? new VideoRepository(); //<--Poor man's dependency. Should use Ninject or another Dependy Injection Framework  -   :(
            _fileReader = fileReader ?? new FileReader();
        }
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);

        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext, IVideoContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}