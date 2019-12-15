using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree
{
    enum Color
    {
        Red,
        Black
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();

            int choose;
            Console.WriteLine("1 - Insert, 2 - Delete, 3  - Show ");

            while (true)
            {
                choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)

                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Insert, 2 - Delete, 3  - Show \n\n\n");
                            Console.WriteLine("Enter value of a new element: ");

                            Console.WriteLine("Value:");
                            int value = Convert.ToInt32(Console.ReadLine());

                            tree.Insert(value);

                            Console.WriteLine("Inserted");
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");

                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Insert, 2 - Delete, 3  - Show \n\n\n");
                            Console.WriteLine("Which one do you want to delete?");

                            Console.WriteLine("Value:");
                            int value = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine($"{value}");

                            tree.Delete(value);
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@");

                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Insert, 2 - Delete, 3  - Show \n\n\n");                            

                            Console.WriteLine(tree);


                            break;
                        }
                }
            }
        }

    }
}

