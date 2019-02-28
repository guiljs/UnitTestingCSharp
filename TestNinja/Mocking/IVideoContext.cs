using System.Collections.Generic;
using System.Data.Entity;

namespace TestNinja.Moq
{
    public interface IVideoContext
    {
        DbSet<Video> Videos { get; set; }
    }
}