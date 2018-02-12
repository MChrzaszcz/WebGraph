using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGraph
{
    class GraphGenerator
    {
        private static string _sourceURLString = "http://www.ur.edu.pl/";
        public static string _websitePattern { get; set; } = "<base href=\"http://www.ur.edu.pl/\"></base>";
        public static string domain { get; set; } = "http://www.ur.edu.pl/";
        public static string saveFilePath { get; set; } = @"C:\Users\Mack\Desktop\urz2.txt";


       public void starGenerateGraph()
        {
            URLSearcher urlSearcher = new URLSearcher(_websitePattern, _sourceURLString, domain);
            urlSearcher.search(_sourceURLString);
            urlSearcher.saveVertexToFile(saveFilePath);
        }
    }
}
