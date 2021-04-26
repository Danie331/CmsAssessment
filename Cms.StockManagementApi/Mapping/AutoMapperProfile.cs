using AutoMapper;
using System;
using Dto = Cms.StockManagementApi.Models;

namespace Cms.StockManagementApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Types.AuthenticationResult, Dto.LoginResult>();
            CreateMap<Dto.PaginationQuery, Types.PaginationFilter>();
            CreateMap<Dto.StockItem, Types.StockItem>().ReverseMap();
            CreateMap<Dto.StockAccessory, Types.StockAccessory>().ReverseMap();
            CreateMap<Dto.Image, Types.Image>().ForMember(i => i.Data, f => f.MapFrom(g => Convert.FromBase64String(g.Data)));
            CreateMap<Types.Image, Dto.Image>().ForMember(i => i.Data, f => f.MapFrom(g => Convert.ToBase64String(g.Data)));
        }
    }
}
