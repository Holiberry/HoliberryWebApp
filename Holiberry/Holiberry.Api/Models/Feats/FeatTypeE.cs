using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Feats
{
    public enum FeatTypeE
    {
        NoData = 0,

        [Display(Name = "Przebyty dystans pieszo w ciągu dnia")]
        DistanceWalkingDaily = 100,
        [Display(Name = "Przebyty dystans rowerem w ciągu dnia")]
        DistanceBikeDaily = 101,
        [Display(Name = "Przebyty dystans hulajnogą w ciągu dnia")]
        DistanceScooterDaily = 102,


        [Display(Name = "Przebyty dystans pieszo")]
        DistanceWalkingGlobal = 200,
        [Display(Name = "Przebyty dystans rowerem")]
        DistanceBikeGlobal = 201,
        [Display(Name = "Przebyty dystans hulajnogą")]
        DistanceScooterGlobal = 202,


        [Display(Name = "Pierwsza trasa do szkoły")]
        FirstRouteHomeToSchool = 300,
        [Display(Name = "Pierwsza trasa do domu")]
        FirstRouteSchoolToHome = 301,


        [Display(Name = "Ukończone zadania")]
        QuestCount = 400,

        [Display(Name = "Zebrane punkty")]
        PointsAccumulated = 500,

    }
}