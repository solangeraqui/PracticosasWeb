﻿using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using PracticosasWeb.Models;
using System.Diagnostics;
using MailKit.Net.Smtp;
using static Org.BouncyCastle.Math.EC.ECCurve;
using PracticosasWeb.Models.DB;
using Microsoft.AspNetCore.Http;

namespace PracticosasWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult contacto()
        {
           
            return View();
        }
        public IActionResult Login(string usuario, string password)
        {
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password))
            {
                Usuario user;

                using (var dbContext = new BdpracticosasContext()) // Reemplaza "TuDbContext" con el contexto de tu base de datos
                {
                    user = dbContext.Usuarios.FirstOrDefault(p => p.Nombre == usuario);
                }

                if (user != null && user.Contrasena == password && user.TipoUsuarioId==1)
                {
                    HttpContext.Session.SetString("Usuario", user.UsuarioId.ToString());
                    HttpContext.Session.SetString("Usuario", usuario);
                    HttpContext.Session.SetString("Contrasena", password);
                    return RedirectToAction("ADmin", "Administrador");
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Error", "Home");

        }
        public void Enviar()
        {
            string _nombre = Request.Form["name"].ToString();
            string Asunto = Request.Form["Asunto"].ToString();
            string _email = Request.Form["Para"].ToString();
            string _mensaje = Request.Form["Contenido"].ToString();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(_email));
            email.Subject = Asunto;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = _mensaje
            };

            using var smtp = new SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Port").Value),
            SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);
            smtp.Send(email);
            smtp.Disconnect(true);


            // Retornar una respuesta HTTP que ejecuta el código JavaScript en el navegador

            //return 0;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}