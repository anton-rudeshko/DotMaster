using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using FluentNHibernate.Cfg;

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

    internal class Discogs : BaseSourceDataProvider<TrackXref, Track>
    {
        public Discogs() : base(new DiscogsSource()) // todo: источники в енум?
        {
        }
    }

    internal class DiscogsSource : ISource
    {
        public string Name { get; set; }
    }

    internal class TrackXref : ICrossReference<Track>
    {
        public string BaseObjKey { get; set; }
        public ISource Source { get; set; }
        public string SourceKey { get; set; }

        public Track Object { get; set; }
    }

    public class Example
    {
        public static void Main(string[] args)
        {
            var kernel = new Kernel<TrackXref, Track>(null); // todo: master db
            var discogs = new Discogs();
            kernel.RegisterDataProvider(discogs);
            discogs.Provide(new TrackXref());
        }
    }
}