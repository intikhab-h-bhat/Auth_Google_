using Auth_Google.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Auth_Google.Controllers
{
    public class SafeChatDemoController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7214/api");

        private readonly HttpClient _httpClient;

        public SafeChatDemoController()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = baseAddress;

        }


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
           string answer = string.Empty;
            string question = "I will Kill You";

            //StringContent content=new StringContent(question);

           // List<AnswerViewModel> answerList = new List<AnswerViewModel>();

            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/SafeChat/CheckSafety/" + question).Result;
            
            if(response.IsSuccessStatusCode) {

                 string  data = response.Content.ReadAsStringAsync().Result;
                //answer = JsonConvert.DeserializeObject<string>(data);
                answer = data;


               // answerList = JsonConvert.DeserializeObject<List<AnswerViewModel>>(result);
               

            }
            
            return View(nameof(Index), answer);
        }
    }
}
