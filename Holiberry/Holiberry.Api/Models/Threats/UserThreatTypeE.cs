using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Threats
{
    public enum UserThreatTypeE
    {
        NoData = 0,

        [Display(Name = "Zagrożenie")]
        Danger = 1,


        [Display(Name = "Brak chodnika")]
        NoSideWalk = 10,

        [Display(Name = "Hałas")]
        Noise = 11,

        [Display(Name = "Niebezpieczne miejsce")]
        DangerousPlace = 12,

        [Display(Name = "Niebezpieczne przejście")]
        DangerousPass = 13,

        [Display(Name = "Nieoświetlona droga")]
        NoLights = 14,

        [Display(Name = "Nierówny chodnik")]
        UnevenSidewalk = 15,

        [Display(Name = "Roboty drogowe")]
        RoadWorks = 16,

        [Display(Name = "Wypadek")]
        Accident = 17,

    }
}