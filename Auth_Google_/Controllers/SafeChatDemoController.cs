using Auth_Google.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        public IActionResult AskQuestion()
        {


            return View(nameof(Index));

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AnswerViewModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(string askquestion)

        {
            string answer = string.Empty;
            string question = askquestion; //"I will Kill You";
           // List<string> message = new List<string>();
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
