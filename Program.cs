using System;

namespace Assignment2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                string choice;
                Huffman value;
                System.Console.WriteLine("Choose a string to be encoded.");
                System.Console.WriteLine("Enter an empty string value to exit.");
                while(true)
                {
                    System.Console.Write("String: ");
                    try 
                    {
                        choice = System.Console.ReadLine();
                        System.Console.WriteLine("");
                        if (choice == ""){Environment.Exit(0);}
                        value = new Huffman(choice);
                        string valueencoded = value.Encode(choice);
                        string valuedecoded = value.Decode(valueencoded);
                        System.Console.WriteLine("Encoded value:\n{0}\n", valueencoded);
                        System.Console.WriteLine("Decoded value:\n{0}\n", valuedecoded);
                    }
                    catch
                    {
                        System.Console.WriteLine("Error, not a valid string (a-z, A-Z)\n");
                    }
                    
                    
                }
            }
        }   
    }
}
