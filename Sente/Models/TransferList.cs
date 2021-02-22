using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sente.Models
{
    [XmlRoot("przelewy")]
    public class TransferList
    {
        [XmlElement("przelew")]
        public List<Transfer> Transfers { get; set; }

        public bool NoTransfers()
        {
            return this is null || this.Transfers == null || this.Transfers.Count <= 0;
        }
    }
}
