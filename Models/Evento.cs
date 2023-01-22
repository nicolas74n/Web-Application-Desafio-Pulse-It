using System;
using System.Collections.Generic;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class Evento
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaEvento { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Invitacion> Invitacions { get; } = new List<Invitacion>();

    public virtual Usuario? Usuario { get; set; }
}
