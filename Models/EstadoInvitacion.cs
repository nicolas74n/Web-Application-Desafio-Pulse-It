using System;
using System.Collections.Generic;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class EstadoInvitacion
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Invitacion> Invitacions { get; } = new List<Invitacion>();
}
