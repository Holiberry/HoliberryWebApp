using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Threats
{
    public enum UserThreatTypeE
    {
        NoData = 0,

        [Display(Name = "Wypadek")]
        Accident = 10,

        [Display(Name = "Hałas")]
        Noise = 20,
    }
}