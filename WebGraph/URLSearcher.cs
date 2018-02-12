using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace WebGraph
{
    class URLSearcher
    {
        public string _currentPageString { get; set; }
        public string _websitePattern { get; set; }    //if appears that mean the page belongs to domain
        public string _websiteURLSource { get; set; }
        public string _domain { get; set; }
        public List<List<string>> _vertexList { get; set; } = new List<List<string>>();
        StringBuilder _edgeBuilder = new StringBuilder();
        private int _i;

        public URLSearcher(string websitePattern, string websiteSource, string domain)
        {
            _websitePattern = websitePattern;
            _websiteURLSource = websiteSource;
            _domain = domain;
        }
      public string getCurrentWebsite()
       {
           return _currentPageString;
       }

  
        public void search(string currentPageURL)
        {
            PageUtilities pageUtilities = new PageUtilities();
            _currentPageString = pageUtilities.getPageContent(currentPageURL);
            List<string> URLList = pageUtilities.getURLsFromPage(_currentPageString, _domain);

//            string links="";
//            foreach (string url in URLList)
//            {
//                links += url + "\n";
//            }
//            File.WriteAllText(@"C:\Users\Mack\Desktop\url.txt", links);

            _i++;
            if (_i % 3000 == 0)
               saveVertexToFile(@"C:\Users\Mack\Desktop\urz2.txt");


            if (!_currentPageString.Contains(_websitePattern)) 
                return;

            

            foreach (string url in URLList)
            {
                add2ConnectedVertexToList(url, currentPageURL);
                _edgeBuilder.Append(currentPageURL + " " + url + "\n");
            }

            foreach (string url in URLList)
            {
                if (isVertexRepeat(url))
                    continue;

                Console.WriteLine(_i + ". " + currentPageURL + "   ---   " + url);
                search(url);     
            }
        }



        void add2ConnectedVertexToList(string childPageURL, string parentPageURL)
        {
            List<string> twoConnectedVertex = new List<string>();
            twoConnectedVertex.Add(parentPageURL);
            twoConnectedVertex.Add(childPageURL);
            _vertexList.Add(twoConnectedVertex);
        }   

        
      
        bool isEdgeRepeat(string childPageURL, string parentPageURL)   // that function check if edges and vertex is repeating or not 
        {
            foreach (List<string> twoConnectedVertex in _vertexList)
            {

                    //in this condition we check if 2 connected vertex didnt repeat before
                    if (twoConnectedVertex[0] == parentPageURL && twoConnectedVertex[1] == childPageURL)
                        return true;                                 
            }
            return false;
        }


        bool isVertexRepeat(string currentPageURL)           // in this function we check that if current vertex wasnt parent url before
        {
            foreach (List<string> twoConnectedVertex in _vertexList)
            {  
                if (twoConnectedVertex[0] == currentPageURL)   // in 0 position list contain parent url
                    return true;        
            }
            return false;
        }

        public void saveVertexToFile(string path)
        {
            File.WriteAllText(path, _edgeBuilder.ToString());
        }
        }
    }

