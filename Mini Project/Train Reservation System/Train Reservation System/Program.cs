using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Train_Reservation_System.User;
using Train_Reservation_System.Admin;

namespace Train_Reservation_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Account();
          ;
            Console.WriteLine("\n\n\n\t\t\tKripya ek baar aur button daba de, Dhanyavaad Aapka Din Shubh ho!!!");

            Console.ReadLine();
           
        }

        static void Account()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\t\t\t\t-----  Welcome To My Train Reservation App  -----\n\n\n\n\n");

                Console.WriteLine("\n\t\t\t\t1.Admin\n\n\t\t\t\t2.User\n\n\t\t\t\t3.Exit\n");
                Console.Write("\n\t\t\t\tChoose an Option: ");

                try
                {
                    int opt = int.Parse(Console.ReadLine());

                    switch (opt)
                    {
                        case 1:
                            Console.Clear();
                          

                            // Implement admin login logic
                            AdminClass.existing_admin();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("\n\t\t\t\t-----  User Log In  -----\n\n\n\n");
                            Console.WriteLine("\n\t\t\t\t" +
                                "a.Existing User" +
                                "\n\n\t\t\t\t" +
                                "b.New User");
                            try
                            {
                                Console.Write("\n\n\t\t\t\tchoose an option: ");
                                char user_opt = Convert.ToChar(Console.ReadLine());


                                switch (user_opt)
                                {
                                    case 'a':
                                        Console.Clear();
                                       UserClass.existing_user();
                                        break;
                                    case 'b':
                                        Console.Clear();
                                       UserClass.Creating_New_User_Account();
                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("\n\t\t\t\tEnter valid option\n\n");
                                        break;
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("\n\t\t\t\tError: " + e.Message+"\n\n");
                            }


                            break;

                        case 3:

                            flag = false;
                            break;

                        default:

                            Console.WriteLine("\n\nChoose valid Option!!");
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("\nError: " + e.Message + "\n\n");
                }
            }
        }

    }

}
