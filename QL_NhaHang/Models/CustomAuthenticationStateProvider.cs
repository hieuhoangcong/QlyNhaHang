using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Check if the application is being prerendered
        var isPrerendering = false;
        try
        {
            var username = await _localStorage.GetItemAsync<string>("username");
            if (string.IsNullOrEmpty(username))
            {
                // User is not authenticated
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // User is authenticated
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "apiauth_type");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch (InvalidOperationException)
        {
            // If an InvalidOperationException occurs, it means we're in prerendering mode
            isPrerendering = true;
        }

        if (isPrerendering)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task MarkUserAsAuthenticated(string username)
    {
        await _localStorage.SetItemAsync("username", username);
        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, "apiauth_type");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("username");
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}
