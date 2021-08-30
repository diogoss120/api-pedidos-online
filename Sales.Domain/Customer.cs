using Sales.Domain.Enums;
using System;

namespace Sales.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public Gender Gender { get; set; }
    }
}
