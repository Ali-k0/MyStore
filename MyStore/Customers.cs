using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public class Customers
    {
        private decimal totalPrice = 0;
        public string Name { get; }
        public string Password { get; }
        private List<Product> Cart { get; }
        private List<Customers> listC = new List<Customers>();

        private String pathFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public Customers()
        {
        }
            public Customers(string name, string password)
        {
            Name = name;
            Password = password;
            Cart = new List<Product>();
        }

        public void Save()
        {

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(pathFile, "customers.txt"), true))
            {
                outputFile.WriteLine(Name + ";" + Password);
                outputFile.Close();
            }

        }

        public List<Customers> LoadCustomer(){
            try{
                string[] lines = File.ReadAllLines(pathFile + "/customers.txt");
                for (int line = 0; line < lines.Length; line++)
                {
                    if (lines[line] != "")
                    {
                        String[] iststor = lines[line].Split(";");
                        listC.Add(new Customers(iststor[0], iststor[1]));
                    }
                }
            } catch (Exception e){
                Console.WriteLine("Exception: " + e.Message);
            }
            return listC;
        }
        public void AddToCart(Product product)
        {
            bool assigenTo = false;
            if (Cart.Any())
            {
                for (int i = 0; i < Cart.Count; i++)
                {
                    if (Cart[i].Name.Equals(product.Name))
                    {
                        Cart[i] = new Product(Cart[i].Name, Cart[i].Price, (Cart[i].Antal + product.Antal));
                        assigenTo = true;
                    }

                }
            }
            if (assigenTo == false && Cart.Any())
            {
                Cart.Add(product);
            }
            if (!Cart.Any())
            {
                Cart.Add(product);
            }

        }
        public bool ifExist(List<Customers> list, string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == name)
                {
                    return true;

                }

            }
            return false;
        }

        public bool chackPass( String password, string pass)
        {
            if (password == pass){
                return true;        
             }
            return false;
        }

        public void ToString()
        {
            Console.WriteLine("Hej " + Name + ": " + Password);


            Console.WriteLine(" Name " + "    Pris " + "  Antal  " + "  TotalPris");
            for (int i = 0; i < Cart.Count; i++)
            {
                Console.WriteLine(Cart[i].Name + "      " + Cart[i].Price + "       " + Cart[i].Antal + "       " + (Cart[i].Price * Cart[i].Antal));
               // totalPrice += (Cart[i].Price * Convert.ToDecimal(Cart[i].Antal));
            }
            Console.WriteLine("Total Pris är : " + totalpris());
        }

        private decimal totalpris()
        {
            totalPrice =0;
            for (int i = 0; i < Cart.Count; i++)
            {
              //  Console.WriteLine(Cart[i].Name + "      " + Cart[i].Price + "       " + Cart[i].Antal + "       " + (Cart[i].Price * Cart[i].Antal));
                totalPrice += (Cart[i].Price * Convert.ToDecimal(Cart[i].Antal));
            }
            return totalPrice;
        }
         public void kassa()
         {

            Console.WriteLine("Din belopp är :" + totalpris());
         }
    }
}
