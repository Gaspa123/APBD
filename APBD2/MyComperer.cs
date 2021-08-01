using System;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace APBD2
{
    class MyComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.Fname} {x.Lname} {x.IndexNumber}",
                $"{y.Fname} {y.Lname} {y.IndexNumber}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode($"{obj.Fname} {obj.Lname} {obj.IndexNumber}");
        }
    }
}
