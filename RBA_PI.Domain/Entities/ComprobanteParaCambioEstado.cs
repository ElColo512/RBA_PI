using System;
using System.Collections.Generic;
using System.Text;

namespace RBA_PI.Domain.Entities
{
    public class ComprobanteParaCambioEstado
    {
        public string Cuit { get; init; } = string.Empty;
        public string NroFactura { get; init; } = string.Empty;
    }
}
