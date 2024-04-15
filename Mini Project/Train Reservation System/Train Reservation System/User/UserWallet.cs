using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Train_Reservation_System.User
{
    public class UserWallet
    {
        private readonly Train_Reservation_System_Entities db;

        public UserWallet(Train_Reservation_System_Entities db)
        {
            this.db = db;
        }

        // Method to manage user's wallet
        public void user_wallet(UserAccount user)
        {
            Console.Clear();
            bool exit = true;

            while (exit)
            {
                Console.WriteLine("\n\t\t\t\t------  Wallet Menu  ------\n\n\n\n");
                Console.WriteLine("\n\t\t\t\t1. Add Money");
                Console.WriteLine("\n\t\t\t\t2. Show Information");
                Console.WriteLine("\n\t\t\t\t3. Exit");
                Console.Write("\n\n\t\t\t\tEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("\n\t\t\t\tEnter amount to add: ");
                        float amount = Convert.ToSingle(Console.ReadLine());
                        AddMoneyToWallet(user.user_id, user.username, amount);
                        break;
                    case "2":
                        ShowWalletInformation(user.user_id);
                        break;
                    case "3":
                        exit = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("\n\n\t\t\t\t-----  Invalid choice. Please enter a number from 1 to 3 ------");
                        exitLoop();
                        break;
                }

            }
        }

        // Method to add money to user's wallet
        void AddMoneyToWallet(int userId, string userName,float amount)
        {
            try
            {
                var wallet = db.Wallets.FirstOrDefault(w => w.user_id == userId);

                if (wallet == null)
                {
                    // If the user doesn't have a wallet, create a new one
                    wallet = new Wallet { user_id = userId, user_name = userName, balance = amount };
                    db.Wallets.Add(wallet);
                }
                else
                {
                    // If the user already has a wallet, update the balance
                    wallet.balance += amount;
                }

                db.SaveChanges();

                Console.WriteLine($"\n\n\t\t\t\t------  Added {amount} to the wallet. New balance: {wallet.balance}  -------");
                exitLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n\t\t\t\t------  An error occurred while adding money to the wallet: {ex.Message}  -------");
                exitLoop();
            }
        }

        // Method to show wallet information for a user
        void ShowWalletInformation(int userId)
        {
            try
            {
                var wallet = db.Wallets.FirstOrDefault(w => w.user_id == userId);
                if (wallet == null)
                {
                    Console.WriteLine("\n\n\t\t\t\t------  Wallet not found for the user !!  ------");
                    exitLoop();
                    return;
                }

                Console.WriteLine($"\n\t\t\t\tWallet Information for User ID: {userId}");
                Console.WriteLine($"\n\t\t\t\tUser Name: {wallet.user_name}");
                Console.WriteLine($"\n\t\t\t\tBalance: {wallet.balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\t\t\t\tAn error occurred while fetching wallet information: {ex.Message}");
                exitLoop();
            }

           
        }

        // Method to clear console after some delay
        static void exitLoop()
        {
            for (int i = 2; i >= 0; i--)
            {
                Thread.Sleep(1200);
            }
            Console.Clear();
        }
    }
}