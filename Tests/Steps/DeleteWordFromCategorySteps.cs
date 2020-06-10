using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class DeleteWordFromCategorySteps
    {
        public HttpClient client;
        public string baseAddress;
        public string customString = null;
        public string content;
        public HttpResponseMessage result;
        public HttpResponseMessage response;
        public TempObj1 temp;
        public class TempObj1
        {
            public string kategorija = "TestDelete";
            public string zodis = "AAA";
            public string laikas = "Diena";
        }
        private void PostNew()
        {
            temp = new TempObj1();
            var content1 = JsonConvert.SerializeObject(temp);
            var client1 = new HttpClient();
            var baseAddress1 = "http://localhost:58065/api/quotedictionary/";
            var ccontent1 = new StringContent(content1.ToString(), Encoding.UTF8, "application/json");
            var result1 = client1.PostAsync(baseAddress1, ccontent1).Result;
            Console.WriteLine("RESPONSE: " + result1.IsSuccessStatusCode.ToString());
            if (!result1.IsSuccessStatusCode)
                throw new Exception("Error");
        }

        [Given(@"I have a word I want to delete")]
        public void GivenIHaveAWordIWantToDelete()
        {
            PostNew();
        }
        
        [Given(@"I have a link to delete the word")]
        public void GivenIHaveALinkToDeleteTheWord()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/words/"+ temp.zodis;
        }
        
        [When(@"I press delete")]
        public void WhenIPressDelete()
        {
            result = client.DeleteAsync(baseAddress).Result;
        }
        
        [Then(@"The server informs me that the code is OK")]
        public void ThenTheServerInformsMeThatTheCodeIsOK()
        {
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error");
        }
        
        [Then(@"The word is deleted from the database")]
        public void ThenTheWordIsDeletedFromTheDatabase()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/" + temp.kategorija;
            response = client.GetAsync(baseAddress).Result;
            Temp();
            if (customString.Contains(temp.zodis))
            {
                throw new Exception("Error");
            }
            
        }
        private async void Temp()
        {
            customString = await response.Content.ReadAsStringAsync();

        }
    }
}
