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
        
        [Then(@"I get a list of categories")]
        public async void ThenIGetAListOfCategories()
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                // Get the response
                var customerJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + customerJsonString);
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}
