using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Tracks
{
    public enum TrackTransportTypeE
    {
        NoData = 0,

        [Display(Name = "Pieszo")]
        Walking = 10,
        
        [Display(Name = "Rower")]
        Bike = 20,
        
        [Display(Name = "Hulajnoga")]
        Scooter = 30,

    }
}