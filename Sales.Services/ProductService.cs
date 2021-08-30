using AutoMapper;
using Sales.Domain;
using Sales.Persistence.Repositories.Entities;
using Sales.Services.Contracts;
using Sales.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductDto> Add(ProductDto productDto)
        {
            try
            {
                var product = mapper.Map<Product>(productDto);
                unitOfWork.ProductRepository.Add<Product>(product);
                if (await unitOfWork.CommitAsync())
                {
                    var response = await unitOfWork.ProductRepository.GetProductByIdAsync(product.Id);
                    return mapper.Map<ProductDto>(response);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(int productId)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
                if (product == null) throw new ArgumentException("Esse produto não existe no banco de dados");

                unitOfWork.ProductRepository.Delete(product);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteRange(int[] productsId)
        {
            try
            {
                ICollection<Product> products = new List<Product>();

                foreach (int id in productsId)
                {
                    products.Add(await unitOfWork.ProductRepository.GetProductByIdAsync(id));
                }

                unitOfWork.ProductRepository.DeleteRange(products.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdAsync(id);
                if (product == null) return null;

                return mapper.Map<ProductDto>(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> ProductListAsync()
        {
            try
            {
                var products = await unitOfWork.ProductRepository.ProductListAsync();
                if (products == null) return null;

                return mapper.Map<ProductDto[]>(products);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ProductDto> Update(int productId, ProductDto productDto)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
                if (product == null) return null;

                var model = mapper.Map<Product>(productDto);
                unitOfWork.ProductRepository.Update<Product>(model);
                if (await unitOfWork.CommitAsync())
                {
                    var response = await unitOfWork.ProductRepository.GetProductByIdAsync(model.Id);
                    return mapper.Map<ProductDto>(response);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
