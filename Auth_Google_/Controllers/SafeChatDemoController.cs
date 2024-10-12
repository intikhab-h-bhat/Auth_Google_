using Auth_Google.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Auth_Google.Controllers
{
    public class SafeChatDemoController : Controller
    {
        //Uri baseAddress = new Uri("https://localhost:7214/api");
        Uri baseAddress = new Uri("https://safechatapi.azurewebsites.net/api");
      
        private readonly HttpClient _httpClient;

        public SafeChatDemoController()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = baseAddress;
           
            

        }


        [HttpGet]
        [Authorize]
        public IActionResult AskQuestion()
        {


            return View(nameof(Index));

        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View(new AnswerViewModel());
        //}

        [HttpPost]
        [Authorize]
        //public IActionResult Index(string askquestion,string respAnswer,string secureKey)
        public IActionResult Index(string askquestion, string respAnswer)
        {
            respAnswer=string.Empty;
            string answer = string.Empty;
            string question = askquestion; //"I will Kill You";
                                           // List<string> message = new List<string>();
                                           //StringContent content=new StringContent(question);

            // List<AnswerViewModel> answerList = new List<AnswerViewModel>();
            // Add the API key to the default request headers
            _httpClient.DefaultRequestHeaders.Add("x-Api-Key", "F526D48AF3D94658886B6F7198FEF2DC");
           // _httpClient.DefaultRequestHeaders.Add("x-Api-Key", secureKey);

            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/SafeChat/CheckSafety/" + question).Result;
            
            if(response.IsSuccessStatusCode) {

                 string  data = response.Content.ReadAsStringAsync().Result;
                //answer = JsonConvert.DeserializeObject<string>(data);
                answer = data;
                  
               // answerList = JsonConvert.DeserializeObject<List<AnswerViewModel>>(result);
               

            }
            else
            {
              answer = response.Content.ReadAsStringAsync().Result;
            }
            
            return View(nameof(Index), answer);
        }
    }
}
