using Microsoft.AspNetCore.Mvc;

namespace MiniPinterest.Web.Controllers
{
    public class BoardsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
