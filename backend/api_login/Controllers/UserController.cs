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
        public ActionResult Cadastrar(User user)
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

        [HttpPost("logar")]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Authentication authentication)
        {
            try
            {
                Response ret = app.Login(authentication);

                if (ret.success)
                {
                    return Ok(ret);
                }
                else
                {
                    return BadRequest(ret);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("recuperarsenha")]
        //[ValidateAntiForgeryToken]
        public ActionResult RecuperarSenha(Authentication authentication)
        {
            try
            {
                Response ret = app.Login(authentication);

                if (ret.success)
                {
                    return Ok(ret);
                }
                else
                {
                    return BadRequest(ret);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("linkrecuperacao")]
        //[ValidateAntiForgeryToken]
        public ActionResult AlteraSenha(RecoverPasswordLink recoverPasswordLink)
        {
            try
            {
                Response ret = app.AlteraSenha(recoverPasswordLink);

                if (ret.success)
                {
                    return Ok(ret);
                }
                else
                {
                    return BadRequest(ret);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
