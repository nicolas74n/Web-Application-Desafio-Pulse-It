using System;
using System.Collections.Generic;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class Invitacion
{
    public int Id { get; set; }

    public int? InvitacionEventoId { get; set; }

    public int? EstadoInvitacionId { get; set; }

    public int? UsuarioInvitadoId { get; set; }

    public string? Email { get; set; }

    public virtual EstadoInvitacion? EstadoInvitacion { get; set; }

    public virtual Evento? InvitacionEvento { get; set; }

    public virtual Usuario? UsuarioInvitado { get; set; }
}
