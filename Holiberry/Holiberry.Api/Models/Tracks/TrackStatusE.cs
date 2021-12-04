using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Tracks
{
    public enum TrackStatusE
    {
        NoData = 0,

        [Display(Name = "Aktywna")]
        Active = 10,
        
        [Display(Name = "Zakończona")]
        Finished = 20,

        [Display(Name = "Przerwana")]
        Aborted = 30,
    }
}