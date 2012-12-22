using System.Collections.Generic;
using DotMaster.Core.Match;
using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;
using FluentNHibernate.Mapping;

namespace DotMaster.Example
{
    [Match(typeof(Student))]
    public class Student : IntBaseObject<Student, StudentXref>, IMatchRule<Student>
    {
        public virtual string Name { get; set; }
        public virtual IList<Mark> Marks { get; set; }
        public virtual StudentType StudentType { get; set; }

        public virtual bool IsMatch(Student other)
        {
            return other.Name == Name && other.StudentType == StudentType;
        }
    }

    public class StudentXref : IntCrossReference<Student, StudentXref> {}

    public class StudentMap : IntBaseObjectMap<Student, StudentXref>
    {
        public StudentMap()
        {
            Map(x => x.Name);
            HasMany(x => x.Marks).Cascade.All();
            References(x => x.StudentType);
        }
    }

    public class StudentXrefMap : IntXrefMap<Student, StudentXref>
    {
        public StudentXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Name));
        }
    }

    public class Mark : IntBaseObject<Mark, MarkXref>
    {
        public virtual int Value { get; set; }
        public virtual Student Student { get; set; }
    }

    public class MarkXref : IntCrossReference<Mark, MarkXref> {}

    public class MarkMap : IntBaseObjectMap<Mark, MarkXref>
    {
        public MarkMap()
        {
            Map(x => x.Value);
            References(x => x.Student);
        }
    }

    public class MarkXrefMap : IntXrefMap<Mark, MarkXref>
    {
        public MarkXrefMap()
        {
            Component(x => x.ObjectData, m => m.Map(x => x.Value));
        }
    }

    public class StudentType
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Code { get; set; }
    }

    public class StudentTypeMap : ClassMap<StudentType>
    {
        public StudentTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Code);
        }
    }
}
