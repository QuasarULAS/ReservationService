﻿using Core.Base.Enum;
using Core.Models;
using Infrastructure;
using Infrastructure.Repositories.PlaceRepo.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using ResultHelper;

namespace Application.Services.PlaceHandler
{

    public class UpdatePlaceRequest : UpdatePlaceIM, IRequest<ApiResult<bool>>
    {
    }
    public class UpdatePlaceRequestHandler : IRequestHandler<UpdatePlaceRequest, ApiResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiResult<bool> _apiResult;
        public UpdatePlaceRequestHandler(IUnitOfWork unitOfWork, IApiResult<bool> apiResult)
        {
            _apiResult = apiResult;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<bool>> Handle(UpdatePlaceRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.Place.GetPlaceById(request.ID);
            if (isExist == null)
            {
                return _apiResult.WithSuccess("مکان مورد نظر یافت نشد.");
            }
            bool IsUpdate;

            UpdatePlaceIM requestModel = new()
            {
                ID = request.ID,
                Title = request.Title,
                Address = request.Address,
                PlaceTypeId = request.PlaceTypeId,
                GeographicalLocation = request.GeographicalLocation,
                RegistrantID = request.RegistrantID,
            };

            var result = await _unitOfWork.Place.UpdatePlace(requestModel);
            _apiResult.WithValue(result);
            return _apiResult.WithSuccess("آپدیت با موفقیت انجام شد.");
        }
    }
}

