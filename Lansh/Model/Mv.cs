using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lansh.Model
{
    public class Mv
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Pic")]
        public string Pic { get; set; }
        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "PlayUrl")]
        public string PlayUrl { get; set; }
        [XmlElement(ElementName = "Comment")]
        public string Comment { get; set; }
        [XmlElement(ElementName = "Like")]
        public string Like { get; set; }
        [XmlElement(ElementName = "Owner")]
        public string Owner { get; set; }
        [XmlElement(ElementName = "PlayCount")]
        public string PlayCount { get; set; }
    }
}
