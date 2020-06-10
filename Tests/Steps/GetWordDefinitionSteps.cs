using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class GetWordDefinitionSteps
    {
        public HttpClient client;
        public string baseAddress;
        public HttpResponseMessage response;
        public string customString = null;



        [Given(@"I have a link to get a word")]
        public void GivenIHaveALinkToGetAWord()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/words/" + "Life";
        }
        
        [When(@"I press enter button")]
        public void WhenIPressEnterButton()
        {
            response = client.GetAsync(baseAddress).Result;
        }
        
        [Then(@"Response is returned")]
        public async void ThenResponseIsReturned()
        {
            customString = await response.Content.ReadAsStringAsync();
            if (customString == null)
                throw new Exception("Error");
        }

        [Then(@"I get word with its definition")]
        public void ThenIGetWordWithItsDefinition()
        {
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                //Console.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");
                // Get the response
                //Console.WriteLine("Your response data is: " + customString);
                if (customString.Contains("Word") && customString.Contains("HasCategories") && customString.Contains("moreWords") && customString.Contains("dictionary"))
                { }
                else
                {
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
