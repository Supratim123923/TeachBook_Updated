using Azure;
using Microsoft.AspNetCore.Mvc;
using TeachBook.Web.Data;
using TeachBook.Web.Models.Domain_Model;
using TeachBook.Web.Models.ViewModels;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    public class AdminTagsController : Controller
    {
       // private readonly TeachBookDBContext _teachBookDBContext;
        private readonly ITagRepository tagRepository;

        //public AdminTagsController(TeachBookDBContext teachBookDBContext)
        //{
        //    this._teachBookDBContext = teachBookDBContext;
        //}
        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTag _addtag)
        {
            var Tag = new Tag { Name = _addtag.Name, DisplayName = _addtag.DisplayName };
            await tagRepository.AddTagAsync(Tag);
            return RedirectToAction("List");

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Alltags = await tagRepository.GetAllTagsAsync();
            return View(Alltags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var Tag = await tagRepository.GetTagAsync(id);
            if (Tag != null)
            {
                var newTag = new EditTag { Name = Tag.Name, DisplayName = Tag.DisplayName, Id = Tag.Id };
                return View(newTag);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTag _Edittag)
        {
            var Tag = new Tag { Name = _Edittag.Name, DisplayName = _Edittag.DisplayName, Id = _Edittag.Id };
            //var FindTag = await _teachBookDBContext.Tags.FindAsync(Tag.Id);
            //if (FindTag != null)
            //{
            //    FindTag.DisplayName = Tag.DisplayName;
            //    FindTag.Name = Tag.Name;
            //    await _teachBookDBContext.SaveChangesAsync();
            //    return RedirectToAction("Edit", new { id = _Edittag.Id });
            //}
            var Isupdated = await tagRepository.UpdateTagAsync(Tag);
            if (Isupdated != null)
            {
            }
            else
            {

            }

            return RedirectToAction("Edit", new { id = _Edittag.Id });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteTag(EditTag Deletedata)
        {
            var Tag = new Tag { Name = Deletedata.Name, DisplayName = Deletedata.DisplayName, Id = Deletedata.Id };
           var Isdeleted =  await tagRepository.DeleteTagAsync(Tag.Id);
            if (Isdeleted !=null)
            {
            return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { Deletedata.Id });

        }
    }
}
