using System.Collections.Generic;
using AutoMapper;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;
using Ninject.Modules;

namespace MiniAmazon.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<AccountInputModel, Account>();
            Mapper.CreateMap<Account, AccountInputModel>();
            Mapper.CreateMap<CategoryInputModel, Category>();
            Mapper.CreateMap<Category, CategoryInputModel>();
        }
    }
}