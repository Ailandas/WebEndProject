using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class GetWordsFromCategorySteps
    {
        public HttpClient client;
        public string baseAddress;
        public HttpResponseMessage response;

        [Given(@"I have a link")]
        public void GivenIHaveALink()
        {
            client = new HttpClient();
        }
        
        [When(@"When I enter a category")]
        public void WhenWhenIEnterACategory()
        {
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/" + "Crime";
        }
        
        [When(@"Press enter")]
        public void WhenPressEnter()
        {
             response = client.GetAsync(baseAddress).Result;
        }
        
        [Then(@"the result should be visible")]
        public async void ThenTheResultShouldBeOnTheScreen()
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
