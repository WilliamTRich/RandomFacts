using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomFacts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace RandomFacts.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //------------------------------------------------------------------------------------ LOGIN PAGE ------------------------------------------------------------------------------------

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher
                .HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session
                .SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Facts");
        }
        else
        {
            return View("Index");
        }
    }


    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser loginUser)
    {
        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users
                .FirstOrDefault(u => u.Email == loginUser.LEmail);
            if (userInDb == null)
            {
                ModelState
                    .AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher
                .VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LPassword);
            if (result == 0)
            {
                ModelState
                    .AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            else
            {
                HttpContext.Session
                    .SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Facts");
            }
        }
        else
        {
            return View("Index");
        }
    }

    [SessionCheck]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    //------------------------------------------------------------------------------------ FACTS PAGE ------------------------------------------------------------------------------------

    [SessionCheck]
    [HttpGet("facts")]
    public async Task<IActionResult> Facts()
    {
        //Facts Pull
        string apiUrl = "https://api.api-ninjas.com/v1/facts?limit=20";
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders
            .Add("X-Api-Key", "q6DsxloyRp9/RgDE5iR99g==kaAYSF5VIE3HlI62");
        string apiResponse = await httpClient
            .GetStringAsync(apiUrl);
        List<FactModel>? facts = JsonSerializer
            .Deserialize<List<FactModel>>(apiResponse);

        //User Pull
        int? userId = HttpContext.Session.GetInt32("UserId");
        User? user = _context.Users
            .FirstOrDefault(u => u.UserId == userId);

        //ViewModel
        MyViewModel viewModel = new MyViewModel
        {
            Facts = facts,
            User = user
        };
        return View("Facts", viewModel);
    }

    [HttpPost("fact/save")]
    public IActionResult SaveFact(MyViewModel viewModel)
    {
        //Save Fact to database
        _context.Add(viewModel.Fact);
        _context.SaveChanges();

        //Save the Association
        Association? newAssociation = new Association
        {
            UserId = viewModel.User.UserId,
            FactId = viewModel.Fact.FactId
        };
        _context.Add(newAssociation);
        _context.SaveChanges();

        //Return to Facts Page
        return RedirectToAction("Facts");
    }

    //------------------------------------------------------------------------------------ USER INFO PAGE ------------------------------------------------------------------------------------

    [SessionCheck]
    [HttpGet("user/{id}")]
    public IActionResult UserInfo()
    {
        //Grab User Data
        int? userId = HttpContext.Session.GetInt32("UserId");
        User? user = _context.Users
            .Include(u => u.Associations)
            .ThenInclude(u => u.Fact)
            .FirstOrDefault(u => u.UserId == userId);

        //Save info into ViewModel
        MyViewModel viewModel = new MyViewModel
        {
            User = user
        };
        return View("AccountInfo", viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}