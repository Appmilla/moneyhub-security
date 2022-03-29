# moneyhub-security

A Xamarin.Forms mobile app which:-

* Authenticates with an IdentityServer to obtain an access token using OAuth 2 PKCE flow
* Calls an API protected by the IdentityServer to obtain an access token from Moneyhub Identity
* Calls the Moneyhub API with this access token to retrieve account information and display this on the UI

An overview video with a demo can be found at https://youtu.be/8dhQkQoXD3s

The API client can be found here https://github.com/Appmilla/moneyhub-api-client

The IdentityServer was generated from this .Net CLI template https://github.com/Appmilla/identity-server-user-registration-template

The API was generated from this .Net CLI template https://github.com/Appmilla/moneyhub-auth-api-template
