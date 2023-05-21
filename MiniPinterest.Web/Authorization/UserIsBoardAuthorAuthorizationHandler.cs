using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Authorization
{
    public class UserIsBoardAuthorAuthorizationHandler
        : AuthorizationHandler<UserIsBoardAuthorRequirement, Board>
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserIsBoardAuthorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsBoardAuthorRequirement requirement, Board resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (resource.AuthorId.ToString() == userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
