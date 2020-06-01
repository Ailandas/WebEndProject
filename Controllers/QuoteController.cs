using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using WebEndProject.Models;
using WebGrease.Css.Extensions;

namespace WebEndProject.Controllers
{
    public class QuoteController : ApiController
    {
        ////////////////custom //////////////////////////////////
        [Route("api/quotedictionary/{category}")]
        public IHttpActionResult Get(string category)
        {
            
             bool existance=Models.SqlLite.CheckIfCategoryEgzists(category);
             if (existance == true)
             {

                List<string> cachingList = new List<string>();
                cachingList = (List<string>)MemoryCacher.GetValue("category");
                if (cachingList != null)
                {
                    cachingList.Add("Extra");
                    return Ok(cachingList);
                }
                else
                {
                    DateTime now = DateTime.Now;
                    int id = Models.TimeDifferences.GetDayOfTimeValue(now);

                    Models.SingleWord singleWord = Models.SqlLite.GetSingleWord(id, category);

                    ExternalAPI.Dictionary fetchWord = new ExternalAPI.Dictionary(singleWord.GetWord());//gauna dictionary pagal keyword

                    ExternalAPI._150000quotes fetchQuote = new ExternalAPI._150000quotes(category);//gauna quote pagal keyword

                    ExternalAPI.Translator fetchTranslation = new ExternalAPI.Translator(fetchQuote.quote.Message);


                    List<string> TempCachingList = new List<string>();
                    TempCachingList.Add(fetchQuote.quote.Message);
                    TempCachingList.Add(fetchTranslation.translatedText.ToString());
                    TempCachingList.Add(fetchWord.moreWords);
                    MemoryCacher.Add("category", TempCachingList, DateTimeOffset.UtcNow.AddMinutes(1));
                    return Ok(fetchQuote.quote.Message + " => " + fetchTranslation.translatedText.ToString() + " => " + fetchWord.moreWords);
                }
             }
             else
             {
                 return NotFound();
             }
            
        }
      

        //http://localhost:58065/api/quoteDictionary?kategorija=kategorija&zodis=zodis&laikas=diena Patalpinti nauja
        [Route("api/quotedictionary")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromUri] string kategorija, [FromUri] string zodis, [FromUri] string laikas) //Prideda nauja zodi i duombaze
        {
            
            try
             {
                 if(laikas.Contains("rytas") || laikas.Contains("diena") || laikas.Contains("naktis"))
                 {
                     if (laikas.Contains("rytas"))
                     {

                        Models.SqlLite.InsertToDatabase(kategorija, zodis, 0);
                        return Ok(200);
                     }
                     else if(laikas.Contains("diena"))
                     {
                         Models.SqlLite.InsertToDatabase(kategorija, zodis, 1);

                         return Ok(200);
                     }
                     else
                     {
                         Models.SqlLite.InsertToDatabase(kategorija, zodis, 2);

                         return Ok(200);
                     }

                 }
                 else
                 {
                     return NotFound();
                 }

             }
             catch(Exception exc)
             {
                 return NotFound();
             }



        }

       
        [Route("api/quotedictionary/categories")]
        [System.Web.Http.HttpGet]
        
        public string GetCategories() //Grazins visa sarasa kategoriju su reiksmemis
        {
            string baseUrl = (Url.Request.RequestUri.GetComponents(
                    UriComponents.SchemeAndServer, UriFormat.Unescaped).TrimEnd('/')
                 + System.Web.HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

             List<Category> cachingList = new List<Category>();
             cachingList = (List<Category>)MemoryCacher.GetValue("categories");

             if (cachingList != null)
             {

                 return Newtonsoft.Json.JsonConvert.SerializeObject(cachingList).ToString();
            }
             else
             {
                List<Category> categories = Models.SqlLite.GetCategories();
                for (int i=0; i<categories.Count; i++)
                {
                    string linkDelete = "DELETE: "+baseUrl + $"/api/quotedictionary/categories/{categories[i].GetName()}";
                    string linkLookup = "GET self : " + baseUrl + $"/api/quotedictionary/categories/{categories[i].GetName()}";
                    List<string> tempLinks = new List<string>();
                    tempLinks.Add(linkDelete);
                    tempLinks.Add(linkLookup);
                    categories[i].SetLinks(tempLinks);
                }
                
                MemoryCacher.Add("categories", categories, DateTimeOffset.UtcNow.AddMinutes(1));
                
                return Newtonsoft.Json.JsonConvert.SerializeObject(categories).ToString(); 
             }
            

            

        }

        [Route("api/quotedictionary/categories/{category}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCategoryEntries(string category) // Grazins visas reiksmes is kategorijos
        {
            
           
                List<SingleWord> cachingList = new List<SingleWord>();
                cachingList = (List<SingleWord>)MemoryCacher.GetValue("CategoryEntries");
                if (cachingList != null)
                {
                    return Ok(cachingList);
                }
                else
                {
                    string baseUrl = (Url.Request.RequestUri.GetComponents(
                     UriComponents.SchemeAndServer, UriFormat.Unescaped).TrimEnd('/')
                  + System.Web.HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

                List<SingleWord> SingleCategory = Models.SqlLite.GetEntriesByCategory(category);
                    for(int i=0; i < SingleCategory.Count; i++)
                    {
                    string linkDelete = "DELETE: " + baseUrl + $"/api/quotedictionary/words/{SingleCategory[i].Word}";
                    string linkLookup = "GET self : " + baseUrl + $"/api/quotedictionary/words/{SingleCategory[i].Word}";
                    SingleCategory[i].links.Add(linkDelete);
                    SingleCategory[i].links.Add(linkLookup);
                }
                    MemoryCacher.Add("CategoryEntries", SingleCategory, DateTimeOffset.UtcNow.AddMinutes(1));
                    
                    return Ok(SingleCategory);
                }
           
           

        }

        [Route("api/quotedictionary/categories/{category}")]
        [System.Web.Http.HttpDelete]
        
        public IHttpActionResult DeleteCategory(string category) //Delete category
        {
            bool deleted = Models.SqlLite.DeleteCategory(category);
            if (deleted == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/quotedictionary/words/{word}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteWord(string word) //Delete word
        {
            bool deleted = Models.SqlLite.DeleteWord(word);
            if (deleted == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/quotedictionary/words/{word}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSingleWord(string word) //Gauna single word
        {

            ExternalAPI.Dictionary dictionary = (ExternalAPI.Dictionary)MemoryCacher.GetValue("SingleWord");

            if (dictionary != null)
            {
                return Ok(dictionary.moreWords.ToString());
            }
            else
            {
                ExternalAPI.Dictionary fetchWord = new ExternalAPI.Dictionary(word);
                MemoryCacher.Add("SingleWord", fetchWord, DateTimeOffset.UtcNow.AddMinutes(1));
                return Ok(fetchWord.moreWords.ToString());
            }

              
        }
    }
}
