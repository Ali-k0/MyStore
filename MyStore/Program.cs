using System.Xml.Linq;

namespace MyStore
{
    internal class Program
    {
        private static List<Customers> customers = new List<Customers>();
        private static List<Product> prods = new List<Product>();

        static void Main(string[] args)
        {
            fillProducts();
            fillCustomers();
            try { 
            while (true)
            {
                Console.WriteLine("1- Registrera dig \n2- Logga in\n3- quit");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            registerNykund();
                            break;
                        case 2:
                             LogIn();
                            break;
                        case 3:
                            Console.WriteLine("Hej Då!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                }
            }

        } catch (Exception ioex) {
              Console.WriteLine("Please Enter correct input/information! ");
            }
}
        static public void registerNykund()
        {
            Console.WriteLine("Ange ditt namn: ");
            String name = Console.ReadLine();

            Console.WriteLine("Ange lösenord: ");
            String pass = Console.ReadLine();
            Customers customer = new Customers();

            if (customer.ifExist(customers, name))
            {
                Console.WriteLine("This account is already exit ");

            }
            else
            {
                customer = new Customers(name, pass);
                
                customers.Add(customer);
                customer.Save();
                Console.WriteLine($"Kund {name} registreringen är lyckades.");
            }
        }

        private static void LogIn()
        {
            Console.Write("Ange ditt namn: ");
            string name = Console.ReadLine();
            Console.Write("Ange lösenord: ");
            string password = Console.ReadLine();

            var customer = customers.Find(c => c.Name == name);

            if (customer == null)
            {
                Console.WriteLine("Customer not found. Please Sign up first!!");
                return;
            }

            if (customer.chackPass(customer.Password, password))
            {
                Console.WriteLine($"Welcome, {customer.Name}!");
                startKöpa(customer);
            }
            else
            {
                Console.WriteLine("Invalid password. Please try again.");
            }  
        }

        static private void fillCustomers()
        {
            Dictionary<String, String> KundList = new Dictionary<String, String>();
            KundList.Add("Knatte", "123");
            KundList.Add("Fnatte", "321");
            KundList.Add("Tjatte", "213");
            Customers ny = new Customers();
            customers = ny.LoadCustomer();
            foreach (var kvp in KundList)
            {
                ny = new Customers(kvp.Key, kvp.Value);
                if (!ny.ifExist(customers,kvp.Key)) { 
                        ny.Save();
                        customers.Add(ny);
            }
            }

        } 
        

        static private void fillProducts()
        {

            prods.Add(new Product("banan", 2));
            prods.Add(new Product("äpple", 7));
            prods.Add(new Product("päron", 4));
        }
        static private void showProducts() {
                Console.WriteLine("Tillgängliga produkter:");
                for (int i = 0; i < prods.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {prods[i].Name} - Price: {prods[i].Price:C}");
                }
        }
        static private void startKöpa(Customers nykund)
        {
            while (true)
            {
                Console.WriteLine("1- Handla.  \n2- Kundvagn  \n3- Kassan \n4- Logga out");
                int choise;
               
                if (int.TryParse(Console.ReadLine(), out choise))
                {
                    switch (choise)
                    {

                        case 1:
                            try {
                                while (true)
                                {
                                    showProducts();
                                    Console.WriteLine("Ange numret på det önskade vara eller 0 för att avbryta.");
                                    int selectItem = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Ange antal eller 0 för att avbryta");
                                    int antal = Convert.ToInt32(Console.ReadLine());

                                    if (selectItem > 0 && selectItem <= prods.Count())
                                    {
                                        nykund.AddToCart(new Product(prods[selectItem-1].Name, prods[selectItem-1].Price, antal));

                                    }else if(selectItem == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ogiltigt produktnummer. ");
                                    }

                                }
                                } catch (Exception ioex) {
                                Console.WriteLine("Vänligen ange korrekt input/information! ");
                            }
                    

                            break;
                        case 2:
                            nykund.ToString();
                            break;

                        case 3:
                            nykund.kassa();
                            break;
                        case 4:
                            Console.WriteLine($"Logging out {nykund.Name}.");
                            return;
                           
                        default:
                            Console.WriteLine("Ogiltigt alternativ. Var god försök igen!.");
                            break;


                    }
                }

            }
        }

    }
}