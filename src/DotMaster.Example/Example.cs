using System.Collections.Generic;
using DotMaster.Core.Interfaces;
using FluentNHibernate.Mapping;

namespace DotMaster.Example
{
    public class Artist : IBaseObject
    {
        public virtual string ObjKey { get; set; }
        public virtual string SrcKey { get; set; }

        public virtual string Name { get; set; }
        public virtual IList<Track> Tracks { get; set; }
        public IList<ICrossReference> Xrefs { get; set; }
    }

    public class Track : IBaseObject
    {
        public virtual string ObjKey { get; set; }
        public virtual string SrcKey { get; set; }

        public virtual string Title { get; set; }
        public virtual Artist Artist { get; set; }
        public IList<ICrossReference> Xrefs { get; set; }
    }

    internal class Discogs : BaseSourceDataProvider<TrackXref>
    {
        public Discogs() : base(new DiscogsSource()) // todo: источники в енум?
        {
        }
    }

    internal class DiscogsSource : ISource
    {
        public string Name { get; set; }
    }

    public class TrackXref : ICrossReference
    {
        public virtual string BaseObjKey { get; set; }
        public virtual ISource Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual IBaseObject Object { get; set; }
        public string ObjKey { get; set; }
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