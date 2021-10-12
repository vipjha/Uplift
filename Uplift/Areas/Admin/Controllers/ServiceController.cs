using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModel;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ServiceVM ServiceVM { get; set; }

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ServiceVM = new ServiceVM()
            {
                Service = new Models.Service(),
                CtegoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
            };
            if (id != null)
            {
                ServiceVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
            }
            return View(ServiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ServiceVM ServiceVM)
        {
            if (ModelState.IsValid)
            {
                string webRoothPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (ServiceVM.Service.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRoothPath, @"image\services");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    ServiceVM.Service.ImageUrl = @"\images\services\" + fileName + extension;

                    _unitOfWork.Service.Add(ServiceVM.Service);
                }
                else
                {
                    //Edit Service
                    var serviceFromDb = _unitOfWork.Service.Get(ServiceVM.Service.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRoothPath, @"image\services");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRoothPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        ServiceVM.Service.ImageUrl = @"\images\services\" + fileName + extension_new;
                    }
                    else
                    {
                        ServiceVM.Service.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.Service.Update(ServiceVM.Service);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ServiceVM.CtegoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                ServiceVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown();
                return View(ServiceVM);
            }
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.Service.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return Json(new { success = false, message = "Error While deleting." });
            }

            _unitOfWork.Service.Remove(serviceFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfuly." });
        }


        /* [HttpDelete]
         public IActionResult Delete(int id)
         {
             var serviceFromDb = _unitOfWork.Service.Get(id);
             string webRoothPath = _hostEnvironment.WebRootPath;
             var imagePath = Path.Combine(webRoothPath, serviceFromDb.ImageUrl.TrimStart('\\'));
             if (System.IO.File.Exists(imagePath))
             {
                 System.IO.File.Delete(imagePath);
             }

             if (serviceFromDb == null)
             {
                 return Json(new { success = false, message = "Error while deleting." });
             }
             _unitOfWork.Service.Remove(serviceFromDb);
             _unitOfWork.Save();
             return Json(new { success = true, message = "Deleted Sucessfuly." });

         }*/


        #region API Calls

        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Frequency") });
        }

        #endregion

    }
}
