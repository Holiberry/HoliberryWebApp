using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Stats
{
    public enum StatTypeE
    {
        NoData = 0,

        [Display(Name = "Dystans pieszo")]
        DistanceWalking = 10,

        [Display(Name = "Dystans rowerem")]
        DistanceBike = 20,
        
        [Display(Name = "Dystans hulajnogą")]
        DistanceScooter = 30,
    }
}