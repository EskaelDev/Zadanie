using System.Xml.Serialization;

namespace Sente.Models
{
    public class Transfer
    {
        [XmlAttribute("od")]
        public int From { get; set; }

        [XmlAttribute("kwota")]
        public decimal Amount { get; set; }

    }
}
