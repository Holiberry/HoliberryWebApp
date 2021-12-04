using System.ComponentModel.DataAnnotations;

namespace Holiberry.Api.Models.Quests
{
    public enum QuestStatusE
    {
        NoData = 0,

        [Display(Name = "Aktywny")]
        Active = 10,

        [Display(Name = "Nieaktywny")]
        NotActive = 20,
    }
}