using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Tesseract;
using WeDoDigi_intern.Models;

namespace WeDoDigi_intern.Controllers
{
    public class OCRController : Controller
    {
        public IActionResult Index(IFormFile file)
        {

            byte[] bitte;

            BinaryReader br = new BinaryReader(file.OpenReadStream());
            bitte = br.ReadBytes((int)file.OpenReadStream().Length);
            var meh = Convert.ToBase64String(bitte);
            ViewBag.foo = string.Format($"data:image/jpg;base64,{meh}");


            return View("OCRTesting");
        }

        [HttpPost]
        public JsonResult AddImage(string data)
        {
            string message = "Didn't work, please try again";
            int i = 1;
            switch (i)
            {
                case 1:
                   // TempData["Title"] = imageData;
                    message = "Mark up description";
                    break;

                case 2:
                   // TempData["Description"] = imageData;
                    message = "Mark up ingredients";
                    break;

                case 3:
                   // TempData["Ingredients"] = imageData;
                    message = "Mark up steps";
                    break;

                case 4:
                  //  TempData["Steps"] = imageData;
                    break;

                case int n when (n >= 5):
                    break;
            }

            return Json(new { success = true});

        }
        [HttpPost]
        public IActionResult OCRResult(IFormFile file)
        {

            /*
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = Path.Combine(path, "tessdata");
            path = path.Replace("file:\\", "");
            if (file == null || file.Length == 0)
            {
                ViewBag.Result = true;
                ViewBag.res = "File not found";
                return View("OCR");
            }

            using (var engine = new TesseractEngine(path, "dan", EngineMode.Default))
            {
                using (var image = new Bitmap(file.OpenReadStream()))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            ViewBag.Result = true;
                            ViewBag.res = page.GetText();
                            ViewBag.mean = String.Format("{0:p}", page.GetMeanConfidence());
                            return View("OCR");
                        }
                    }
                }
            }
            */
            return View();
        }
    }
}
