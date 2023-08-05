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
    public async Task ReservePlace200_Test()
    {
        _httpClient = new HttpClient();

        //Arrange
        await GetToken();

        var bookLog = new MakeBookLogWithoutUserIdIM();
        bookLog.BookingPlaceId = 1003;
        bookLog.ReservationDate = DateTime.Now;
        bookLog.Price = 155000;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookLog)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Booking/ReservePlace", byteContent);
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

        var bookLog = new MakeBookLogWithoutUserIdIM();
        bookLog.BookingPlaceId = 1002;
        bookLog.ReservationDate = new DateTime(2023, 05, 30, 13, 37, 11);
        bookLog.Price = 10000;

        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookLog)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //Act
        var response = await _httpClient.PostAsync(_localHostURL + "/Booking/ReservePlace", byteContent);
        var result = await response.Content.ReadAsStringAsync();
        var _res = JsonConvert.DeserializeObject<dynamic>(result);
        var msg = _res.message.ToString();

        //Assert
        Assert.Equal("این مکان از قبل رزرو شده است ، نمیتوانید ذر این زمان رزرو کنید.", msg);
    }
}