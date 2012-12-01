using System.Collections.Generic;
using DotMaster.Core.Interfaces;
using FluentNHibernate.Mapping;

namespace DotMaster.Example
{
    public class Artist : IBaseObject
    {
        public string ObjKey { get; set; }
        public string SrcKey { get; set; }

        public string Name { get; set; }
        public IList<Track> Tracks { get; set; }
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

    public class TrackMap : ClassMap<Track>
    {
        public TrackMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.Title);

            References(track => track.Artist);
        }
    }

    public class ArtistMap : ClassMap<Artist>
    {
        public ArtistMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.Name);

            HasMany(artist => artist.Tracks);
        }
    }
}