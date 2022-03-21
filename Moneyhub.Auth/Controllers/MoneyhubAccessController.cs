using Appmilla.Moneyhub.Refit.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Moneyhub.Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyhubAccessController : ControllerBase
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<MoneyhubAccessController> _logger;
        private readonly IAccessToken _accessToken;

        public MoneyhubAccessController(IHttpClientFactory httpFactory, ILogger<MoneyhubAccessController> logger, IAccessToken accessToken)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _accessToken = accessToken;
        }

        [Authorize]
        [HttpGet(Name = "GetAccessToken")]
        public async Task<ActionResult<TokenResponse>> Get()
        {
            AccessTokenResponse? accessTokenResponse = null;
            try
            {
                accessTokenResponse = await _accessToken.GetAccessToken(new AccessTokenRequest() { GrantType = "client_credentials", Scope = "accounts:read savings_goals:read", Sub = "61ac9b75220d4100a72e17a2" });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error: " + e.Message);
            }
            return new OkObjectResult(accessTokenResponse?.access_token);
        }
    }
}