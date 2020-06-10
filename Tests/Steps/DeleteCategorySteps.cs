using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class DeleteCategorySteps
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



        [Given(@"I have a category i want to delete")]
        public void GivenIHaveACategoryIWantToDelete()
        {
            PostNew();
        }
        
        [Given(@"I have a link to delete the category")]
        public void GivenIHaveALinkToDeleteTheCategory()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/TestDelete";
        }
        
        [When(@"I proceed to delete")]
        public void WhenIProceedToDelete()
        {
            result = client.DeleteAsync(baseAddress).Result;
        }
        
        [Then(@"Server returns status OK")]
        public void ThenServerReturnsStatusOK()
        {
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error");
        }
        
        [Then(@"The record is deleted")]
        public void ThenTheRecordIsDeleted()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/" + temp.kategorija;
            response = client.GetAsync(baseAddress).Result;
            Temp();
            Console.WriteLine("Response: " + response.StatusCode.ToString());
            if (response.IsSuccessStatusCode)
                throw new Exception("Error");
        }

        private async void Temp()
        {
            customString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Content: " + customString);
            if (customString != null && customString!="[]")
                throw new Exception("Content: " + customString);
        }
    }
}
