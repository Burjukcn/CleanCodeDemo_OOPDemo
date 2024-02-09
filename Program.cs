using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IproductService productService = new ProductManager(new CentralBankServiceAdapter());
            productService.Sell(new Product { Id = 1,Name="Laptop",Price=1000 }, new Customer { Id = 1, Name = "Engin", IsStudent=
                true, IsOfficers = false }); ;
        }
    }

    class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStudent { get; set; }
        public bool IsOfficers {  get; set; }
    }
    interface IEntity
    {

    }
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
    }
    class ProductManager : IproductService
    {
        private IBankService _bankService;
        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }
        public void Sell(Product product, Customer customer)
        {   
            decimal price = product.Price;
            if (customer.IsStudent)
            {
                price =product.Price * (decimal)0.90;
            }
            if (customer.IsOfficers)
            {
                price = product.Price * (decimal)0.80;
            }


            price= _bankService.ConvertRate(new CurrencyRate { Currency = 1, Price = price });
            Console.WriteLine(price);
            Console.ReadLine();

        }
    }
    internal interface IproductService
    {
        void Sell(Product product,Customer customer);//Sat
    }


    class FakeBankService :IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price /  (decimal) 5.30;
        } 
    }
    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }

    class CurrencyRate
        {
            public decimal Price { get; set; }
            public int Currency { get; set; }
    
        }

    class CentralBankServiceAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            CentralBankService  centralBankService = new CentralBankService();
            return centralBankService.ConvertCurrency(currencyRate);
        }
    }


    // Merkez bankasının kodu gibi düşünün 
    class CentralBankService
    {
        public decimal ConvertCurrency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal) 5.28;
        }
    }
  }



 

