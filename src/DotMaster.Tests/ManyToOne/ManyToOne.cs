using System.Collections.Generic;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;

namespace DotMaster.Tests.ManyToOne
{
    public class Student : IntBaseObject<Student, StudentXref>
    {
        public virtual string Name { get; set; }
        public virtual IList<Lecture> Lectures { get; set; }
    }

    public class StudentXref : IntCrossReference<Student, StudentXref> {}

    public class StudentMap : IntBaseObjectMap<Student, StudentXref>
    {
        public StudentMap()
        {
            Map(x => x.Name);
            HasMany(x => x.Lectures);
        }
    }

    public class StudentXrefMap : IntXrefMap<Student, StudentXref>
    {
        public StudentXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Name));
        }
    }

    public class Lecture : IntBaseObject<Lecture, LectureXref>
    {
        public virtual string Title { get; set; }
        public virtual Student Student { get; set; }
    }

    public class LectureXref : IntCrossReference<Lecture, LectureXref> {}

    public class LectureMap : IntBaseObjectMap<Lecture, LectureXref>
    {
        public LectureMap()
        {
            Map(x => x.Title);
            References(x => x.Student);
        }
    }

    public class LectureXrefMap : IntXrefMap<Lecture, LectureXref>
    {
        public LectureXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Title));
        }
    }
}
