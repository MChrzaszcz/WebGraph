using System;
using System.Collections.Generic;
using System.Net.Http;


namespace WebGraph
{
    class PageUtilities
    {
       public string getPageContent(string urlPage)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {


                    using (HttpResponseMessage response = client.GetAsync(urlPage).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            return content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine(" Problem with connection to website! Trying to reconnect.");
                return getPageContent(urlPage);
            }
            
        }



       public List<string> getURLsFromPage(string page,string pageDomain)
       {
           int n = page.Length;
           string pageURL = "";
           List<string> pageURLList = new List<string>();
           bool isNotPageUrlCompleted = true;
            for (int i = 1; i < n; i++)
            {
                pageURL = pageDomain;                                   // page domain - http://www.ur.edu.pl - in this case
                isNotPageUrlCompleted = true;
                if (page[i - 1] == '<' && page[i] == 'a')
                {
                    for (int j = i + 1; isNotPageUrlCompleted; j++)
                    {
                        try
                        {
                            
                            if (page[j] == '"' && page[j + 1] == '/')
                            {
                                if (page[j + 2] == 'f' && page[j + 3] == 'i' && page[j + 4] == 'l' && page[j + 5] == 'e') //ommit files
                                {
                                    isNotPageUrlCompleted = false;
                                    break;
                                }
                                for (int k = j + 2; ; k++)
                                {
                                    if (page[k] == 'k' && page[k + 1] == 'a' && page[k + 2] == 'l' && page[k + 3] == 'e' && page[k + 4] == 'n'
                                        && page[k + 5] == 'd' && page[k + 6] == 'a') //ommit calendar(resolve the infinity redirect issue)
                                    {
                                        isNotPageUrlCompleted = false;
                                        break;
                                    }
                                    if (page[k] == 'm' && page[k + 1] == 'o' && page[k + 2] == 'n' && page[k + 3] == 't' && page[k + 4] == 'h')
                                    {
                                        isNotPageUrlCompleted = false;
                                        break;
                                    }

                                    if (page[k] == 'd' && page[k + 1] == 'a' && page[k + 2] == 'y')
                                    {
                                        isNotPageUrlCompleted = false;
                                        break;
                                    }

                                    if (page[k] == '.' && page[k + 1] == 'j' && page[k + 2] == 's') // ommit redirect to js library
                                    {
                                        isNotPageUrlCompleted = false;
                                        break;
                                    }

                                    if (page[k] == '"')
                                    {
                                        isNotPageUrlCompleted = false;
                                        pageURLList.Add(pageURL);
                                        break;
                                    }
                                    pageURL += page[k];
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
//                            Console.WriteLine();
                            isNotPageUrlCompleted = false;
                            break;
                        }
                       
                    }
                }     
            }
            return pageURLList;
        }
    }
}
