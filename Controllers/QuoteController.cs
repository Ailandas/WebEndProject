using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebEndProject.Controllers
{
    public class QuoteController : ApiController
    {
        ////////////////custom //////////////////////////////////
        [Route("api/word/{keyword}")]
        public string Get(string keyword)
        {
            ExternalAPI.Dictionary fetchWord = new ExternalAPI.Dictionary(keyword);//gauna dictionary pagal keyword

            ExternalAPI._150000quotes fetchQuote = new ExternalAPI._150000quotes(keyword);//gauna quote pagal keyword

            ExternalAPI.Translator fetchTranslation = new ExternalAPI.Translator(fetchQuote.quote.Message);

            return fetchTranslation.translatedText;
            //return fetchQuote.jsonAsString +"---------------------------------"+fetchWord.moreWords;
        }
        ////////////////////////////////////////////////////////





        // GET: api/Quote
        [Route("api/word/{category}/{word}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Post(string category, string word) //Not found, neranda duomenu bazes
        {
            Models.SqlLite.InsertToDatabase(category, word);
            //string Path = HttpContext.Current.Server.MapPath("~");
            return "success";
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
