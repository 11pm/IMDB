using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IMDB
{
    class Search
    {
        public XDocument doc;
        private string _search { get; set; }
        public Search(string search)
        {
            _search = search;
            //reuturns online hosted xml into the xdoc
            string url = "http://www.omdbapi.com/?r=xml&s=" + search;
            doc = XDocument.Load(url); 
                
        }
        //Todo convert title to imdbid for better experience
        public string Imbd()
        {
            return " ";
        }
        
    }
}
