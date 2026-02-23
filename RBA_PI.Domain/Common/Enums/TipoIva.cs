using System.ComponentModel.DataAnnotations;

namespace RBA_PI.Domain.Common.Enums
{
    public enum TipoIva
    {
        [Display(Name = "21", Description = "21%")]
        Iva21,

        [Display(Name = "27", Description = "27%")]
        Iva27,

        [Display(Name = "10.5", Description = "10.5%")]
        Iva105,

        [Display(Name = "2.5", Description = "2.5%")]
        Iva25,

        [Display(Name = "5", Description = "5%")]
        Iva5
    }
}
