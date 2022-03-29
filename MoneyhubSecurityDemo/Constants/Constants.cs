using System;
namespace MoneyhubSecurityDemo
{
    public static class Constants
    {
        public static string AuthorityUri = "AuthorityUri";
        public static string RedirectUri = "io.identitymodel.native://callback";
        public static string ApiUri = "ApiUri";
        public static string ClientId = "pkce_client";
        public static string Scope = "openid profile email api1 offline_access";
    }
}
