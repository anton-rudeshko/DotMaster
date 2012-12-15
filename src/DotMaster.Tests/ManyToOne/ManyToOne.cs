using System.Collections.Generic;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;

namespace DotMaster.Tests.ManyToOne
{
    public class Student : LongBaseObject<Student, StudentXref>
    {
        public virtual string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
    }

    public class StudentXref : LongCrossReference<Student, StudentXref> {}

    public class StudentMap : LongBaseObjectMap<Student, StudentXref>
    {
        public StudentMap()
        {
            Map(x => x.Name);
            HasMany(x => x.Lectures);
        }
    }

    public class StudentXrefMap : LongXrefMap<Student, StudentXref>
    {
        public StudentXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Name));
        }
    }

    public class Lecture : LongBaseObject<Lecture, LectureXref>
    {
        public virtual string Title { get; set; }
        public virtual Student Student { get; set; }
    }

    public class LectureXref : LongCrossReference<Lecture, LectureXref> {}

    public class LectureMap : LongBaseObjectMap<Lecture, LectureXref>
    {
        public LectureMap()
        {
            Map(x => x.Title);
            References(x => x.Student);
        }
    }

    public class LectureXrefMap : LongXrefMap<Lecture, LectureXref>
    {
        public LectureXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Title));
        }
    }
}
