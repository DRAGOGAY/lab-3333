using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliverySimulation
{
    public class Dish
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; } // в грамах
        public int Calories { get; set; }
        public List<string> Allergens { get; set; }

        public Dish(string name, string description, double price, double weight, int calories, List<string> allergens)
        {
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;
            Calories = calories;
            Allergens = allergens;
        }

        public override string ToString()
        {
            return $"{Name} ({Price} грн) - {Description}. Калорійність: {Calories} кал.";
        }
    }
    public class Restaurant
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public double Rating { get; set; }
        public List<Dish> Menu { get; set; }

        public Restaurant(string name, string address, string type, double rating)
        {
            Name = name;
            Address = address;
            Type = type;
            Rating = rating;
            Menu = new List<Dish>();
        }

        public void AddDish(Dish dish)
        {
            Menu.Add(dish);
        }

        public override string ToString()
        {
            return $"{Name} ({Type}), Рейтинг: {Rating}, Адреса: {Address}";
        }
    }

    public class Courier
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public double Rating { get; set; }
        public string TransportType { get; set; }

        public Courier(string name, string contact, double rating, string transportType)
        {
            Name = name;
            Contact = contact;
            Rating = rating;
            TransportType = transportType;
        }

        public override string ToString()
        {
            return $"{Name} ({TransportType}), Контакт: {Contact}, Рейтинг: {Rating}";
        }
    }

    public class Client
    {
        public string Name { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public List<Order> OrderHistory { get; set; }

        public Client(string name, string deliveryAddress, string contactNumber)
        {
            Name = name;
            DeliveryAddress = deliveryAddress;
            ContactNumber = contactNumber;
            OrderHistory = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            OrderHistory.Add(order);
        }

        public override string ToString()
        {
            return $"{Name}, Адреса доставки: {DeliveryAddress}, Контакт: {ContactNumber}";
        }
    }

    public class Order
    {
        public List<Dish> Dishes { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public Restaurant Restaurant { get; set; }
        public Courier Courier { get; set; }
        public Client Client { get; set; }

        public Order(List<Dish> dishes, Restaurant restaurant, Client client)
        {
            Dishes = dishes;
            Restaurant = restaurant;
            Client = client;
            TotalAmount = Dishes.Sum(d => d.Price);
            Status = "Створено";
        }

        public void UpdateStatus(string status)
        {
            Status = status;
        }

        public override string ToString()
        {
            return $"Замовлення від {Restaurant.Name} для {Client.Name}, Статус: {Status}, Загальна сума: {TotalAmount} грн.";
        }
    }

    public class DeliveryManager
    {
        public List<Courier> AvailableCouriers { get; set; }

        public DeliveryManager()
        {
            AvailableCouriers = new List<Courier>();
        }

public void AddCourier(Courier courier)
        {
            AvailableCouriers.Add(courier);
        }

        public Courier AssignCourier()
        {
            return AvailableCouriers.OrderByDescending(c => c.Rating).FirstOrDefault();
        }

        public void ProcessOrder(Order order)
        {
            Courier courier = AssignCourier();
            if (courier != null)
            {
                order.Courier = courier;
                order.UpdateStatus("У процесі доставки");
                Console.WriteLine($"Кур'єр {courier.Name} призначений для замовлення.");
            }
        }
    }

    public class MockTester
    {
        public List<Client> Clients { get; set; }
        public DeliveryManager DeliveryManager { get; set; }

        public MockTester()
        {
            Clients = new List<Client>();
            DeliveryManager = new DeliveryManager();
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public void CreateAndProcessRandomOrder()
        {
            Random random = new Random();

            Client client = Clients[random.Next(Clients.Count)];

            Restaurant restaurant = new Restaurant("Best Sushi", "Pyrihiva 9", "Ukraine", 4.5);
            restaurant.AddDish(new Dish("Sushi Roll", "Delicious sushi roll", 120, 200, 250, new List<string> { "Fish", "Rice" }));
            restaurant.AddDish(new Dish("Golybtsi", "spec Stravy", 150, 300, 350, new List<string> { "Cabbage", "Meat" }));

            List<Dish> selectedDishes = new List<Dish> { restaurant.Menu[random.Next(restaurant.Menu.Count)] };

            Order order = new Order(selectedDishes, restaurant, client);
            client.AddOrder(order);

         
            DeliveryManager.ProcessOrder(order);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
     
            Client client1 = new Client("Олег", "Kyiv, 23", "097-454-7890");
            Client client2 = new Client("Марія", "Lviv, 45", "098-765-4321");

          
            Courier courier1 = new Courier("Олександр", "23fmif.o.ziabriev@std.udu.edu", 4.8, "F16");
            Courier courier2 = new Courier("Іван", "gachimuchi@gmaik.com", 4.6, "Car");

            DeliveryManager deliveryManager = new DeliveryManager();
            deliveryManager.AddCourier(courier1);
            deliveryManager.AddCourier(courier2);

     
            MockTester tester = new MockTester();
            tester.AddClient(client1);
            tester.AddClient(client2);
            tester.DeliveryManager = deliveryManager;

            tester.CreateAndProcessRandomOrder();
        }
    }
}
