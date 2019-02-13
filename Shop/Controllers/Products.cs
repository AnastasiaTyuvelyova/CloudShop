using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private const string jsonFileName = "goods.json";

        private IHostingEnvironment hostingEnvironment;

        private List<Item> items;

        public ProductsController(IHostingEnvironment env)
        {
            this.hostingEnvironment = env;
            this.items = this.GetListOfItems();
        }

        public void AddItemToOrder(int itemId)
        {
            Item item = this.items.Find(i => i.Id == itemId);
            Order.TotalSum += item.Price;
            Order.ItemsInOrder.Add(item);
        }

        public void RemoveItemFromOrder(int itemId)
        {
            Item item = this.items.Where(i => i.Id == itemId).FirstOrDefault();
            Order.TotalSum -= item.Price;
            Order.ItemsInOrder.RemoveAll(i => i.Id == itemId); 
        }

        public double GetTotalSum()
        {
            return Order.TotalSum;
        }

        public bool CheckCurrency()
        {
            bool areRubles = Order.ItemsInOrder.All(item => item.Currency == Currencies.RUR);
            bool areDollars = Order.ItemsInOrder.All(item => item.Currency == Currencies.USD);
            bool areEuros = Order.ItemsInOrder.All(item => item.Currency == Currencies.EUR);
            return areRubles || areDollars || areEuros || false;
        }

        public IActionResult Index()
        {
            return View(this.items);
        }

        public IActionResult TotalSum()
        {
            return PartialView("TotalSum", Order.TotalSum);
        }

        private List<Item> GetListOfItems()
        {
            string contentRootPath = this.hostingEnvironment.ContentRootPath;
            string jsonPath = string.Format("{0}/{1}", contentRootPath, jsonFileName);

            List<Item> listOfItems = new List<Item>();
            using (StreamReader sr = new StreamReader(jsonPath))
            {
                string jsonGoods = sr.ReadToEnd();
                try
                {
                    listOfItems = JsonConvert.DeserializeObject<List<Item>>(jsonGoods);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return listOfItems;
        }
    }
}
