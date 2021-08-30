using Sales.Domain;
using Sales.Persistence.Repositories.Entities;
using Sales.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork unitOfWork;
        public CustomerService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Customer> Add(Customer customer)
        {
            try
            {
                unitOfWork.CustomerRepository.Add(customer);
                if (await unitOfWork.CommitAsync())
                {
                    return await unitOfWork.CustomerRepository.GetCustomerByIdAsync(customer.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Customer>> CustomerListAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.CustomerListAsync();

                if (customers == null) return null;

                return customers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(int customerId)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(customerId);
                if (customer == null) throw new ArgumentException("Esse cliente não está cadastrado");

                unitOfWork.CustomerRepository.Delete(customer);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteRange(int[] customersId)
        {
            try
            {
                ICollection<Customer> customers = new List<Customer>();

                foreach (int id in customersId)
                {
                    customers.Add(await GetCustomerByIdAsync(id));
                }

                unitOfWork.CustomerRepository.DeleteRange(customers.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = unitOfWork.CustomerRepository.GetCustomerByIdAsync(id);
                if (customer == null) throw new ArgumentException("Esse cliente não exite na base de dados");

                return customer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Customer> Update(int customerid, Customer model)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(customerid);
                if (customer == null) return null;

                unitOfWork.CustomerRepository.Update(model);
                if (await unitOfWork.CommitAsync())
                {
                    return await unitOfWork.CustomerRepository.GetCustomerByIdAsync(model.Id);
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
