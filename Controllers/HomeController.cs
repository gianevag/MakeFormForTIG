using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MakeFormForTIG.Models;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeFormForTIG.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration _configuration { get; }
        private readonly modelContext _context = null;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IOptions<Setting> settings, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = new modelContext(settings);
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new FormData();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(FormData model)
        {
            if (ModelState.IsValid)
            {
                //upload images
                UploadFiles(model);

                //Tranform data from FormData model to Jewelry Model
                var jewel = FormDataToJewelry(model);

                //instert to db
                _context.JewelriesLinq.InsertOne(jewel);

            }
            return RedirectToAction("GetAllJewelries");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AllJewelries()
        {
            var model = _context.JewelriesLinq.AsQueryable().Where(x => x.isFirstPage >= 0)
                                              .Select(x => new
                                              {
                                                  Id = x.Id,
                                                  JewelId = x.JewelryId,
                                                  Price = x.Price,
                                                  photo = x.first_thumb_photo,
                                                  Title = x.Title,
                                                  OrderInFirstPage = x.orderInFirstPage
                                              }).ToList();

            var a =
            new
            {
                data = model.Select(x =>
                {
                    return new
                    {
                        Id = x.Id.ToString(),
                        photo = x.photo,
                        price = x.Price,
                        title = x.Title,
                        jewelId = x.JewelId,
                        orderInFirstPage = x.OrderInFirstPage
                    };
                }).ToList()
            };

            return Json(a);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllJewelries()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet("home/Jewelry/{id}")]
        public IActionResult GetJewelry(string id)
        {
            var _jewel = _context.JewelriesLinq.AsQueryable().FirstOrDefault(x => x.Id == new ObjectId(id));
            var model = JewelryToFormData(_jewel);
            ViewData["_id"] = id;
            return View("Jewelry", model);
        }

        [Authorize]
        [HttpPost("Edit/{id}"), ActionName("Edit")]
        public IActionResult GetJewelry(string id, FormData model)
        {
            UploadFiles(model);

            var _jewel = FormDataToJewelry(model);
            _jewel.Id = ObjectId.Parse(id);

            var result = _context.JewelriesLinq.ReplaceOne(x => x.Id.Equals(_jewel.Id), _jewel, new UpdateOptions { IsUpsert = true });

            return RedirectToAction("GetAllJewelries");
        }

        [Authorize]
        [HttpGet("home/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _context.JewelriesLinq.DeleteOne(x => x.Id.Equals(ObjectId.Parse(id)));
            return RedirectToAction("GetAllJewelries");
        }


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Layout = "differtLayout";
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (LoginUser(loginModel.Username, loginModel.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username)
            };

                var userIdentity = new ClaimsIdentity(
                    claims, "cookie");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return Redirect("/");
            }
            return Redirect("Login");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }

        // FUNCTION AREA
        //save images to upload file path
        public void UploadFiles(FormData model)
        {
            Microsoft.AspNetCore.Http.IFormFile[] files =
                {model.first_thumb_file,model.second_thumb_file,model.yellowPhoto_file,model.whitePhoto_file,model.rosePhoto_file};

            foreach (var file in files)
            {
                if (file != null)
                {
                    var parsedContentDisposition =
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var filename = Path.Combine(_configuration.GetSection("UploadFilePath").Value, parsedContentDisposition.FileName.Trim('"'));
                    var hostingFilename = Path.Combine(_hostingEnvironment.WebRootPath, "images/shop", parsedContentDisposition.FileName.Trim('"'));
                    System.Console.WriteLine(file.Length);
                    //copy data to TIG file path
                    CopyData(file, filename);
                    //copy data to hosting inv
                    CopyData(file, hostingFilename);
                }
            }
        }

        public void CopyData(IFormFile file, string filename)
        {
            using (var stream = System.IO.File.OpenWrite(filename))
            {
                stream.Position = 0;
                file.CopyTo(stream);
            }
        }

        //Tranform data from FormData model to Jewelry Model
        public Jewelry FormDataToJewelry(FormData model)
        {
            var jewel = model.jewelry;
            if (model.first_thumb_file != null) { jewel.first_thumb_photo = model.first_thumb_file.FileName.Replace(".jpg", ""); };

            if (model.second_thumb_file != null) { jewel.second_thumb_photo = model.second_thumb_file.FileName.Replace(".jpg", ""); };

            if (model.yellowPhoto_file != null) { jewel.yellowPhoto = model.yellowPhoto_file.FileName.Replace(".jpg", ""); };

            if (model.whitePhoto_file != null) { jewel.whitePhoto = model.whitePhoto_file.FileName.Replace(".jpg", ""); }

            if (model.rosePhoto_file != null) { jewel.rosePhoto = model.rosePhoto_file.FileName.Replace(".jpg", ""); }

            jewel.isNecklace = Convert.ToInt32(model.Necklaces.isNecklaces);
            jewel.isBirthstone = Convert.ToInt32(model.Birthstones.isBirthstones);
            jewel.isBracelet = Convert.ToInt32(model.Bracelets.isBracelets);
            jewel.isDiamond = Convert.ToInt32(model.Diamond.isDiamond);
            jewel.isPersonalized = Convert.ToInt32(model.Personalized.isPersonalized);
            jewel.isRing = Convert.ToInt32(model.Rings.isRings);
            jewel.isEarring = Convert.ToInt32(model.Earrings.isEarring);
            jewel.isFirstPage = Convert.ToInt32(model.OrderInFirstPage.isFirstPage);

            return jewel;
        }

        public FormData JewelryToFormData(Jewelry model)
        {
            var formdata = new FormData();

            formdata.jewelry = model;

            formdata.Birthstones.isBirthstones = Convert.ToBoolean(Convert.ToInt16(model.isBirthstone));
            formdata.Bracelets.isBracelets = Convert.ToBoolean(Convert.ToInt16(model.isBracelet));
            formdata.Diamond.isDiamond = Convert.ToBoolean(Convert.ToInt16(model.isDiamond));
            formdata.Earrings.isEarring = Convert.ToBoolean(Convert.ToInt16(model.isEarring));
            formdata.Necklaces.isNecklaces = Convert.ToBoolean(Convert.ToInt16(model.isNecklace));
            formdata.Personalized.isPersonalized = Convert.ToBoolean(Convert.ToInt16(model.isPersonalized));
            formdata.Rings.isRings = Convert.ToBoolean(Convert.ToInt16(model.isRing));
            formdata.OrderInFirstPage.isFirstPage = Convert.ToBoolean(Convert.ToInt16(model.isFirstPage));
            return formdata;
        }

        private bool LoginUser(string username, string password)
        {

            var user = _context.LoginLinq.AsQueryable().FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
