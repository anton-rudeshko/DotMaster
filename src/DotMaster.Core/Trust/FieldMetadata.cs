using System;
using System.Reflection;

namespace DotMaster.Core.Trust
{
    public class FieldMetadata
    {
        public PropertyInfo PropertyInfo { get; set; }

        public bool IsRelationship { get; set; }

        public bool IsMastered { get; set; }

        public Type DeclaredType { get; set; }

        public ITrustStrategy TrustStrategy { get; set; }
    }
}
