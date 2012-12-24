using System;
using System.Collections.Generic;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;
using FluentNHibernate.Mapping;

namespace DotMaster.Social
{
    /// <summary>
    /// Социальный профиль пользователя
    /// </summary>
    public class Profile : IntBaseObject<Profile, ProfileXref>
    {
        /// <summary>
        /// Полное имя
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual Sex? Sex { get; set; }

        /// <summary>
        /// Занятость
        /// </summary>
        public virtual string Occupation { get; set; }

        /// <summary>
        /// Сообщения на стене
        /// </summary>
        public virtual IList<Address> Addresses { get; set; }
    }

    /// <summary>
    /// Пол
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// Неизвестен
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Мужской
        /// </summary>
        Male = 1,

        /// <summary>
        /// Женский
        /// </summary>
        Female = 2
    }

    public class ProfileXref : IntCrossReference<Profile, ProfileXref> {}

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
            m.Map(x => x.Sex);
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

    /// <summary>
    /// Запись на стене
    /// </summary>
    public class Address : IntBaseObject<Address, AddressXref>
    {
        /// <summary>
        /// Страна
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Строка адреса
        /// </summary>
        public virtual string Line { get; set; }

        /// <summary>
        /// Ссылка на профиль пользователя
        /// </summary>
        public virtual Profile Profile { get; set; }
    }

    public class AddressXref : IntCrossReference<Address, AddressXref> {}

    public class AddressMap : IntBaseObjectMap<Address, AddressXref>
    {
        public AddressMap()
        {
            MapProperties(this);

            References(x => x.Country);
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

    public class Country
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string IsoCode { get; set; }
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
