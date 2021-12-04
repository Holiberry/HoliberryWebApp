using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Tracks
{
    public enum TrackDestinationTypeE
    {
        NoData = 0,

        [Display(Name = "Szkoła")]
        School = 10,

        [Display(Name = "Dom")]
        Home = 20,

        [Display(Name = "Inne")]
        Different = 30,
    }
}