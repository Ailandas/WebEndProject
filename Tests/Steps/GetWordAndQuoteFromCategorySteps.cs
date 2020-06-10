using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class GetWordAndQuoteFromCategorySteps
    {
        public HttpClient client;
        public string baseAddress;
        public HttpResponseMessage response;
        public string customerJsonString = null;




        [Given(@"I have a link with category")]
        public void GivenIHaveALinkWithCategory()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/Kitchen/";
        }
        
        [When(@"I proceed")]
        public void WhenIProceed()
        {
            response = client.GetAsync(baseAddress).Result;
        }
        
        [Then(@"Result of the word should be returned")]
        public async void ThenResultOfTheWordShouldBeReturned()
        {
            customerJsonString = await response.Content.ReadAsStringAsync();
            if (customerJsonString == null)
                throw new Exception("Error");
        }
        
        [Then(@"I get a word and a quote")]
        public void ThenIGetAWordAndAQuote()
        {
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                //Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                //// Get the response
                //Console.WriteLine("Your response data is: " + customerJsonString);
                if (customerJsonString.Contains("word") && customerJsonString.Contains("definition") && customerJsonString.Contains("typeOf")) { }
                else
                {
                    Assert.Fail();
                    throw new Exception("Error");
                }
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}
