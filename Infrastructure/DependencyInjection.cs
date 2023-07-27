﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Data;
using Infrastructure.Repositories.AuthRepo;
using Infrastructure.Repositories.UserRepo;
using Infrastructure.Repositories.BookingRepo;
using Infrastructure.Repositories.PlaceRepo;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJWTManagerRepository, JWTManagerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbSession>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));
            return services;
        }
    }
}

