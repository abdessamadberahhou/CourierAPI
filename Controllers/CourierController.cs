using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CourierApi.Models.courier;
using CourierApi.Models.Requests;
using CourierApi.Models.Responses;
using CourierApi.Services.CourierRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourierApi.Models.file;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Controllers
{
    [Route("api/courrier")]
    [ApiController]
    [Authorize]
    public class CourierController : Controller
    {
        const float max = 5f;
        private readonly ICourierRepository _courierRepository;
        public static IWebHostEnvironment _enviroment;

        public CourierController(ICourierRepository courierRepository, IWebHostEnvironment envirment)
        {
            _courierRepository = courierRepository;
            _enviroment = envirment;
        }

        [HttpGet("allcouriers/{id}/page{page}")]
        public async Task<IActionResult> AllCourriers(Guid id, int page)
        {
            using (var context = new CouriersContext())
            {
                var pageResults = max;
                var pageCount = Math.Ceiling(context.Courriers.Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Count() / pageResults);
                var totalCourier = context.Courriers.Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Count();
                var couriers = await context.Courriers.Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();
                return Ok(new FetchCourierResponse()
                    {
                        Courier = couriers,
                        CurrentPage = page,
                        page = (int)pageCount,
                        totalCourrier = (int)totalCourier
                });
            }
        }
        [HttpGet("allarchived/{id}/page{page}")]
        public async Task<IActionResult> ArchivedCourriers(Guid id, int page)
        {
            using (var context = new CouriersContext())
            {
                var pageResults = max;
                var pageCount = Math.Ceiling(context.Courriers.Where(c => c.CourrierArchiver == 1).Where(c => c.IdUser == id).Count() / pageResults);
                var totalCourier = context.Courriers.Where(c => c.CourrierArchiver == 1).Where(c => c.IdUser == id).Count();
                var couriers = await context.Courriers.Where(c => c.CourrierArchiver == 1).Where(c => c.IdUser == id).Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();
                return Ok(new FetchCourierResponse()
                {
                    Courier = couriers,
                    CurrentPage = page,
                    page = (int)pageCount,
                    totalCourrier = (int)totalCourier
                });
            }

        }
        [HttpGet("allimportant/{id}/page{page}")]
        public async Task<IActionResult> ImportantCourriers(Guid id, int page)
        {
            using (var context = new CouriersContext())
            {
                var pageResults = max;
                var pageCount = Math.Ceiling(context.Courriers.Where(c => c.CourrierFavoriser == 1).Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Count() / pageResults);
                var totalCourier = context.Courriers.Where(c => c.CourrierFavoriser == 1).Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Count();
                var couriers = await context.Courriers.Where(c => c.CourrierFavoriser == 1).Where(c => c.CourrierArchiver == 0).Where(c => c.IdUser == id).Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();
                return Ok(new FetchCourierResponse()
                {
                    Courier = couriers,
                    CurrentPage = page,
                    page = (int)pageCount,
                    totalCourrier = (int)totalCourier
                });
            }
        }
        [HttpGet("show/{id}")]
        public async Task<IActionResult> EditCourrier(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using(var context = new CouriersContext())
            {
                var courier = context.Courriers.Where(c => c.Id == id).FirstOrDefault();
                using(var fcontext = new FilesContext())
                {
                    var files = fcontext.Files.Where(f => f.IdCourrier == id).ToList();
                    if (courier != null)
                    {
                        if (files != null)
                        {
                            return Ok(new FetchSingleCourier()
                            {
                                Courier = courier,
                                files = files
                            });
                        }
                        else
                        {
                            return Ok(courier);
                        }
                    }
                    else
                    {
                        return BadRequest("not found");
                    }
                }
            }
            
        }


        [HttpPost("add/courier")]
        public async Task<IActionResult> AddCourrier([FromBody] AddCourierRequest cour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Courrier courier = new Courrier()
            {
                Id = Guid.NewGuid(),
                TypeCourrier = cour.TypeCourrier,
                ObjetCourrier = cour.ObjetCourrier,
                ExpiditeurCourrier = cour.ExpiditeurCourrier,
                DestinataireCourrier = cour.DestinataireCourrier,
                DateCourrier = cour.DateCourrier,
                TagsCourrier = cour.TagsCourrier,
                CourrierFavoriser = 0,
                CourrierArchiver = 0,
                CourrierUrgent = cour.CourrierUrgent,
                IdUser = cour.idUser
            };
            await _courierRepository.CreateCourier(courier);
            var response = await UploadFile(cour.files, courier.Id);
            return Ok("Courrier ajouté avec succée");
        }



        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditCourrier([FromBody] AddCourierRequest cour, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Courrier courier = new Courrier()
            {
                TypeCourrier = cour.TypeCourrier,
                ObjetCourrier = cour.ObjetCourrier,
                ExpiditeurCourrier = cour.ExpiditeurCourrier,
                DestinataireCourrier = cour.DestinataireCourrier,
                DateCourrier = cour.DateCourrier,
                TagsCourrier = cour.TagsCourrier,
                CourrierUrgent = cour.CourrierUrgent,
            };
            var result = await _courierRepository.ModifyCourier(courier, id);
            var response = await UploadFile(cour.files, id);
            if (result!=null)
                return Ok("Courrier modifié avec succée");
            else
                return BadRequest("Courrier invalide");
        }





        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteCourrier(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _courierRepository.DeleteCourier(id);
            using(var fcontext = new FilesContext())
            {
                var res = fcontext.Files.Where(f => f.IdCourrier == id).FirstOrDefault();
                if(res != null)
                {
                    fcontext.Files.Remove(res);
                    fcontext.SaveChanges();
                }
            }
            if (result != null)
                return Ok("Courrier supprimé avec succée");
            else
                return BadRequest("courrier invalide");
        }





        [HttpPost("delete/file/{id}")]
        public async Task<IActionResult> DeleteCourrierFile(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using (var fcontext = new FilesContext())
            {
                var res = fcontext.Files.Where(f => f.IdFile == id).FirstOrDefault();
                if (res != null)
                {
                    fcontext.Files.Remove(res);
                    fcontext.SaveChanges();
                    return Ok("Fichier supprimé avec succée");
                }
                else
                    return BadRequest("Fichier introuvable");
            }
            
        }


        [HttpPost("favorise/{id}")]
        public async Task<IActionResult> FavoriseCourrier(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using (var context = new CouriersContext())
            {
                var courrier = context.Courriers.Where(c => c.Id == id).FirstOrDefault();
                if (courrier != null)
                {
                    if(courrier.CourrierFavoriser == 0)
                    {
                        courrier.CourrierFavoriser = 1;
                        context.SaveChanges();
                        return Ok("Courrier favriser avec succée");
                    }
                    else
                    {
                        courrier.CourrierFavoriser = 0;
                        context.SaveChanges();
                        return Ok("Courrier defavriser avec succée");
                    }
                    
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            
        }



        [HttpPost("archive/{id}")]
        public async Task<IActionResult> ArchiveCourrier(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using (var context = new CouriersContext())
            {
                var courrier = context.Courriers.Where(c => c.Id == id).FirstOrDefault();
                if (courrier != null)
                {
                    if (courrier.CourrierArchiver == 0)
                    {
                        courrier.CourrierArchiver = 1;
                        context.SaveChanges();
                        return Ok("Courrier archivé avec succée");
                    }
                    else
                    {
                        courrier.CourrierArchiver = 0;
                        context.SaveChanges();
                        return Ok("Courrier Ajouter au principale");
                    }
                    
                }
                else
                {
                    return BadRequest("Error archive");
                }
            }

        }

        [HttpPost("upload-file/{id}")]
        public async Task<IActionResult> UploadFile([FromBody] FileRequest[] img, Guid id)
        {
            List<FileRequest> bodyImages = new List<FileRequest>();
            int counter = 0;

            for(int i = 0; i < img.Length; i++)
            {
                using(var context = new FilesContext())
                {
                    var f = context.Files.Where(f => f.IdFile == img[i].Id).FirstOrDefault();
                    if(f == null)
                    {
                        Models.file.File file = new Models.file.File()
                        {
                            IdCourrier = id,
                            IdFile = Guid.NewGuid(),
                            File1 = img[i].FILE,
                            FileName = img[i].FileName,
                            FileExtention = img[i].FileExtention
                        };
                        counter ++;
                        context.Files.Add(file);
                        context.SaveChanges();
                    }
                }
                
            }
            return Ok(counter);
        }
        [HttpPost("search/allcouriers")]
        public async Task<IActionResult> SearchAllCouriers(Guid id, string query)
        {
            using(var context = new CouriersContext())
            {
                var result = context.Courriers.
                    Where(c => (c.ObjetCourrier.Contains(query)) || (c.ExpiditeurCourrier.Contains(query)) || (c.DestinataireCourrier.Contains(query)) || (c.TagsCourrier.Contains(query))).
                    Where(c => c.CourrierArchiver == 0).
                    Where(c => c.IdUser == id).
                    ToList();
                    
                if(result.Count > 0)
                {
                    return Ok(new FetchCourierResponse()
                    {
                        Courier = result
                    });
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost("search/importantcouriers")]
        public async Task<IActionResult> SearchImportantCouriers(Guid id, string query)
        {
            using (var context = new CouriersContext())
            {
                var result = context.Courriers.
                    Where(c => (c.ObjetCourrier.Contains(query)) || (c.ExpiditeurCourrier.Contains(query)) || (c.DestinataireCourrier.Contains(query)) || (c.TagsCourrier.Contains(query))).
                    Where(c => c.CourrierArchiver == 0).
                    Where(c => c.CourrierFavoriser == 1).
                    Where(c => c.IdUser == id).
                    ToList();
                if (result.Count > 0)
                {
                    return Ok(new FetchCourierResponse()
                    {
                        Courier = result
                    });
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost("search/archivedcouriers")]
        public async Task<IActionResult> SearchArchivedCouriers(Guid id, string query)
        {
            using (var context = new CouriersContext())
            {
                var result = context.Courriers.
                    Where(c => (c.ObjetCourrier.Contains(query)) || (c.ExpiditeurCourrier.Contains(query)) || (c.DestinataireCourrier.Contains(query)) || (c.TagsCourrier.Contains(query))).
                    Where(c => c.CourrierArchiver == 1).
                    Where(c => c.IdUser == id).
                    ToList();
                if (result.Count > 0)
                {
                    return Ok(new FetchCourierResponse()
                    {
                        Courier = result
                    });
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
