using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData(string name)
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }


        public bool Equals(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override string ToString()
        {
            return "name=" + Name;
        }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}