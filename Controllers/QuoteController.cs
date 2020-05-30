using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebEndProject.Controllers
{
    public class QuoteController : ApiController
    {
        ////////////////custom //////////////////////////////////
        [Route("api/quoteDictionary/{keyword}")]
        public string Get(string keyword)
        {
            ExternalAPI.Dictionary fetchWord = new ExternalAPI.Dictionary(keyword);//gauna dictionary pagal keyword

            ExternalAPI._150000quotes fetchQuote = new ExternalAPI._150000quotes(keyword);//gauna quote pagal keyword

            ExternalAPI.Translator fetchTranslation = new ExternalAPI.Translator(fetchQuote.quote.Message);

            return fetchTranslation.translatedText;
            //return fetchQuote.jsonAsString +"---------------------------------"+fetchWord.moreWords;
        }
        ////////////////////////////////////////////////////////


        [Route("api/quoteDictionary/{category}/{word}")]
        
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post(string category, string word) //Prideda nauja zodi i duombaze
        {
            
            Models.SqlLite.InsertToDatabase(category, word);

            return Ok(200);
        }

        [Route("api/quoteDictionary/categories")]

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCategories() //Grazins visa sarasa kategoriju su reiksmemis
        {

          

            return Ok(200);
        }

        // GET: api/Quote
        public IEnumerable<string> Get()
        {
            return new string[] { "Default quote controller", "value2" };
        }

        //GET: api/Quote/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Quote
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Quote/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Quote/5
        public void Delete(int id)
        {
        }
    }
}
