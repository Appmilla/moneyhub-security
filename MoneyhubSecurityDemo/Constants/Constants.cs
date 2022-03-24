﻿using System;
namespace MoneyhubSecurityDemo
{
    public static class Constants
    {
        //public static string AuthorityUri = "https://identityserverhost20220225150440.azurewebsites.net";
        public static string AuthorityUri = "https://identityservertest20220322115258.azurewebsites.net";
        public static string RedirectUri = "io.identitymodel.native://callback";
        //public static string ApiUri = "https://api20220228203615.azurewebsites.net/";
        public static string ApiUri = "https://moneyhubauth20220321154509.azurewebsites.net/";
        public static string ClientId = "pkce_client";
        public static string Scope = "openid profile email api1 offline_access";
    }
}
