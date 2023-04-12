using gameStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace gameStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JelszoController : ControllerBase
    {
        [HttpPost("{felhasznaloNev},{regiJelszo},{ujJelszo}")]

        public IActionResult JelszoMosositas(string felhasznaloNev, string regiJelszo, string ujJelszo)
        {
            try
            {
                using (gamestoreContext context = new gamestoreContext())
                {
                    var felhasznalok = context.Felhasznaloks.Where(f => f.FelhasznaloNev == felhasznaloNev).ToList();
                    Console.WriteLine();
                    if (felhasznalok.Count > 0)
                    {
                        if (regiJelszo == felhasznalok[0].Hash)
                        {
                            Felhasznalok felhasznalo = felhasznalok[0];
                            felhasznalo.Hash = ujJelszo;
                            context.Felhasznaloks.Update(felhasznalo);
                            context.SaveChanges();
                            return Ok("A jelszó módosítása sikeresen megtörtént.");
                        }
                        else
                        {
                            return StatusCode(201, "Hibás a régi jelszó!");
                        }
                    }
                    else
                    {
                        return BadRequest("Nincs ilyen nevű felhasználó!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{Email}")]
        public IActionResult ElfelejtettJelszo(string Email)
        {
            using (gamestoreContext context = new gamestoreContext())
            {
                try
                {
                    var felhasznalok = context.Felhasznaloks.Where(f => f.Email == Email).ToList();
                    if (felhasznalok.Count > 0)
                    {
                        string jelszo = Program.GenerateSalt().Substring(0, 12);
                        felhasznalok[0].Hash = Program.CreateSHA256(Program.CreateSHA256(jelszo + felhasznalok[0].Salt));
                        context.Felhasznaloks.Update(felhasznalok[0]);
                        context.SaveChanges();
                        Program.SendEmail(@felhasznalok[0].Email, "Elfelejtett jelszó", "Az új jelszava: " + jelszo);
                        return Ok("E-mail küldése megtörtént.");
                    }
                    else
                    {
                        return StatusCode(201, "Nincs ilyen e-Mail cím!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}