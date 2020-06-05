using Newtonsoft.Json;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class CategorySteps
    {
        public HttpClient client;
        [Given(@"link")]
       
        public void GivenLink()
        {
             client = new HttpClient();
            
                
                


        }

        [Then(@"It I get a list of categories")]
        public async void ThenItIGetAListOfCategories()
        {
            string baseAddress = "http://localhost:58065/api/quotedictionary/categories";
            HttpResponseMessage response = client.GetAsync(baseAddress).Result;
            if (response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                // Get the response
                var customerJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + customerJsonString);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
