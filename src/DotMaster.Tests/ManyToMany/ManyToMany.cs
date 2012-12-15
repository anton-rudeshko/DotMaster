using System.Collections.Generic;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;

namespace DotMaster.Tests.ManyToMany
{
    public class Book : LongBaseObject<Book, BookXref>
    {
        public virtual string Title { get; set; }

        public virtual IList<Author> Authors { get; set; }
    }

    public class Author : LongBaseObject<Author, AuthorXref>
    {
        public virtual string Name { get; set; }

        public virtual IList<Book> Books { get; set; }
    }

    public class BookXref : LongCrossReference<Book, BookXref> {}

    public class AuthorXref : LongCrossReference<Author, AuthorXref> {}

    public class AuthorMap : LongBaseObjectMap<Author, AuthorXref>
    {
        public AuthorMap()
        {
            Map(x => x.Name);

            HasManyToMany(x => x.Books);
        }
    }

    public class BookMap : LongBaseObjectMap<Book, BookXref>
    {
        public BookMap()
        {
            Map(x => x.Title);

            HasManyToMany(x => x.Authors);
        }
    }

    public class BookXrefMap : LongXrefMap<Book, BookXref>
    {
        public BookXrefMap()
        {
            Map(x => x.ObjectData.Title);
        }
    }

    public class AuthorXrefMap : LongXrefMap<Author, AuthorXref>
    {
        public AuthorXrefMap()
        {
            Map(x => x.ObjectData.Name);
        }
    }
}
