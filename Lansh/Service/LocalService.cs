using Lansh.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Lansh.Service
{
    public class LocalService
    {
        public async Task<List<Mv>> GetMvList( )
        {
            StorageFile storageFile = await Package.Current.InstalledLocation.GetFileAsync(@"Assets\Local\Local.xml");
            string s;
            using (Stream File = await storageFile.OpenStreamForReadAsync())
            {
                using (StreamReader read = new StreamReader(File))
                {
                    s = read.ReadToEnd();
                }
            }
            XmlSerializer xmlSearializer = new XmlSerializer(typeof(LocalStorage));
            LocalStorage localStorage = new LocalStorage();
            using (StringReader sr = new StringReader(s))
            {
                localStorage = (LocalStorage)xmlSearializer.Deserialize(sr);
            }
            List<Mv> mvList = localStorage.MvList;
            return mvList;
        }
    }
}
