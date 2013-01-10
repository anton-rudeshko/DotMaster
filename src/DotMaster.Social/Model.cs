using System.Collections.Generic;
using DotMaster.Core.Match;
using DotMaster.Core.Model.Impl;
using DotMaster.Core.Trust.Attributes;
using DotMaster.NHibernate.Mappings;
using FluentNHibernate.Mapping;

namespace DotMaster.Social
{
    [FixedScore(30, ForSource = "Facebook")]
    [FixedScore(50, ForSource = "MAI")]
    [FixedScore(70, ForSource = "Google Plus")]
    public class Profile : IntBaseObject<Profile, ProfileXref>, IMatchRule<Profile>
    {
        [FixedScore(80, ForSource = "MAI")]
        public virtual string FullName { get; set; }

        public virtual int? Age { get; set; }

        public virtual Sex? Sex { get; set; }

        [FixedScore(10, ForSource = "MAI")]
        public virtual string Occupation { get; set; }

        public virtual IList<Address> Addresses { get; set; }

        public virtual bool IsMatch(Profile other)
        {
            // сравнение профилей
            return false;
        }
    }

    public enum Sex
    {
        Unknown = 0, Male = 1, Female = 2
    }

    [FixedScore(30, ForSource = "Facebook")]
    [FixedScore(50, ForSource = "MAI")]
    [FixedScore(70, ForSource = "Google Plus")]
    public class Address : IntBaseObject<Address, AddressXref>, IMatchRule<Address>
    {
        public virtual Country Country { get; set; }

        public virtual string City { get; set; }

        public virtual string Line { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual bool IsMatch(Address other)
        {
            // сравнение адресов
            return false;
        }
    }

    public class Country
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string IsoCode { get; set; }
    }

    public class ProfileXref : IntCrossReference<Profile, ProfileXref> {}

    public class AddressXref : IntCrossReference<Address, AddressXref> {}

    public class ProfileMap : IntBaseObjectMap<Profile, ProfileXref>
    {
        public ProfileMap()
        {
            MapBaseObject(this);
        }

        public static void MapBaseObject(ClasslikeMapBase<Profile> m)
        {
            m.Map(x => x.FullName).Not.Nullable();
            m.Map(x => x.Age);
            m.Map(x => x.Sex).CustomType<int>();
            m.Map(x => x.Occupation);

            m.HasMany(x => x.Addresses);
        }
    }

    public class ProfileXrefMap : IntXrefMap<Profile, ProfileXref>
    {
        public ProfileXrefMap()
        {
            Component(x => x.ObjectData, ProfileMap.MapBaseObject);
        }
    }

    public class AddressMap : IntBaseObjectMap<Address, AddressXref>
    {
        public AddressMap()
        {
            MapProperties(this);

            References(x => x.Country).Cascade.None();
            References(x => x.Profile);
        }

        public static void MapProperties(ClasslikeMapBase<Address> part)
        {
            part.Map(x => x.Line).Not.Nullable();
        }
    }

    public class AddressXrefMap : IntXrefMap<Address, AddressXref>
    {
        public AddressXrefMap()
        {
            Component(x => x.ObjectData, part =>
                {
                    AddressMap.MapProperties(part);
                    part.References(x => x.Profile).ForeignKey("none");
                    part.References(x => x.Country).ForeignKey("none");
                });
        }
    }

    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsoCode);
        }
    }
}
