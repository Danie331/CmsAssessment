using AutoMapper;
using DataModel = Cms.Data.DAL.Context.Models;

namespace Cms.Data.DAL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DataModel.Image, Types.Image>();
            CreateMap<Types.Image, DataModel.Image>().ForMember(i => i.StockItem, g => g.Ignore());

            CreateMap<DataModel.StockAccessory, Types.StockAccessory>();
            CreateMap<Types.StockAccessory, DataModel.StockAccessory>().ForMember(i => i.StockItem, g => g.Ignore());

            CreateMap<DataModel.StockItem, Types.StockItem>().ReverseMap();
        }
    }
}
