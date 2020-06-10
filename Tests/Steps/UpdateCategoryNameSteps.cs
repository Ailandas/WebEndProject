using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class UpdateCategoryNameSteps
    {
        public TempObj1 temp;
        public HttpClient client;
        public string baseAddress;
        public HttpResponseMessage result;
        public string content;
        public HttpResponseMessage response;
        public string customString = null;
        public class TempObj1
        {
            public string oldData = "Test";
            public string newData = "TestUpdate";
        }

        [Given(@"I have a new category name")]
        public void GivenIHaveANewCategoryName()
        {
            temp = new TempObj1();
            content = JsonConvert.SerializeObject(temp);
        }
        
        [Given(@"I have a link to update category")]
        public void GivenIHaveALinkToUpdateCategory()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/";
           
        }
        
        [When(@"I submit my update")]
        public void WhenISubmitMyUpdate()
        {
            var ccontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            result = client.PutAsync(baseAddress, ccontent).Result;
        }
        
        [Then(@"The server informs me that the status is ok")]
        public void ThenTheServerInformsMeThatTheStatusIs()
        {
            Console.WriteLine("RESPONSE: " + result.IsSuccessStatusCode.ToString());
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error Response");
        }
        
        [Then(@"The category name has changed")]
        public void ThenTheCategoryNameHasChanged()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/";
            response = client.GetAsync(baseAddress).Result;
            Temp();
            if (!customString.Contains(temp.newData))
            {
                throw new Exception("Error");
            }
            UnUpdate(); //Is naujo atsato reiksme// Reversina update'a
        }
        private async void Temp()
        {
            customString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Content: " + customString);
            if (customString != null && customString != "[]")
                throw new Exception("Content: " + customString);
        }
        private void UnUpdate()
        {
            TempObj1 oldObject = new TempObj1();
            string oldData = oldObject.oldData;
            string newData = oldObject.newData;
            oldObject.newData = oldData;
            oldObject.oldData = newData;
            string contentas = JsonConvert.SerializeObject(oldObject);
            var ccontent = new StringContent(contentas.ToString(), Encoding.UTF8, "application/json");
            result = client.PutAsync(baseAddress, ccontent).Result;
        }
    }
}
