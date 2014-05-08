using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IMDB
{
    class MovieData
    {
        public XDocument doc;
        //constuctor that fills xdocument with xml data for selected movie
        public MovieData(string search, char type)
        {
            string url = "http://www.omdbapi.com/?plot=full&r=xml&" + type + "=" + search;
            doc = XDocument.Load(Database.MySQLEscape(url));
            //map key value
            Info = new Dictionary<string, string>(){
                {"title",       Attr("title")       },
                {"year",        Attr("year")        },
                {"poster",      Attr("poster")      },
                {"plot",        Attr("plot")        },
                {"rated",       Attr("rated")       },
                {"released",    Attr("released")    },
                {"runtime",     Attr("runtime")     },
                {"genre",       Attr("genre")       },
                {"director",    Attr("director")    },
                {"writer",      Attr("writer")      },
                {"actors",      Attr("actors")      },
                {"language",    Attr("language")    },
                {"country",     Attr("country")     },
                {"awards",      Attr("awards")      },
                {"metascore",   Attr("metascore")   },
                {"imdbVotes",   Attr("imdbVotes")   },
                {"imdbrating",  Attr("imdbRating")  },
                {"imdbID",      Attr("imdbID")      },
                {"type",        Attr("type")        }
            };
        }
        private static Dictionary<string, string> _info;
        public static Dictionary<string, string> Info
        {
            get
            {
                return _info;
            }
            private set { _info = value; }
        }
       
       
        //return value of attribute in "movie" element
        private string Attr(string attr)
        {
            return doc.Root.Element("movie").Attribute(attr).Value;
        }
    }
}
