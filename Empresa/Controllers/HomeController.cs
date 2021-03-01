using Empresa.Data;
using Empresa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Empresa.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(string NOMBRE, string APELLIDO, string EMAIL, string TELEFONO, string MENSAJE)
        {
            enviarCorreo(NOMBRE, APELLIDO, EMAIL, TELEFONO, MENSAJE);

            ViewBag.clave = "¡GRACIAS POR CONTACTAR CON NOSOTROS! En breve nos pondremos en contacto con usted.";

            return View();
        }

        public void enviarCorreo(string nombre, string apellido, string email, string telefono, string mensaje)
        {

            string DireccionOrigen = "claudiathasp@gmail.com"; //habra que configurar nuestro gmail cambiando la seg permitiendo aplicaciones menos seguras

            MailMessage message = new MailMessage(); //Representa un mensaje de correo electrónico que se
            //puede enviar con la clase SmtpClient.

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            string pass = Properties.Resources.Password.ToString();
            smtpClient.Credentials = new System.Net.NetworkCredential() //le pasamos las credenciales
            {
                UserName = DireccionOrigen,

                Password = pass
            };

            //Especifique si el objeto SmtpClient utiliza SSL (Secure Sockets Layer) para cifrar la conexión.
            smtpClient.EnableSsl = true; //Es true si el objeto SmtpClient utiliza SSL; en caso contrario, es false.
            //De manera predeterminada, es false.
            message.From = new MailAddress(DireccionOrigen);
            message.To.Add(new MailAddress(DireccionOrigen));
            message.Subject = nombre + "-" + apellido + " - Telefono: " + telefono;
            message.IsBodyHtml = true;
            message.Body = mensaje + "Email: " + email;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //Especifica la forma en que se controlarán
            //los mensajes de correo electrónico salientes.
            smtpClient.Send(message);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
