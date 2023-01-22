using System;
using System.Collections.Generic;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class Asistencium
{
    public int Id { get; set; }

    public int? AsistenciaUsuarioId { get; set; }

    public virtual Usuario? AsistenciaUsuario { get; set; }
}
