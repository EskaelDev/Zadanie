using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Sente.Models
{
    public class Participant : IComparable<Participant>
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("uczestnik")]
        public List<Participant> Subordinates { get; set; }

        public Participant Superior { get; set; }

        public int Level { get; set; }

        public int SubordinatesWithoutSubordinatesCount { get; set; }

        public decimal Commission { get; set; }

        public int CompareTo(Participant other)
        {
            return Id - other.Id;
        }

        public override string ToString()
        {
            return $"{Id} {Level} {SubordinatesWithoutSubordinatesCount} {Commission}";
        }
        public Participant()
        {
            SubordinatesWithoutSubordinatesCount = 0;
        }

    }
}
