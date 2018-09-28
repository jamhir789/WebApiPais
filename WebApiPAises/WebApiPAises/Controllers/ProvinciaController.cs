using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPAises.Models;

namespace WebApiPAises.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais/{PaisId}/Provincia")]
    public class ProvinciaController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int MyProperty { get; set; }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return context.Provincias.Where(x => x.PaisID == PaisId).ToList();
        }




        [HttpGet("{id}", Name = "provinciaById")]
        public IActionResult GetById(int id)
        {
            var pais = context.Provincias.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            return new ObjectResult(pais);
        }


        //agregar
        [HttpPost]
        public IActionResult Create([FromBody]Provincia provincia, int idpais)
        {
            provincia.PaisID = idpais;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            context.Provincias.Add(provincia);
            context.SaveChanges();
            return new CreatedAtRouteResult("provinciaByid", new { id = provincia.Id });
        }





        //modificar
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }
            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //eliminar
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia= context.Provincias.FirstOrDefault(x => x.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }




      

    }
}