using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class GetCategoriesSteps
    {

        public HttpClient client;
        public string baseAddress;
        public HttpResponseMessage response;
        public string customerJsonString = null;

        [Given(@"I have a link to all categories")]
        public void GivenIHaveALinkToAllCategories()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/";
        }
        
        [When(@"I press enter")]
        public void WhenIPressEnter()
        {
            response = client.GetAsync(baseAddress).Result;
        }

        [Then(@"Result should be returned")]
        public async void ThenResultShouldBeReturned()
        {
            customerJsonString = await response.Content.ReadAsStringAsync();
            if (customerJsonString == null)
                throw new Exception("Error");
        }


        [Then(@"I get a list of categories")]
        public  void ThenIGetAListOfCategories()
        {
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                //Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                //// Get the response
                Console.WriteLine("Your response data is: " + customerJsonString);
                if (customerJsonString.Contains("name") && customerJsonString.Contains("PUT") && customerJsonString.Contains("GET") && customerJsonString.Contains("DELETE")) { }
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
