using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Quests
{
    public enum UserQuestStatusE
    {
        NoData = 0,

        [Display(Name = "Aktywny")]
        Active = 10,

        [Display(Name = "Zakończony")]
        Finished = 20,
        
        [Display(Name = "Przerwany")]
        Aborted = 30,
    }
}