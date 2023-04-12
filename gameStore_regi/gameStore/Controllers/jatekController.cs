using gameStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class jatekController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            List<Osszesjatek> list = new List<Osszesjatek>();
            using (var context = new gamestoreContext())
            {
                try
                {

                    return StatusCode(200, context.Osszesjateks.ToList());
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost/*("{uId}")*/]  

        public IActionResult Post(/*string uId,*/ Osszesjatek osszesjatek)
        {
            using (var context = new gamestoreContext())
            {
                try
                {
                    context.Osszesjateks.Add(osszesjatek);
                    context.SaveChanges();
                    return StatusCode(201, "A játék hozzáadása sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest("A játék hozzáadása sikertelen." + ex.Message);
                }
            }
        }

        [HttpPut/*("{uId}")*/]

        public IActionResult Put(/*string uId,*/ Osszesjatek osszesjatek)
        {
            using (var context = new gamestoreContext())
            {
                try
                {
                    context.Osszesjateks.Update(osszesjatek);
                    context.SaveChanges();
                    return StatusCode(290, "A játék adatainak a módosítása sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest("A játék módosítása sikertelen." + ex.Message);
                }
            }
        }

        [HttpDelete/*("{uId}")*/]

        public IActionResult Delete(/*string uId,*/ int Id) 
        {
            using (var context = new gamestoreContext())
            {
                try
                {
                    Osszesjatek osszesjatek = new Osszesjatek();
                    osszesjatek.Id = Id;
                    context.Osszesjateks.Remove(osszesjatek);
                    context.SaveChanges();
                    return StatusCode(204, "A játék adatainak törlése sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest("A játék törlése sikertelen." + ex.Message);
                }
            }
        }
    }
}