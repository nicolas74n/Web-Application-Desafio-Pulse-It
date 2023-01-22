using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Application_Desafio_Pulse_It.Models;

namespace Web_Application_Desafio_Pulse_It.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitacionControllerRest : ControllerBase
    {
        private readonly DesafioContext _context;

        public InvitacionControllerRest(DesafioContext context)
        {
            _context = context;
        }

        // GET: api/InvitacionControllerRest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invitacion>>> GetInvitacions()
        {
            return await _context.Invitacions.ToListAsync();
        }

        // GET: api/InvitacionControllerRest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invitacion>> GetInvitacion(int id)
        {
            var invitacion = await _context.Invitacions.FindAsync(id);

            if (invitacion == null)
            {
                return NotFound();
            }

            return invitacion;
        }
    }
}
