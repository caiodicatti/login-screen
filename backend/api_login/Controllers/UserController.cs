using api_login.Application;
using api_login.Application.Interface;
using api_login.Model;
using api_login.Repository.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace api_login.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UserController : Controller
    {

        public readonly IAppUser app;

        public UserController(IAppUser _app)
        {
            app = _app;
        }

        // POST: UserController/Create
        [HttpPost("cadastro")]
        //[ValidateAntiForgeryToken]
        public ActionResult Cadastra(User user)
        {
            try
            {
                Response ret = app.Cadastrar(user);

                if (ret.success)
                {
                    return Ok(ret);
                }
                else
                {
                    return BadRequest(ret);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //// POST: UserController/Create
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Autentica(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// POST: UserController/Create
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult AlteraSenha(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
       
    }
}
