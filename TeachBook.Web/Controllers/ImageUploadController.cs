using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IUploadImg uploadImg;

        public ImageUploadController(IUploadImg uploadImg)
        {
            this.uploadImg = uploadImg;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var FileUpload = await uploadImg.UploadImgAsync(file);
            if (FileUpload == null) { return Problem("Something Went Wrong", null, (int)HttpStatusCode.InternalServerError); }
            return new JsonResult(new {link = FileUpload});
        }
        //public async Task<IActionResult> UploadAsyncForFlora(IFormFile file)
        //{
        //    var link = await uploadImg.UploadImgAsync(file);
        //    if (link == null) { return Problem("Something Went Wrong", null, (int)HttpStatusCode.InternalServerError); }
        //    return new JsonResult(new { link });
        //}
    }
}
