using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Greenlight.Modules.Users.Application.Users.GetUser;
using Greenlight.Modules.Users.IntegrationTests.Abstractions;
using Greenlight.Modules.Users.Presentation.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shouldly;

namespace Greenlight.Modules.Users.IntegrationTests.Users;

public class GetUserProfileTests : BaseIntegrationTest
{
    private const string RequestUriProfile = "users/profile";
    private const string RequestUriRegister = "users/register";

    public GetUserProfileTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenAccessTokenNotProvided()
    {
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(RequestUriProfile);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenUserExists()
    {
        // Arrange
        string accessToken = await RegisterUserAndGetAccessTokenAsync(
            "exists@test.com", Faker.Internet.Password());

        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync(RequestUriProfile);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        UserResponse? user = await response.Content.ReadFromJsonAsync<UserResponse>();
        user.ShouldNotBeNull();
    }

    private async Task<string> RegisterUserAndGetAccessTokenAsync(string email, string password)
    {
        var request = new RegisterUser.Request
        {
            Email = email,
            Password = password,
            FirstName = Faker.Name.FirstName(),
            LastName = Faker.Name.LastName()
        };

        await HttpClient.PostAsJsonAsync(RequestUriRegister, request);

        string accessToken = await GetAccessTokenAsync(request.Email, request.Password);

        return accessToken;
    }
}
