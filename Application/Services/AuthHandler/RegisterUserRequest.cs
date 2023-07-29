using MediatR;
using ResultHelper;
using Infrastructure;
using Infrastructure.Repositories.AuthRepo.Model;

namespace Application.Services.AuthHandler
{

    public class RegisterUserRequest : RegisterUserIM, IRequest<ApiResult<bool>>
    { }
    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, ApiResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<bool> _apiResult;

        public RegisterUserRequestHandler(IUnitOfWork unitOfWork, IApiResult<bool> apiResult)
        {
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<bool>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            RegisterUserIM requestModel = new()
            {
                Username = request.Username,
                Password = request.Password,
                Status = request.Status,
            };

            var result = await _unitOfWork.Auth.RegisterUser(requestModel);
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess(EStatusCode.Success);
        }
    }
}



