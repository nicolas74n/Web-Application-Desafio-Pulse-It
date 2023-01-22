using System;
using System.Collections.Generic;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string NombreCompleto
    {
        get { return Nombre + " " + Apellido; }
    }

    public string? Email { get; set; }

    public string? Contrasena { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; } = new List<Asistencium>();

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();

    public virtual ICollection<Invitacion> Invitacions { get; } = new List<Invitacion>();
}
