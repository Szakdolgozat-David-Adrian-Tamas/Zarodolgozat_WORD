using gameStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace gameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class felhasznaloController : ControllerBase
    {
        [HttpGet("{uId}")]

        public IActionResult Get(string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                List<Felhasznalok> list = new List<Felhasznalok>();
                using (var context = new gamestoreContext())
                {
                    try
                    {

                        return StatusCode(200, context.Felhasznaloks.ToList());
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }

        [HttpPost]

        public IActionResult Post(/*string uId,*/ Felhasznalok felhasznalok)
        {
            //if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            //{
                using (var context = new gamestoreContext())
                {
                    try
                    {
                        context.Felhasznaloks.Add(felhasznalok);
                        context.SaveChanges();
                        return Ok( "A felhasználó hozzáadása sikeresen megtörtént.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("A felhasználó hozzáadása sikertelen." + ex.Message);
                    }
                }
            //}
            //else
            //{
            //    return BadRequest("Nincs bejelentkezve/jogosultsága!");
            //}
        }

        [HttpPut("{uId}")]

        public IActionResult Put(string uId, Felhasznalok felhasznalok)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                using (var context = new gamestoreContext())
                {
                    try
                    {
                        context.Felhasznaloks.Update(felhasznalok);
                        context.SaveChanges();
                        return StatusCode(290, "A felhasználó adatainak a módosítása sikeresen megtörtént.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("A felhasználó módosítása sikertelen." + ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }

        [HttpDelete("{uId}")]

        public IActionResult Delete(string uId, int Id)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Jogosultsag == 9)
            {
                using (var context = new gamestoreContext())
                {
                    try
                    {
                        Felhasznalok felhasznalok = new Felhasznalok();
                        felhasznalok.Id = Id;
                        context.Felhasznaloks.Remove(felhasznalok);
                        context.SaveChanges();
                        return StatusCode(204, "A felhasználó adatainak törlése sikeresen megtörtént.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("A felhasználó törlése sikertelen." + ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUserName(string uId, string userName)
        {
            if (Program.LoggedInUsers.ContainsKey(uId))
            {
                using (var context = new gamestoreContext())
                {
                    try
                    {
                        var felhasznalok = context.Felhasznaloks.Where(f => f.FelhasznaloNev == userName).ToList();
                        if (felhasznalok.Count > 0)
                        {
                            context.Felhasznaloks.Remove(felhasznalok[0]);
                            context.SaveChanges();
                            return Ok("A bejelentkezési és személyes adatai törlésre kerültek.");
                        }
                        else
                        {
                            return StatusCode(210, "Nincs ilyen nevű felhasználó!");
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }
    }
}