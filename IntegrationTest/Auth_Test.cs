using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Infrastructure.Repositories.AuthRepo.Model;
using Newtonsoft.Json;

namespace BookingPlacesTest;

public class Auth_Test
{
    private HttpClient _httpClient;
    private readonly string _localHostURL = "https://localhost:7020";

    [Fact]
    public async Task RegisterUser200_Test()
    {
        //Arrange
        _httpClient = new HttpClient();

        var obj = new RegisterUserIM();
        obj.Username = "satava";
        obj.Password = "667788";
        obj.Status = false;

        // for convert media type to application/json
        var data = JsonConvert.SerializeObject(obj);
        var buffer = Encoding.UTF8.GetBytes(data);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Auth/RegisterUser", byteContent);

        //Act
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegisterUser400_Test()
    {
        //Arrange
        _httpClient = new HttpClient();

        var obj = new UserIM();
        obj.Username = "test";
        obj.Password = "no";

        // for convert media type to application/json
        var data = JsonConvert.SerializeObject(obj);
        var buffer = Encoding.UTF8.GetBytes(data);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Auth/RegisterUser", byteContent);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Authenticate_200()
    {
        //Arrange
        _httpClient = new HttpClient();

        var _user = new UserIM();
        _user.Username = "hasan";
        _user.Password = "1234";

        // for convert media type to application/json
        var data = JsonConvert.SerializeObject(_user);
        var buffer = Encoding.UTF8.GetBytes(data);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Auth/authenticate", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var _token = JsonConvert.DeserializeObject<Tokens>(result);

        //Assert
        Assert.NotEmpty(_token.Token);
    }

    [Fact]
    public async Task Authenticate_401()
    {
        _httpClient = new HttpClient();

        var _user = new UserIM();
        _user.Username = "hasan";
        _user.Password = "12345";

        // for convert media type to application/json
        var data = JsonConvert.SerializeObject(_user);
        var buffer = Encoding.UTF8.GetBytes(data);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync(_localHostURL + "/Auth/authenticate", byteContent);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}