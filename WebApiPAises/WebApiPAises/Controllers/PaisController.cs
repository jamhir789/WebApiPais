﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPAises.Models;

namespace WebApiPAises.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PaisController : Controller
    {
        private readonly ApplicationDbContext context;
        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return context.Paises.ToList();
            
        }


        [HttpGet("{id}", Name = "paisCreado")]
        public IActionResult GetById(int id)
        {
            var pais = context.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);
            if(pais == null)
            {
                return NotFound();
            }
            return Ok(pais);
        }


        //agregar
        [HttpPost]
        public IActionResult post([FromBody]Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisCreado", new { id = pais.Id });

            }
            return BadRequest(ModelState);
        }




        //modificar
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Pais pais, int id)
        {
            if (pais.Id !=id)
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
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            context.Paises.Remove(pais);
            context.SaveChanges();
            return Ok(pais);
        }
    }
}