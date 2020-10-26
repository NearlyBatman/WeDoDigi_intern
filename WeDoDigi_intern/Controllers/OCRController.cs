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
            //Tager imod en base 64 string i json formaet og laver den til en model
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<CountHolder>(imgString);
            // Messeage er beskeden vi sender til viewet
            string message = "Please try again";
            // Fjerne unødvendige tegn i base64 stringen
            var data = Regex.Replace(res.imageString, " ", "+");
            // Omdanner fra base64 til byte array
            var bString = Convert.FromBase64String(res.imageString);
            // Laver arrayet til en stream
            var testing = new MemoryStream(bString);
            // Finder tesseract dataen
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = Path.Combine(path, "tessdata");
            path = path.Replace("file:\\", "");
            // Bruger dataen med dansk
            using (var engine = new TesseractEngine(path, "dan", EngineMode.Default))
            {
                // Laver streamen til et Bitmap
                using (var image = new Bitmap(testing))
                {
                    // Konvertere Bitmappet til Pix for at Tesseract kan læse det
                    using (var pix = PixConverter.ToPix(image))
                    {
                        // Giver et resultat på hvad der står på det markeret billede
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
                // Gemmer dataerne forksellige steder alt efter hvad markeringen skulle være
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
            // Returnere Json
            return Json(new { success = true, responseText = message }) ;
        }

        [HttpPost]
        public IActionResult AddOCRToRec()
        {
            // Laver et RecipeDb objekt med de værdier fundet igennem OCR
            RecipeDb recipe = new RecipeDb(TempData["Title"] as string, TempData["Description"] as string, TempData["Ingredients"] as string, TempData["Steps"] as string);

            return View("../Recipe/Addrecipe", recipe);
        }
    }
}
