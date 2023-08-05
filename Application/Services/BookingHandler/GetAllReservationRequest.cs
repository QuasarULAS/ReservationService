using Infrastructure;
using Infrastructure.Repositories.BookingRepo.Model;
using MediatR;
using ResultHelper;

namespace Application.Services.BookingHandler
{

    public class GetAllReservationRequest : IRequest<ApiResult<List<BookLogVM>>>
    { }
    public class GetAllReservationRequestHandler : IRequestHandler<GetAllReservationRequest, ApiResult<List<BookLogVM>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<List<BookLogVM>> _apiResult;

        public GetAllReservationRequestHandler(IUnitOfWork unitOfWork, IApiResult<List<BookLogVM>> apiResult)
        {
            _unitOfWork = unitOfWork;
            _apiResult = apiResult;
        }

        public async Task<ApiResult<List<BookLogVM>>> Handle(GetAllReservationRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Booking.GetAllBookLog();
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess("عملیات با موفقیت انجام شد.");
        }
    }
}


