using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Quests
{
    public enum QuestTypeE
    {
        NoData = 0,


        [Display(Name = "Przebyty dystans pieszo")]
        DistanceWalking = 100,

        [Display(Name = "Przebyty dystans rowerem")]
        DistanceBike = 101,

        [Display(Name = "Przebyty dystans hulajnogą")]
        DistanceScooter = 102,




        [Display(Name = "Wspólna trasa")]
        TrackTogether = 200,
    }
}