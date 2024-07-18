using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourismMVC.Controllers
{
    [Authorize]
    public class BaseControllerMVC : Controller
    {
    }
}
