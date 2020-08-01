using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lansh.Model
{
    [XmlRoot("LocalStorage")]
    public class LocalStorage
    {
        private List<Mv> mvList = new List<Mv>();

        [XmlArray(ElementName = "MvList")]
        public List<Mv> MvList
        {
            get
            {
                return mvList;
            }
            set
            {
                mvList = value;
            }
        }

    }
}
