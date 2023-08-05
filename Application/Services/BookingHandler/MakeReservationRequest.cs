using System.IdentityModel.Tokens.Jwt;
using Infrastructure;
using Infrastructure.Repositories.BookingRepo.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using ResultHelper;

namespace Application.Services.BookingHandler
{

    public class MakeReservationRequest : MakeBookLogWithoutUserIdIM, IRequest<ApiResult<bool>>
    {
    }
    public class MakeReservationRequestHandler : IRequestHandler<MakeReservationRequest, ApiResult<bool>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<bool> _apiResult;

        public MakeReservationRequestHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IApiResult<bool> apiResult)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<bool>> Handle(MakeReservationRequest request, CancellationToken cancellationToken)
        {
            BookLogRecordsVM bkr = new()
            {
                ReservationDate = request.ReservationDate,
                BookingPlaceId = request.BookingPlaceId,
            };

            var IsExist = await _unitOfWork.Booking.GetBookLogRecords(bkr);

            if (IsExist != null)
            {
                return _apiResult.WithError("این مکان از قبل رزرو شده است ، نمیتوانید ذر این زمان رزرو کنید.");
            };

            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string token = authorizationHeader.Substring("Bearer ".Length);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Guid UserId = new(jwtToken.Claims.FirstOrDefault(c => c.Type == "ID")?.Value);

            MakeBookLogWithoutUserIdIM requestModel = new()
            {
                ReservationDate = request.ReservationDate,
                BookingPlaceId = request.BookingPlaceId,
                Price = request.Price,
            };

            var result = await _unitOfWork.Booking.InsertBookLog(requestModel, UserId);
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess("مکان مورد نظر با موفقیت رزرو شد.");
        }
    }
}


