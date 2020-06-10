using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class UpdateWordFromCategorySteps
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

        [Given(@"I have word I would like to update")]
        public void GivenIHaveWordIWouldLikeToUpdate()
        {
            temp = new TempObj1();
            content = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
        }
        
        [Given(@"I have a link to update the word")]
        public void GivenIHaveALinkToUpdateTheWord()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/words/";
        }
        
        [When(@"I press submit my request to update")]
        public void WhenIPressSubmitMyRequestToUpdate()
        {
            var ccontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");
            result = client.PutAsync(baseAddress, ccontent).Result;
        }
        
        [Then(@"the server should return a response")]
        public void ThenTheServerShouldReturnAResponse()
        {
            Console.WriteLine("RESPONSE: " + result.IsSuccessStatusCode.ToString());
            if (!result.IsSuccessStatusCode)
                throw new Exception("Error Response");
        }
        
        [Then(@"the word is updated")]
        public void ThenTheWordIsUpdated()
        {
            client = new HttpClient();
            baseAddress = "http://localhost:58065/api/quotedictionary/categories/Test";
            response = client.GetAsync(baseAddress).Result;
            Temp();
            if (!customString.Contains(temp.newData))
            {
                throw new Exception("Error");
            }
            UnUpdate(); //Is naujo atsato
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
            string newBaseAdress= "http://localhost:58065/api/quotedictionary/words/";
            result = client.PutAsync(newBaseAdress, ccontent).Result;
        }
    }
}
