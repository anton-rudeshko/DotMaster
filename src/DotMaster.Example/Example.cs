using DotMaster.Core.Interfaces;

namespace DotMaster.Example
{
    public class Artist : IBaseObject
    {
        public string ObjKey { get; set; }
        public string SrcKey { get; set; }

        public string Name { get; set; }
    }

    public class Track : IBaseObject
    {
        public string ObjKey { get; set; }
        public string SrcKey { get; set; }

        public string Title { get; set; }

        public Artist Artist { get; set; }
    }

    internal class Discogs : BaseSourceDataProvider
    {
        public Discogs() : base(new DiscogsSource()) // todo: источники в енум?
        {
        }
    }

    internal class DiscogsSource : ISource
    {
        public string Name { get; set; }
    }

    internal class TrackXref : ICrossReference
    {
        public string BaseObjKey { get; set; }
        public ISource Source { get; set; }
        public string SourceKey { get; set; }

        public IBaseObject Object { get; set; }
    }
}