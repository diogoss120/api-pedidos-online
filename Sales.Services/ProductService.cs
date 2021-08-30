using Sales.Domain;
using Sales.Persistence.Repositories.Entities;
using Sales.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> Add(Product product)
        {
            try
            {
                unitOfWork.ProductRepository.Add(product);
                if (await unitOfWork.CommitAsync())
                {
                    return await unitOfWork.ProductRepository.GetProductByIdAsync(product.Id);
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
                    products.Add(await GetProductByIdAsync(id));
                }

                unitOfWork.ProductRepository.DeleteRange(products.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdAsync(id);
                if (product == null) throw new ArgumentException("Esse produto não existe no banco de dados");

                return product;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Product>> ProductListAsync()
        {
            try
            {
                var products = await unitOfWork.ProductRepository.ProductListAsync();
                if (products == null) return null;

                return products;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Product> Update(int productId, Product model)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
                if (product == null) return null;

                unitOfWork.ProductRepository.Update(model);
                if (await unitOfWork.CommitAsync())
                {
                    return await unitOfWork.ProductRepository.GetProductByIdAsync(model.Id);
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
