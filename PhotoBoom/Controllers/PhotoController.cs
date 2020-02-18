using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoBoom.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PhotoController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.webRootPath = _hostingEnvironment.WebRootPath;

            return View(DataSource.Instance.PhotoList.Where(x => !x.IsDeleted));
        }
        public async Task<IActionResult> Save(Models.PhotoModel photo, IFormFile image)
        {
            if (photo == null)
            {
                return NotFound("Photo not found.");
            }
            
            if (image.Length > 0)
            {
                var filePath = $"{_hostingEnvironment.WebRootPath}/Photos/{photo.Id}.jpg";
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.OpenReadStream().CopyToAsync(fileStream);
                }
                photo.TagList.ForEach(x => x = x.Trim().Trim('#'));
                DataSource.Instance.PhotoList.Add(photo);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Detail(string id)
        {
            var photo = DataSource.Instance.PhotoList
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (photo != null)
            {
                return Json(photo);
            }

            return NotFound("Photo not found.");
        }
        public IActionResult Delete(string id)
        {
            var photo = DataSource.Instance.PhotoList
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (photo != null)
            {
                photo.IsDeleted = true;
                return Ok(photo);
            }

            return NotFound("Photo not found.");
        }

        public IActionResult Image(string id)
        {
            var photo = DataSource.Instance.PhotoList
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (photo != null)
            {
                var path = $"{_hostingEnvironment.WebRootPath}/Photos/{id}.jpg";

                string contentRootPath = _hostingEnvironment.ContentRootPath;

                if (System.IO.File.Exists(path))
                {
                    return File(System.IO.File.ReadAllBytes(path), "image/jpeg");
                }
            }

            return NotFound("Photo not found.");
        }
    }
}
