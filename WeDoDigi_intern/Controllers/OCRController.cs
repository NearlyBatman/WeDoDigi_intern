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
using System.Drawing.Imaging;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net.Http;

namespace WeDoDigi_intern.Controllers
{
    public class OCRController : Controller
    {
        // Start Viewet
        public IActionResult Index(IFormFile file)
        {
            // Tager filen oploadet laver den til en stream, til BitArray
            // og så til en base 64 string jeg giver videre til Viewet
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                ViewBag.foo = string.Format($"data:image/jpg;base64,{s}");
            }

            return View("OCRTesting");
        }

        [HttpPost]
        public JsonResult AddImage(string imgString)
        {
            CountHolder res = Newtonsoft.Json.JsonConvert.DeserializeObject<CountHolder>(imgString);

            string message = "Please try again";

            var data = Regex.Replace(res.imageString, " ", "+");

            var bString = Convert.FromBase64String(res.imageString);

            var mStream = new MemoryStream(bString);

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = Path.Combine(path, "tessdata");
            path = path.Replace("file:\\", "");

            using (var engine = new TesseractEngine(path, "dan", EngineMode.Default))
            {
                using (var image = new Bitmap(mStream))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            data = page.GetText();
                        }
                    }
                }
            }


            // Switch for at se hvor vi er nået i rækken
            switch (res.intCounter)
            {
                case 1:
                    TempData["Title"] = data as string;
                    message = "Mark up description";
                    break;

                case 2:
                    TempData["Description"] = data as string;
                    message = "Mark up ingredients";
                    break;

                case 3:
                    TempData["Ingredients"] = data as string;
                    message = "Mark up steps";
                    break;

                case 4:
                    TempData["Steps"] = data as string;
                    break;

                case int n when (n >= 5 || n <= 0):
                    break;
            }
            return Json(new { success = true, responseText = message }) ;
        }

        [HttpPost]
        public IActionResult AddOCRToRec()
        {
            // Laver et RecipeDb objekt med de værdier fundet igennem OCR
            RecipeDb recipe = new RecipeDb(TempData["Title"] as string,
                TempData["Description"] as string,
                TempData["Ingredients"] as string,
                TempData["Steps"] as string);
            // Sender os til viewet for at lave recipes med objektet
            return View("../Recipe/Addrecipe", recipe);
        }
    }
}
