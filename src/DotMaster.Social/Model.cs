using System;
using System.Collections.Generic;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;
using FluentNHibernate.Mapping;

namespace DotMaster.Social
{
    public class Profile : IntBaseObject<Profile, ProfileXref>
    {
        /// <summary>
        /// Полное имя
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual int Age { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual Sex Sex { get; set; }

        /// <summary>
        /// Занятость
        /// </summary>
        public virtual string Occupation { get; set; }

        /// <summary>
        /// Сообщения на стене
        /// </summary>
        public virtual IList<Post> Posts { get; set; }

        /// <summary>
        /// Друзья
        /// </summary>
        public virtual IList<Profile> Friends { get; set; }
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
            m.Map(x => x.Sex).Not.Nullable();

            m.HasManyToMany(x => x.Friends);
        }
    }

    public class ProfileXrefMap : IntXrefMap<Profile, ProfileXref>
    {
        public ProfileXrefMap()
        {
            Component(x => x.ObjectData, ProfileMap.MapBaseObject);
        }
    }

    public class Country
    {
        public virtual int Id { get; set; }
        public virtual string IsoCode { get; set; }
        public virtual string Name { get; set; }
    }

    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id).Not.Nullable();

            Map(x => x.Name).Not.Nullable();
            Map(x => x.IsoCode).Not.Nullable();
        }
    }

    /// <summary>
    /// Запись на стене
    /// </summary>
    public class Post : IntBaseObject<Post, PostXref>
    {
        /// <summary>
        /// Дата
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public virtual string Text { get; set; }
    }

    public class PostXref : IntCrossReference<Post, PostXref> {}

    public class PostMap : IntBaseObjectMap<Post, PostXref>
    {
        public PostMap()
        {
            MapBaseObject(this);
        }

        public static void MapBaseObject(ClasslikeMapBase<Post> m)
        {
            m.Map(x => x.Date).Not.Nullable();
            m.Map(x => x.Text).Not.Nullable();
        }
    }

    public class PostXrefMap : IntXrefMap<Post, PostXref>
    {
        public PostXrefMap()
        {
            Component(x => x.ObjectData, PostMap.MapBaseObject);
        }
    }
}
