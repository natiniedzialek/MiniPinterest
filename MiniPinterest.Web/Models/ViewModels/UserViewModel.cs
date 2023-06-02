using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
