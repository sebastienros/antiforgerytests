using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.CommentViewModels;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        public CommentController()
        {
        }

        //
        // GET: /Comment
        [HttpGet]
        [ActionName("Index")]
        public IActionResult CreateComment()
        {
            return View(new CommentViewModel());
        }

        //
        // POST: /Comment
        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult PostComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save comment here
            }

            return View("PostComment", model);
        }
    }
}
