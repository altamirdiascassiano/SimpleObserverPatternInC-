using System;
using System.Collections.Generic;

namespace SimpleObserverPatternInCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // instance of the ProductList and Customer
            var waitingList = new ProductWaitingList("Happy new year");     
            var customer = new Customer("Altamir Dias");
            var customerB = new Customer("Amanda Cunha");
            var customerC = new Customer("Marcos Lucas");
            var customerVip = new VipCustomer("Carlos Da Silva");
            
            // Customers subscribe the Happy new year list 
            waitingList.Attach(customer);
            waitingList.Attach(customerB);
            waitingList.Attach(customerC);
            waitingList.Attach(customerVip);

            waitingList.ReceivedNewProduct();
            // When the customer won't get more notification of the new product
            waitingList.Detach(customerC);
            // In this point the customer C ( Marcos Lucas) do not wnont receive more notification of the new products
            waitingList.ReceivedNewProduct();

            Console.ReadKey();
        }
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
    public interface IObserver
    {
        void Update(ISubject subject);
    }
    public class ProductWaitingList : ISubject
    {
        public string Name { get; private set; }
        readonly List<IObserver> observers;
        public ProductWaitingList(string name)
        {
            observers = new List<IObserver>();
            this.Name = name;
        }

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Customer adicionado!");
            Console.WriteLine();
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Console.WriteLine("Customer removed!");
            Console.WriteLine();
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
                observer.Update(this);
        }
        public void ReceivedNewProduct()
        {
            Console.WriteLine("The new product received!", Name);
            Console.WriteLine();
            Notify();
        }
    }
    public class Customer : IObserver
    {
        public string Name { get; private set; }

        public Customer(string name)
        {
            Name = name;
        }

        public void Update(ISubject subject)
        {
            Console.WriteLine("The Customer {0} received a notification!", Name);
            Console.WriteLine();
        }
    }
    public class VipCustomer: IObserver
    {
        public string Name { get; private set; }

        public VipCustomer(string name)
        {
            Name = name;
        }
        public void  Update(ISubject subject){
            Console.WriteLine("Hello {0}. You are my VIP customer and a list  {1} received a great product for you!", this.Name, (subject as ProductWaitingList).Name);
            Console.WriteLine();
        }
    }
}
