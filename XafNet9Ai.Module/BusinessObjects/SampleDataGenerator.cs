using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafNet9Ai.Module.BusinessObjects
{
    public class SampleDataGenerator
    {
        public static void CreateSampleData(Session session)
        {
            // Create Regions and Cities
            var regions = new[]
            {
            new { Region = "North", Cities = new[] { "New York", "Boston" } },
            new { Region = "South", Cities = new[] { "Miami", "Atlanta" } },
            new { Region = "West", Cities = new[] { "Los Angeles", "San Francisco" } }
        };

            // Create Products
            var products = new[]
            {
            new { Code = "LAPTOP", Name = "Business Laptop", Cost = 800m, Price = 1200m },
            new { Code = "PHONE", Name = "Smartphone", Cost = 400m, Price = 800m },
            new { Code = "TABLET", Name = "Tablet Pro", Cost = 300m, Price = 600m },
            new { Code = "MONITOR", Name = "4K Monitor", Cost = 200m, Price = 400m },
            new { Code = "PRINTER", Name = "Color Printer", Cost = 150m, Price = 300m }
        };

            var productObjects = new Dictionary<string, Product>();
            foreach (var p in products)
            {
                var product = new Product(session)
                {
                    Code = p.Code,
                    Name = p.Name,
                    UnitCost = p.Cost,
                    UnitPrice = p.Price
                };
                product.Save();
                productObjects[p.Code] = product;
            }

            // Create Customers and Invoices
            var random = new Random(123); // Fixed seed for reproducible data
            var customers = new List<Customer>();

            foreach (var r in regions)
            {
                foreach (var city in r.Cities)
                {
                    // Create 2 customers per city
                    for (int i = 1; i <= 2; i++)
                    {
                        var customer = new Customer(session)
                        {
                            Name = $"Customer {city} {i}",
                            Region = r.Region,
                            City = city
                        };
                        customer.Save();
                        customers.Add(customer);
                    }
                }
            }

            // Generate invoices for the last 12 months
            var startDate = DateTime.Now.AddMonths(-11).Date;
            var invoiceNumber = 1;

            foreach (var month in Enumerable.Range(0, 12))
            {
                var currentDate = startDate.AddMonths(month);

                foreach (var customer in customers)
                {
                    // Generate 1-3 invoices per customer per month
                    var invoicesThisMonth = random.Next(1, 4);

                    for (int i = 0; i < invoicesThisMonth; i++)
                    {
                        var invoice = new Invoice(session)
                        {
                            Number = $"INV-{invoiceNumber:D5}",
                            Date = currentDate.AddDays(random.Next(0, 28)),
                            Customer = customer
                        };
                        invoice.Save();

                        // Add 1-3 products to each invoice
                        var productsCount = random.Next(1, 4);
                        var selectedProducts = products.OrderBy(x => random.Next()).Take(productsCount);

                        foreach (var product in selectedProducts)
                        {
                            var quantity = random.Next(1, 5);
                            // Occasionally apply small discount
                            var priceVariation = product.Price * (1 - random.Next(0, 15) / 100m);

                            var detail = new InvoiceDetail(session)
                            {
                                Invoice = invoice,
                                Product = productObjects[product.Code],
                                Quantity = quantity,
                                UnitPrice = priceVariation
                            };
                            detail.Save();
                        }

                        invoiceNumber++;
                    }
                }
            }

            session.CommitTransaction();
        }
    }
}
