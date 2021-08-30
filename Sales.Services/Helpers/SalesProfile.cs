using AutoMapper;
using Sales.Domain;
using Sales.Services.DTOs;

namespace Sales.Services.Helpers
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }

    }
}
