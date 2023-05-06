using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Authorization
{
    public class UserIsPinAuthorAuthorizationHandler
            : AuthorizationHandler<UserIsPinAuthorRequirement, Pin>
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserIsPinAuthorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsPinAuthorRequirement requirement, Pin resource)
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
