using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Application.Services.PlaceHandler.Dto;
using Core.Models;
using Infrastructure.Repositories.AuthRepo.Model;
using Infrastructure.Repositories.PlaceRepo.Model;
using Newtonsoft.Json;

namespace BookingPlacesTest;

public class Places_Test
{
    private HttpClient _httpClient;
    private readonly string _localHostURL = "https://localhost:7020";

    public async Task GetToken()
    {
        _httpClient = new HttpClient();

        var _user = new UserIM();
        _user.Username = "hasan";
        _user.Password = "1234";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_user)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync(_localHostURL + "/Auth/authenticate", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var _token = JsonConvert.DeserializeObject<dynamic>(result);
        var token = _token.data.token.ToString();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task RegisterPlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new InsertPlaceWithoutUserIdIM();
        obj.Title = "test-place4";
        obj.PlaceTypeId = (Core.Base.Enum.EPlaceType)12;
        obj.Address = "test14545";
        obj.GeographicalLocation = "12145q34a5355";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Places/InsertPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlace500_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new InsertPlaceWithoutUserIdIM();
        obj.Title = "test-place2";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Places/InsertPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }


    [Fact]
    public async Task UpdatePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new UpdatePlaceIM
        {
            ID = 2010,
            Title = "update-test-place5",
            PlaceTypeId = (Core.Base.Enum.EPlaceType)9,
            Address = "updated-s-test",
            GeographicalLocation = "u-1234567",
            RegistrantID = new Guid("76992b33-8e43-4857-846b-1cce064adcf4")
        };

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync(_localHostURL + "/Places/UpdatePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdatePlace404_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new UpdatePlaceIM();
        obj.ID = 0;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync(_localHostURL + "/Places/UpdatePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var _res = JsonConvert.DeserializeObject<dynamic>(result);
        var msg = _res.message.ToString();
        //Assert
        Assert.Equal("مکان مورد نظر یافت نشد.", msg);
    }


    [Fact]
    public async Task UpdatePlace500_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new Places();
        obj.ID = 3003;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync(_localHostURL + "/Places/UpdatePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }


    [Fact]
    public async Task DeletePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        //Act
        var response = await _httpClient.DeleteAsync(_localHostURL + "/Places/DeletePlace?PlaceId=3005");
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeletePlace404_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();


        //Act
        var response = await _httpClient.DeleteAsync(_localHostURL + "/Places/DeletePlace?PlaceId=0");
        var result = await response.Content.ReadAsStringAsync();
        var _res = JsonConvert.DeserializeObject<dynamic>(result);
        var msg = _res.message.ToString();
        //Assert
        Assert.Equal("مکان مورد نظر یافت نشد.", msg);
    }
}