using System.Collections.Generic;

namespace TestNinja.Moq
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}