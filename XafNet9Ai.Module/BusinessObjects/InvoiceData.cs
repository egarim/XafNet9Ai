using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafNet9Ai.Module.BusinessObjects
{
   

    [DefaultClassOptions]
    public class Customer : BaseObject
    {
        public Customer(Session session) : base(session) { }

        private string name;
        [Size(100)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        private string region;
        [Size(50)]
        public string Region
        {
            get => region;
            set => SetPropertyValue(nameof(Region), ref region, value);
        }

        private string city;
        [Size(50)]
        public string City
        {
            get => city;
            set => SetPropertyValue(nameof(City), ref city, value);
        }

        [Association("Customer-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get => GetCollection<Invoice>(nameof(Invoices));
        }
    }

    [DefaultClassOptions]
    public class Product : BaseObject
    {
        public Product(Session session) : base(session) { }

        private string code;
        [Size(20)]
      
        public string Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }

        private string name;
        [Size(100)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        private decimal unitCost;
        public decimal UnitCost
        {
            get => unitCost;
            set => SetPropertyValue(nameof(UnitCost), ref unitCost, value);
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value);
        }

        [Association("Product-InvoiceDetails")]
        public XPCollection<InvoiceDetail> InvoiceDetails
        {
            get => GetCollection<InvoiceDetail>(nameof(InvoiceDetails));
        }
    }

    [DefaultClassOptions]
    public class Invoice : BaseObject
    {
        public Invoice(Session session) : base(session) { }

        private string number;
        [Size(20)]
       
        public string Number
        {
            get => number;
            set => SetPropertyValue(nameof(Number), ref number, value);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }

        private Customer customer;
        [Association("Customer-Invoices")]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(nameof(Customer), ref customer, value);
        }

        [Association("Invoice-Details")]
        public XPCollection<InvoiceDetail> Details
        {
            get => GetCollection<InvoiceDetail>(nameof(Details));
        }

        // Calculated fields for analysis
        [PersistentAlias("Details.Sum(Amount)")]
        public decimal TotalAmount
        {
            get => (decimal)(EvaluateAlias(nameof(TotalAmount)) ?? 0m);
        }

        [PersistentAlias("Details.Sum(Cost)")]
        public decimal TotalCost
        {
            get => (decimal)(EvaluateAlias(nameof(TotalCost)) ?? 0m);
        }

        [PersistentAlias("TotalAmount - TotalCost")]
        public decimal Margin
        {
            get => (decimal)(EvaluateAlias(nameof(Margin)) ?? 0m);
        }
    }

    [DefaultClassOptions]
    public class InvoiceDetail : BaseObject
    {
        public InvoiceDetail(Session session) : base(session) { }

        private Invoice invoice;
        [Association("Invoice-Details")]
        public Invoice Invoice
        {
            get => invoice;
            set => SetPropertyValue(nameof(Invoice), ref invoice, value);
        }

        private Product product;
        [Association("Product-InvoiceDetails")]
        public Product Product
        {
            get => product;
            set => SetPropertyValue(nameof(Product), ref product, value);
        }

        private decimal quantity;
        public decimal Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value);
        }

        [PersistentAlias("Quantity * UnitPrice")]
        public decimal Amount
        {
            get => (decimal)(EvaluateAlias(nameof(Amount)) ?? 0m);
        }

        [PersistentAlias("Quantity * Product.UnitCost")]
        public decimal Cost
        {
            get => (decimal)(EvaluateAlias(nameof(Cost)) ?? 0m);
        }

        [PersistentAlias("Amount - Cost")]
        public decimal Margin
        {
            get => (decimal)(EvaluateAlias(nameof(Margin)) ?? 0m);
        }
    }
}