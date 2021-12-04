using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Rankings
{
    public enum RankingTypeE
    {
        NoData = 0,

        [Display(Name = "Szkoła")]
        School = 10,

        [Display(Name = "Miasto")]
        City = 20,
        
        [Display(Name = "Globalny")]
        Global = 100,
    }
}