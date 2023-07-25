using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Infrastructure.Repositories.AuthRepo.Model;
using Infrastructure.Repositories.BookingRepo.Model;
using Newtonsoft.Json;

namespace BookingPlacesTest;

public class BookLog_Test
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
    public async Task ReservePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var bookLog = new MakeBookLogWithoutUserIdDto();
        bookLog.BookingPlaceId = 2009;
        bookLog.ReservationDate = DateTime.Now;
        bookLog.Price = 1;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookLog)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Booking/ReservePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.True(!string.IsNullOrEmpty(result));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task ReservePlace400_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var bookLog = new MakeBookLogWithoutUserIdDto();
        bookLog.BookingPlaceId = 1002;
        bookLog.ReservationDate = new DateTime(2023, 05, 30, 13, 37, 11);
        bookLog.Price = 10000;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookLog)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync("https://localhost:7224/Booking/ReservePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("This Place Is Reserved By Another Person In This Time.", result);
    }
}