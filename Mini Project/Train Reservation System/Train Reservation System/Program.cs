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
        static Train_Reservation_System_Entities db = new Train_Reservation_System_Entities(); 
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
                            AdminClass admin = new AdminClass(db);
                            admin.existing_admin();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("\n\t\t\t\t-----  User Section  -----\n\n\n\n");
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
                                        UserClass user1 = new UserClass(db);
                                        user1.existing_user();
                                        break;
                                    case 'b':
                                        Console.Clear();
                                        UserClass user2 = new UserClass(db);
                                        user2.Creating_New_User_Account();
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
                                exitLoop();
                            }


                            break;

                        case 3:

                            flag = false;
                            break;

                        default:

                            Console.WriteLine("\n\n\t\t\t\t\tChoose valid Option!!");
                            exitLoop();
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("\n\t\t\t\tError: " + e.Message + "\n\n");
                    exitLoop();
                }
            }
        }
        static void exitLoop()
        {
            for (int i = 1; i >= 0; i--)
            {
                Thread.Sleep(1000);
            }
            Console.Clear();
        }
    }

}
