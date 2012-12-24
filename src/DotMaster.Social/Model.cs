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

            m.HasMany(x => x.Posts);

            m.HasManyToMany(x => x.Friends)
             .ParentKeyColumn("Friend1Id")
             .ChildKeyColumn("Friend2Id")
             .Cascade.All();
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
    public class Post : IntBaseObject<Post, PostXref>
    {
        /// <summary>
        /// Дата
        /// </summary>
        public virtual DateTime PostedOn { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public virtual Profile Profile { get; set; }
    }

    public class PostXref : IntCrossReference<Post, PostXref> {}

    public class PostMap : IntBaseObjectMap<Post, PostXref>
    {
        public PostMap()
        {
            MapBaseObject(this);
            References(x => x.Profile);
        }

        public static void MapBaseObject(ClasslikeMapBase<Post> part)
        {
            part.Map(x => x.PostedOn).Not.Nullable();
            part.Map(x => x.Text).Not.Nullable();
        }
    }

    public class PostXrefMap : IntXrefMap<Post, PostXref>
    {
        public PostXrefMap()
        {
            Component(x => x.ObjectData, part =>
                {
                    PostMap.MapBaseObject(part);
                    part.References(x => x.Profile).ForeignKey("none");
                });
        }
    }
}
