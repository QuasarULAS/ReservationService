using Application.Services.PlaceHandler.Dto;
using Infrastructure.Repositories.AuthRepo.Model;
using Infrastructure.Repositories.BookingRepo.Model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        // Assuming ResultHelper.ApiResult<T> is the implementation of IApiResult<T> 
        services.AddScoped(typeof(ResultHelper.IApiResult<bool>), typeof(ResultHelper.ApiResult<bool>));
        services.AddScoped(typeof(ResultHelper.IApiResult<SearchPlacesWithTotalAndListVM>), typeof(ResultHelper.ApiResult<SearchPlacesWithTotalAndListVM>));
        services.AddScoped(typeof(ResultHelper.IApiResult<List<BookLogVM>>), typeof(ResultHelper.ApiResult<List<BookLogVM>>));
        services.AddScoped(typeof(ResultHelper.IApiResult<Tokens>), typeof(ResultHelper.ApiResult<Tokens>));


        return services;
    }
}

