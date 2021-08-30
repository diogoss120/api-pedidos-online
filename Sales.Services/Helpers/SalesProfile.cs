using AutoMapper;
using Sales.Domain;
using Sales.Services.DTOs.Order;

namespace Sales.Services.Helpers
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<Order, OrderDto>();
        }

    }
}
