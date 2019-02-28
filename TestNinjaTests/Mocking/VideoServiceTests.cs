using NUnit.Framework;
using TestNinja.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Moq;

namespace TestNinja.Moq.Tests
{
    [TestFixture()]
    public class VideoServiceTests
    {
        [Test()]
        public void GetUnprocessedVideosAsCsv_ThereIsVideosNotProcessed_ReturnStringOfVideosNotProcessed()
        {
            var mockVideoRepository = new Mock<IVideoRepository>();
            var mockFileReader = new Mock<IFileReader>();
            var videoService = new VideoService(mockVideoRepository.Object, mockFileReader.Object);

            var MockDbSet = new Mock<DbSet<Video>>();

            var mockListVideos = new List<Video> {
                    new Video { Id = 1, IsProcessed = false, Title = "Teste" },
                    new Video { Id = 2, IsProcessed = true, Title = "Teste Processed" }
                };

            mockVideoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(mockListVideos);

            Assert.That(videoService.GetUnprocessedVideosAsCsv(), Has.Length.GreaterThan(0));
        }
    }
}