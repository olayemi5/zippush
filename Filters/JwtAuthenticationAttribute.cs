using CCMSAutomationAPI.Models;
//using CCMSService.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Linq;
using ClsUtility2 = CCMSAutomationAPI.Models.ClsUtility2;

namespace WebApi.Jwt.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var request = context.Request;
                var authorization = request.Headers.Authorization;
              //  = request.Headers.GetValues("ClientId").ToString();
                request.Headers.TryGetValues("ClientId", out var headerValue);
                var clientId = headerValue.FirstOrDefault();

                if (authorization == null || authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Authentication failed", request);
                    return;
                }
                

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                    return;
                }
                if (string.IsNullOrEmpty(clientId))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid ClientId", request);
                    return;
                }

                var token = authorization.Parameter;
                var principal = await AuthenticateJwtToken(token,clientId);

                if (principal == null)
                    context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

                else
                    context.Principal = principal;
            }
            catch(Exception e)
            {
               // ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:AuthenticateAsync|class: JwtAuthenticationAttribute");
                new ClsUtility2().rLogTracker("Exception AuthenticateAsync: ", e.ToString());
            }
        }

        private static bool ValidateToken(string token, /*out string clientId*/string clientId)
        {
            //clientId = null;
            try
            {
                string secretKey = JwtManager.GetSecretKey(clientId);
                if (secretKey != null)
                {

                    var simplePrinciple = JwtManager.GetPrincipal(token,clientId);
                    var identity = simplePrinciple?.Identity as ClaimsIdentity;

                    if (identity == null)
                        return false;

                    if (!identity.IsAuthenticated)
                        return false;

                    var usernameClaim = identity.FindFirst(ClaimTypes.Name);
                    clientId = usernameClaim?.Value;

                    if (string.IsNullOrEmpty(clientId))
                        return false;

                    // More validate to check whether username exists in system

                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
              //  ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:ValidateToken|class: JwtAuthenticationAttribute");
                new ClsUtility2().rLogTracker("Exception ValidateToken: ", e.ToString());
            }
            return false;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token,string clientId)
        {
            try
            {
                // if (ValidateToken(token, out var clientId))
                if (ValidateToken(token, clientId))
                {
                    // based on username to get more information from database in order to build local identity
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, clientId)
                    // Add more claims if needed: Roles, ...
                };

                    var identity = new ClaimsIdentity(claims, "Jwt");
                    IPrincipal user = new ClaimsPrincipal(identity);

                    return Task.FromResult(user);
                }

                return Task.FromResult<IPrincipal>(null);
            }
            catch(Exception e)
            {
                //  ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:AuthenticateJwtToken|class: JwtAuthenticationAttribute");
                new ClsUtility2().rLogTracker("Exception AuthenticateJwtToken: ", e.ToString());
            }
            return null;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}
