using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Application_Desafio_Pulse_It.Models;
using Web_Application_Desafio_Pulse_It.Services;

namespace Web_Application_Desafio_Pulse_It.Controllers
{
    public class InvitacionController : Controller
    {
        private readonly DesafioContext _context;
        private readonly IMailService _mailService;

        public InvitacionController(DesafioContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // GET: Invitacion
        public async Task<IActionResult> Index()
        {
            var desafioContext = _context.Invitacions.Include(i => i.EstadoInvitacion).Include(i => i.InvitacionEvento).Include(i => i.UsuarioInvitado);
            return View(await desafioContext.ToListAsync());
        }

        // GET: Invitacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions
                .Include(i => i.EstadoInvitacion)
                .Include(i => i.InvitacionEvento)
                .Include(i => i.UsuarioInvitado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitacion == null)
            {
                return NotFound();
            }

            return View(invitacion);
        }

        // GET: Invitacion/Respuesta/5
        public async Task<IActionResult> Respuesta(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions
                .Include(i => i.EstadoInvitacion)
                .Include(i => i.InvitacionEvento)
                .Include(i => i.UsuarioInvitado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitacion == null)
            {
                return NotFound();
            }
            if(invitacion.EstadoInvitacionId != 1)
            {
                return View("Respondido", invitacion);
            }

            return View(invitacion);
        }

        // GET: Invitacion/Respuesta/Aceptar/5
        public async Task<IActionResult> Aceptar(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions
                .Include(i => i.EstadoInvitacion)
                .Include(i => i.InvitacionEvento)
                .Include(i => i.UsuarioInvitado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitacion == null)
            {
                return NotFound();
            }


            try
            {
                invitacion.EstadoInvitacionId = 2;
                _context.Update(invitacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitacionExists(invitacion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(invitacion);
        }

        // GET: Invitacion/Respuesta/Rechazar/5
        public async Task<IActionResult> Rechazar(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions
                .Include(i => i.EstadoInvitacion)
                .Include(i => i.InvitacionEvento)
                .Include(i => i.UsuarioInvitado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitacion == null)
            {
                return NotFound();
            }


            try
            {
                invitacion.EstadoInvitacionId = 3;
                _context.Update(invitacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitacionExists(invitacion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(invitacion);
        }

        // GET: Invitacion/Create
        public IActionResult Create()
        {
           // List<Usuario> usuarios = new List<Usuario>();
           // usuarios.AddRange(_context.Usuarios);

           // usuarios.Add(new Usuario { Id = -1, Nombre = "Usuario Sin Cuenta", Apellido = " "});

            ViewData["InvitacionEventoId"] = new SelectList(_context.Eventos, "Id", "Titulo");
            ViewData["UsuarioInvitadoId"] = new SelectList(_context.Usuarios, "Id", "NombreCompleto");
            return View();
        }

        // POST: Invitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvitacionEventoId,EstadoInvitacionId,UsuarioInvitadoId,Email")] Invitacion invitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invitacion);
                 await _context.SaveChangesAsync();
                var evento = await _context.Eventos.FirstOrDefaultAsync(m => m.Id == invitacion.InvitacionEventoId);
                sendMail(invitacion, evento.Titulo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoInvitacionId"] = new SelectList(_context.EstadoInvitacions, "Id", "Id", invitacion.EstadoInvitacionId);
            ViewData["InvitacionEventoId"] = new SelectList(_context.Eventos, "Id", "Id", invitacion.InvitacionEventoId);
            ViewData["UsuarioInvitadoId"] = new SelectList(_context.Usuarios, "Id", "Id", invitacion.UsuarioInvitadoId);
            return View(invitacion);
        }

        public async void sendMail(Invitacion invitacion, string titulo)
        {
            string email;
            if (invitacion.Email != null)
            {
                email = invitacion.Email;
            }
            else
            {
                var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == invitacion.UsuarioInvitadoId);
                email = usuario.Email;
            }
            string subject = "Te han invitado a: " +titulo;
            string body = "Te han invitado a: " + titulo+ "\n\n\n para responder ingresa al siguiente enlace: \n https://localhost:7270/Invitacion/Respuesta/"+invitacion.Id;
            _mailService.SendEmailAsync(new MailRequest(email, subject, body));
        }

        // GET: Invitacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions.FindAsync(id);
            if (invitacion == null)
            {
                return NotFound();
            }
            ViewData["EstadoInvitacionId"] = new SelectList(_context.EstadoInvitacions, "Id", "Id", invitacion.EstadoInvitacionId);
            ViewData["InvitacionEventoId"] = new SelectList(_context.Eventos, "Id", "Id", invitacion.InvitacionEventoId);
            ViewData["UsuarioInvitadoId"] = new SelectList(_context.Usuarios, "Id", "Id", invitacion.UsuarioInvitadoId);
            return View(invitacion);
        }

        // POST: Invitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvitacionEventoId,EstadoInvitacionId,UsuarioInvitadoId,Email")] Invitacion invitacion)
        {
            if (id != invitacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvitacionExists(invitacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoInvitacionId"] = new SelectList(_context.EstadoInvitacions, "Id", "Id", invitacion.EstadoInvitacionId);
            ViewData["InvitacionEventoId"] = new SelectList(_context.Eventos, "Id", "Id", invitacion.InvitacionEventoId);
            ViewData["UsuarioInvitadoId"] = new SelectList(_context.Usuarios, "Id", "Id", invitacion.UsuarioInvitadoId);
            return View(invitacion);
        }

        // GET: Invitacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invitacions == null)
            {
                return NotFound();
            }

            var invitacion = await _context.Invitacions
                .Include(i => i.EstadoInvitacion)
                .Include(i => i.InvitacionEvento)
                .Include(i => i.UsuarioInvitado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitacion == null)
            {
                return NotFound();
            }

            return View(invitacion);
        }

        // POST: Invitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invitacions == null)
            {
                return Problem("Entity set 'DesafioContext.Invitacions'  is null.");
            }
            var invitacion = await _context.Invitacions.FindAsync(id);
            if (invitacion != null)
            {
                _context.Invitacions.Remove(invitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvitacionExists(int id)
        {
          return _context.Invitacions.Any(e => e.Id == id);
        }
    }
}
