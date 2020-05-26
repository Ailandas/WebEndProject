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
        [Route("api/quote/{keyword}")]
        public string Get(string keyword)
        {
            ExternalAPI._150000quotes fetchQuote = new ExternalAPI._150000quotes(keyword);
            return fetchQuote.jsonAsString;
        }
        ////////////////////////////////////////////////////////


        


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
