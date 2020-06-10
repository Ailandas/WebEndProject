using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class PostNewRecordSteps
    {
        public HttpClient client;
        public string baseAddress;
        public string customString = null;
        public string content;
        public HttpResponseMessage result;
        public HttpResponseMessage response;
        public string customString2 = null;

        public class TempObj
        {
            public string kategorija="TestPost";
            public string zodis="OOO";
            public string laikas = "Diena";
        }




        [Given(@"I have a word, its category and time")]
        public void GivenIHaveAWordItsCategoryAndTime()
        {
            var temp = new TempObj();
            content = JsonConvert.SerializeObject(temp);
            Console.WriteLine("DES: "+content);
        }
        
        [Given(@"I have a link to post")]
        public void GivenIHaveALinkToPost()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/";
        }
        
        [When(@"I submit the post")]
        public void WhenISubmitThePost()
        {
            var ccontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            result = client.PostAsync(baseAddress, ccontent).Result;
        }
        
        [Then(@"I get response from the server")]
        public void ThenIGetResponseFromTheServer()
        {
            Console.WriteLine("RESPONSE: "+ result.IsSuccessStatusCode.ToString());
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error Response");
        }
        
        [Then(@"The record is found in database")]
        public void ThenTheRecordIsFoundInDatabase()
        {

            var client2 = new HttpClient();

            var baseAddress2 = $"http://localhost:58065/api/quotedictionary/categories/TestPost";

            var response2 = client2.GetAsync(baseAddress2).Result;

            Asyncho(response2);

            if (response2.IsSuccessStatusCode)
            {
                Console.WriteLine("Stringas: " + customString2);
                if (customString2.Contains("OOO"))
                { }
                else
                {
                    Console.WriteLine("Necontainina");
                    throw new Exception("Error Contains");
                }
            }
            else
            {
                Console.WriteLine("Nesuccesful");
                throw new Exception("Error Response OK");
            }
        }

        private async void Asyncho(HttpResponseMessage response2)
        {
            customString2 = await response2.Content.ReadAsStringAsync();
            if (customString2 == null)
            {
                Console.WriteLine("Tuscias stringas");

                throw new Exception("Error String =null");
            }
        }
    }
}
