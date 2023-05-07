using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniPinterest.Web.Models.ViewModels
{
    public class AddPinToBoardRequest
    {
        public string PinId { get; set; }
        public string BoardId { get; set; }

        public IEnumerable<SelectListItem> Boards { get; set; }
    }
}
