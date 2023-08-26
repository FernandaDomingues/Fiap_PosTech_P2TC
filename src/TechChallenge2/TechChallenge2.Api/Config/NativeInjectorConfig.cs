using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechChallenge2.Application.Services;
using TechChallenge2.Data.Context;
using TechChallenge2.Data.Repositories;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Request;
using TechChallenge2.Domain.Interfaces.Repositories;
using TechChallenge2.Domain.Interfaces.Services;

namespace TechChallenge.Api.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public static class NativeInjectorConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection strings
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TechChallengeConnection")));

            //Auto Mapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NoticiaRequest, Noticia>().ReverseMap();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Repositórios
            services.AddScoped<INoticiaRepository, NoticiaRepository>();

            // Services
            services.AddScoped<INoticiaService, NoticiaService>();


            return services;
        }
    }
}