using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Infrastructure.Repositories.AuthRepo.Model;
using Infrastructure.Repositories.PlaceRepo.Model;
using Newtonsoft.Json;

namespace BookingPlacesTest;

public class Places_Test
{
    private HttpClient _httpClient;


    public async Task GetToken()
    {
        _httpClient = new HttpClient();

        var _user = new UserAuthenticateM();
        _user.Username = "hasan";
        _user.Password = "1234";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_user)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync("https://localhost:7224/Auth/authenticate", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<Tokens>(result);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.Token);
    }

    [Fact]
    public async Task GetPlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new SearchPlacesDto();
        obj.PlaceName = null;
        obj.PlaceKind = null;
        obj.Page = 1;
        obj.Limit = 5;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Places/GetAllPlaces", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<SearchPlacesWithTotalAndListDto>(result);

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(5, data.data.Count());
    }


    [Fact]
    public async Task GetPlace400_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new SearchPlacesDto();
        obj.PlaceName = null;
        obj.PlaceKind = null;
        obj.Page = 0;
        obj.Limit = 0;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Places/GetAllPlaces", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("page and limit must be grater than 0!", result);
    }

    [Fact]
    public async Task RegisterPlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new InsertPlaceWithoutUserIdDto();
        obj.Title = "test-place2";
        obj.PlaceTypeId = 10;
        obj.Address = "test12";
        obj.GeographicalLocation = "1213232663";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Places/InsertPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlace400_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new InsertPlaceWithoutUserIdDto();
        obj.Title = "test-place2";

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Places/InsertPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task UpdatePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new UpdatePlaceDto
        {
            ID = 3002,
            Title = "update-test-place2",
            PlaceTypeId = 11,
            Address = "updated-s-test",
            GeographicalLocation = "u-1234567",
            RegistrantID = new Guid("ea17a795-a907-4d5b-9c04-43de8b0da1ef")
        };

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync("https://localhost:7224/Places/EditPlace", byteContent);
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

        var obj = new UpdatePlaceDto();
        obj.ID = 0;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync("https://localhost:7224/Places/EditPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task UpdatePlace400_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var obj = new BasePlace();
        obj.ID = 3003;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PutAsync("https://localhost:7224/Places/EditPlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task DeletePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        //Act
        var response = await _httpClient.DeleteAsync("https://localhost:7224/Places/DeletePlace/3002");
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
        var response = await _httpClient.DeleteAsync("https://localhost:7224/Places/EditPlace/0");
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task DeltePlace400_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        //Act
        var response = await _httpClient.DeleteAsync("https://localhost:7224/Places/EditPlace");
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }
}