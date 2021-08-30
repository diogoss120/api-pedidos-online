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
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CustomerDto> Add(CustomerDto customerDto)
        {
            try
            {
                var customer = mapper.Map<Customer>(customerDto);
                unitOfWork.CustomerRepository.Add<Customer>(customer);
                if (await unitOfWork.CommitAsync())
                {
                    var model = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(customer.Id);
                    return mapper.Map<CustomerDto>(model);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CustomerDto>> CustomerListAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.CustomerListAsync();

                if (customers == null) return null;

                return mapper.Map<CustomerDto[]>(customers);
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
                    customers.Add(await unitOfWork.CustomerRepository.GetCustomerByIdAsync(id));
                }

                unitOfWork.CustomerRepository.DeleteRange(customers.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(id);
                if (customer == null) throw new ArgumentException("Esse cliente não exite na base de dados");

                return mapper.Map<CustomerDto>(customer);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CustomerDto> Update(int customerid, CustomerDto customerDto)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(customerid);
                if (customer == null) return null;

                var model = mapper.Map<Customer>(customerDto);
                unitOfWork.CustomerRepository.Update<Customer>(model);
                if (await unitOfWork.CommitAsync())
                {
                    var response = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(model.Id);
                    return mapper.Map<CustomerDto>(response);
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
