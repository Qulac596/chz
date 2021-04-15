using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using WebService.MemoryDataBase;

namespace WebService.Filters
{
    public class AuthenticationFiltr : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            IEnumerable<string> headers;
            if (context.Request.Headers.TryGetValues("accessToken", out headers) == true)
            {
                var accessToken = headers.First();
                var user = UsersStorage.Find(accessToken, DateTime.Now);
                if (user != null)
                {
                    var getnericIdentiry = new GenericIdentity(user.Login);

                    getnericIdentiry.AddClaim(new Claim("Id", user.Id + ""));
                    getnericIdentiry.AddClaim(new Claim("Login", user.Login));
                    getnericIdentiry.AddClaim(new Claim("Password", user.Password));
                    getnericIdentiry.AddClaim(new Claim("AccessToken", accessToken));

                    context.Principal = new GenericPrincipal(getnericIdentiry, null);
                }
                else
                {
                    context.ErrorResult = new AuthenticationErrorResult("Не действительный токен доступа.");
                }
            }
            else
            {
                context.ErrorResult = new AuthenticationErrorResult("Не указан токен доступа.");
            }
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}