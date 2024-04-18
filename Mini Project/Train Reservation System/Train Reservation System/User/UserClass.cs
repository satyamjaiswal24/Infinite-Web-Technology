using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Train_Reservation_System.User
{
    public class UserClass
    {
        static Train_Reservation_System_Entities db;

        public UserClass(Train_Reservation_System_Entities udb)
        {
            db = udb;
        }


        // Method for creating a new user account
        public void Creating_New_User_Account()
        {
            Console.WriteLine("\n\n\n\n\t\t\t\t\t------  Creating New User  ------\n\n\n\n\n");
            Console.Write("\t\t\t* Create your User Name: ");
            string username = Console.ReadLine();
            Console.Write("\n\t\t\t* Create your Email ID: ");
            string email = Console.ReadLine();

            bool flag = true;
            string password, conpassword;
            do
            {
                Console.Write("\n\t\t\t* Create your Password: ");
                password = Console.ReadLine();
                Console.Write("\n\t\t\t* Confirm your Password: ");
                conpassword = Console.ReadLine();

                if (password.Equals(conpassword))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("\n\t\t\t* Password dosen't match");
                }
            } while (flag);

            Console.Write("\n\t\t\t* Enter your age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t* Enter your Phone Number: ");
            string phone_no = Console.ReadLine();

            Console.Write("\n\t\t\t* Enter your Address: ");
            string address = Console.ReadLine();

            var new_account = new UserAccount
            {
                username = username,
                user_email = email,
                user_password = password,
                age = age,
                phone_number = phone_no,
                address = address,
            };


            try
            {
                db.UserAccounts.Add(new_account);
                db.SaveChanges();

                // After saving the user account, retrieve the newly created user ID
                int userId = new_account.user_id;

                // Create a corresponding entry in the Wallet table with a balance of 0
                var new_wallet = new Wallet
                {
                    user_id = userId,
                    user_name = username,
                    balance = 0
                };

                db.Wallets.Add(new_wallet);
                db.SaveChanges();

                Console.WriteLine("\n\n\t\t\t\t\t------  User registration successful  ------\n\n");
                exitLoop();

            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n\t\t\t\tError: " + e.Message);
                exitLoop();
            }
        }

        // Method for existing user login
        public void existing_user()
        {
            Console.WriteLine("\n\t\t\t\t\t------  User Login Section  ------\n\n\n");

            bool flag = true;
            Console.WriteLine("\n\n\n\n");
            Console.Write("\t\t\t\tEnter your Email ID: ");
            string email = Console.ReadLine();
            Console.Write("\n\t\t\t\tEnter your Password: ");
            string password = Console.ReadLine();

            var user = db.UserAccounts.FirstOrDefault(a => a.user_email == email && a.user_password == password);

            if (user != null)
            {
                // Admin logged in successfully, proceed with admin functionalities
                Console.WriteLine("\n\n\t\t\t\t\t------  User Login Successfully  ------\n\n\n");
                exitLoop();

                while (flag)
                {
                    Console.WriteLine("\n\t\t\t\t\t------  User Section  ------\n\n\n\n");
                    Console.WriteLine("\t\t\t\t" +
                        "1. Book Tickets" +
                        "\n\t\t\t\t" +
                        "2. Cancel Tickets" +
                        "\n\t\t\t\t" +
                        "3. Show all Trains" +
                        "\n\t\t\t\t" +
                        "4. ShowBooking/Cancellation History" +
                        "\n\t\t\t\t" +
                        "5. My Profile" +
                        "\n\t\t\t\t" +
                        "6. Wallet" +
                        "\n\t\t\t\t" +
                        "7. Log Out");
                    try
                    {
                        Console.Write("\n\t\t\t\tChoose an option: ");
                        int opt = int.Parse(Console.ReadLine());

                        switch (opt)
                        {
                            case 1:
                                Console.WriteLine("\n\n\n");
                                User_booking_details(user.user_id);
                                db.SaveChanges();
                                break;

                            case 2:
                                // Cancel Ticket
                                Console.Write("Enter PNR Number: ");
                                int pnrNo = int.Parse(Console.ReadLine());
                                CancelBooking(user.user_id, pnrNo);
                                break;

                            case 3:
                                showAllTrains();
                                break;

                            case 4:
                                //show booking/Cancellation History
                                ShowBookingCancellationHistory(user.user_id);
                                break;

                            case 5:

                                // My Profile Section 

                                myProfile(email, password);
                                break;

                            case 6:
                                // Wallet Section 
                                UserWallet wallet = new UserWallet(db);
                                wallet.user_wallet(user);
                                break;

                            case 7:
                                flag = false;
                                Console.WriteLine("\n\n\n\t\t\t\t----- Log Out Successfully  -----\t\t\t\t\n\n\n\n");
                                exitLoop();
                                break;

                            default:
                                Console.WriteLine("\n\t\t\t\t-----  Enter a valid option  -----\n\n");
                                exitLoop();
                                break;
                        }


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n\t\t\t\tError" + e.Message);
                        exitLoop();
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t\t\t-----  User doesn't exists  -----\n\n\n");
                exitLoop();
            }
        }


        // Method to display available trains

        private static void showAllTrains()
        {
            // Retrieving and displaying available trains from the database

            var activeTrains = db.Trains.Where(t => t.is_active == true).ToList();
            Console.WriteLine("\n\n\n\t\t------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t\t|\tTrain Number\t| \tTrain Name\t| Source\t| Destination\t| Arrival Time | Departure Time |");
            Console.WriteLine("\t\t------------------------------------------------------------------------------------------------------------\n");
            foreach (var train in activeTrains)
            {
                var train_details = db.TrainPrices.FirstOrDefault(tp => tp.train_id == train.train_id);
                Console.WriteLine($"\t\t|\t{train.train_id}\t\t| {train.train_name}\t| {train.source}\t| {train.destination}\t| {train.arrival_time}\t| {train.departure_time} |");
                Console.WriteLine($"\n\t\t    First Class: {train_details.first_class_price}rs ({train_details.first_class_tickets_available} seats)" +
                                  $"\tSecond Class: {train_details.second_class_price}rs ({train_details.second_class_tickets_available} seats)" +
                                  $"\tSleeper Class: {train_details.sleeper_class_price}rs ({train_details.sleeper_class_tickets_available} seats)");
                Console.WriteLine("\t\t------------------------------------------------------------------------------------------------------------\n");
            }
        }

        // Method to display user profile information
        private static void myProfile(string email, string password)
        {
            var user = db.UserAccounts.FirstOrDefault(u => u.user_email == email && u.user_password == password);

            if (user != null)
            {
                Console.WriteLine("\n\n\n\t\t\t\t-----  Profile  -----\n\n\n");

                // Print user details
                Console.WriteLine($"\t\t\t\tUsername: {user.username}");
                Console.WriteLine($"\n\t\t\t\tEmail: {user.user_email}");
                Console.WriteLine($"\n\t\t\t\tAge: {user.age}");
                Console.WriteLine($"\n\t\t\t\tPhone Number: {user.phone_number}");
                Console.WriteLine($"\n\t\t\t\tAddress: {user.address}");

                Console.WriteLine("\n\n\n");

            }
            else
            {
                Console.WriteLine("\n\t\t\t\t-----  User not found !! -----");
                exitLoop();
            }

        }

        // Method to handle booking details for users

        private static void User_booking_details(int userId)
        {
            Console.Clear();

            string train_class;
            float trainPrice;
            Console.WriteLine("\t\t\t\t ***** Welcome to Booking Section *****\n\n\n");

            Console.Write("\n\t\t\t\tEnter your source: ");
            string source = Console.ReadLine();

            Console.Write("\n\t\t\t\tEnter your Destination: ");
            string destination = Console.ReadLine();

            try
            {
                var activeTrains = db.Trains.Where(t => t.source == source && t.destination == destination && t.is_active == true).ToList();
                float trainPrince;

                if (activeTrains.Count != 0)
                {
                    Console.WriteLine("\n\n\n\t\t---------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("\t\t|\tTrain Number\t| \tTrain Name\t| Source\t| Destination\t| Arrival Time | Departure Time |");
                    Console.WriteLine("\t\t------------------------------------------------------------------------------------------\n");

                    foreach (var train in activeTrains)
                    {
                        var train_details = db.TrainPrices.FirstOrDefault(tp => tp.train_id == train.train_id);
                        Console.WriteLine($"\t\t|\t{train.train_id}\t\t| {train.train_name}\t| {train.source}\t| {train.destination}\t| {train.arrival_time}\t| {train.departure_time} |");
                        Console.WriteLine($"\n\t\t    First Class: {train_details.first_class_price}rs ({train_details.first_class_tickets_available} seats)" +
                                          $"\tSecond Class: {train_details.second_class_price}rs ({train_details.second_class_tickets_available} seats)" +
                                          $"\tSleeper Class: {train_details.sleeper_class_price}rs ({train_details.sleeper_class_tickets_available} seats)");
                        Console.WriteLine("\t\t-----------------------------------------------------------------------------------------------------------------------------\n");
                    }


                    int trainId;
                    while (true)
                    {
                        Console.Write("\n\t\t\t\tEnter train number you want to book:  ");
                        trainId = int.Parse(Console.ReadLine());
                        var runningTrain = db.Trains.FirstOrDefault(t => t.train_id == trainId && t.source == source && t.destination == destination && t.is_active == true);
                        if (runningTrain != null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n\n\t\t\t\tThere is not train available by given your train number\n\n");
                        }
                    }

                    Console.WriteLine("\n\t\t\t\tWhich class you want to prefer: ");


                    while (true)
                    {
                        Console.WriteLine("\n\t\t\t\t" +
                            "1. First Class" +
                            "\n\t\t\t\t" +
                            "2. Second Class" +
                            "\n\t\t\t\t" +
                            "3. Sleeper Class");
                        Console.Write("\n\t\t\t\tchoose an option: ");
                        int opt = int.Parse(Console.ReadLine());
                        if (opt == 1)
                        {
                            train_class = "First Class";
                            trainPrice = (float)db.TrainPrices.Where(t => t.train_id == trainId).Select(p => p.first_class_price).FirstOrDefault();
                            break;
                        }
                        else if (opt == 2)
                        {
                            train_class = "Second Class";
                            trainPrice = (float)db.TrainPrices.Where(t => t.train_id == trainId).Select(p => p.second_class_price).FirstOrDefault();
                            break;
                        }
                        else if (opt == 3)
                        {
                            train_class = "sleeper Class";
                            trainPrice = (float)db.TrainPrices.Where(t => t.train_id == trainId).Select(p => p.sleeper_class_price).FirstOrDefault();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n\t\t\t\tChoose valid option");
                            exitLoop();
                        }
                    }

                    DateTime bookingDate;
                    while (true)
                    {
                        try
                        {
                            Console.Write("\n\t\t\t\tEnter booking date (YYYY-MM-DD): ");
                            bookingDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
                            if (bookingDate >= DateTime.Now)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n\t\t\t\tEnter Correct Date");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("\n\n\t\t\t\tError: "+e.Message);
                        }
                    }




                    try
                    {
                        while (true)
                        {
                            Console.Write("\n\t\t\t\tHow many tickets you want (Maximum 5 tickets per user): ");
                            int totalTickets = int.Parse(Console.ReadLine());
                            if (totalTickets > 5) 
                            {
                                Console.WriteLine("\n\t\t\t\tSorry ! You can buy maximum 5 tickets only.");
                            }
                            else
                            {
                                float total_ticketPrice = trainPrice * totalTickets;

                                if (checkMoney(total_ticketPrice, userId))
                                {
                                    bool flag = InsertBookingDetails(userId, totalTickets, trainId, activeTrains, train_class, bookingDate,trainPrice);

                                    if (flag == true)
                                    {
                                        Console.WriteLine("\n\n\n\t\t\t\t-----  Tickets booked successfully!! -----");
                                        exitLoop();

                                    }
                                    else
                                    {
                                        Console.WriteLine("\n\n\n\t\t\t\t-----  Ticket Booked Failed!!  -----");
                                        exitLoop();
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n\t\t\t\tYou don't have enough money. Please add money in your wallet");
                                    exitLoop();
                                    return;
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n\t\t\t\tInvalid input for ticket count.");
                        exitLoop();
                    }

                }
                else
                {
                    Console.WriteLine("\n\t\t\t\tThere is no train available in this route!!");
                    exitLoop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\t\t\t\tAn error occurred: " + ex.Message);
                exitLoop();
            }


        }

        // Method to check user wallet balance before booking

        private static bool checkMoney(float totat_trainPrice, int userId)
        {
            var userMoney = db.Wallets.Where(w => w.user_id == userId).Select(w => w.balance).FirstOrDefault();
            if (userMoney > totat_trainPrice)
            {
                return true;
            }
            else
                return false;
        }

        // Method to generate a unique seat number for a train

        private static int GenerateUniqueSeatNumber(int trainId)
        {
            // Logic to generate a unique seat number for the given train
            Random random = new Random();
            int seatNumber = random.Next(1, 100);
            return seatNumber;
        }

        // Method to generate a unique PNR number for a booking

        private static int GenerateUniquePNRNumber()
        {
            // Logic to generate a unique PNR number
            Random random = new Random();
            int pnrNumber = random.Next(100000, 999999);
            return pnrNumber;
        }

        // Method to insert booking details into the database

        private static bool InsertBookingDetails(int userId, int totalTickets, int trainId, List<Train> activeTrains, string train_class, DateTime bookingDate, float trainPrice)
        {
            float totalPrice = 0;

            try
            {
                int age = 0;
                for (int i = 0; i < totalTickets; i++)
                {
                    // Generate unique seat number (you may need to adjust this logic)
                    int seatNumber = GenerateUniqueSeatNumber(trainId);

                    // Generate unique PNR number (you may need to adjust this logic)
                    int pnrNumber = GenerateUniquePNRNumber();

                    Console.Write("\n\t\t\t\tEnter Your Name: ");
                    string userName = Console.ReadLine();

                    while (true)
                    {
                        try
                        {
                            Console.Write("\n\t\t\t\tEnter Your Age: ");
                            age = int.Parse(Console.ReadLine());
                            
                            if(age>=1 || age <= 100)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n\t\t\t\tAre you an Alien?");
                                exitLoop();
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("\n\t\t\t\tError: "+e.Message);
                            exitLoop();
                        }
                    }

                    string trainName = activeTrains.FirstOrDefault(t => t.train_id == trainId)?.train_name;

                    TimeSpan? arrivalTime = activeTrains.FirstOrDefault(t => t.train_id == trainId)?.arrival_time;
                    TimeSpan? departureTime = activeTrains.FirstOrDefault(t => t.train_id == trainId)?.departure_time;

                    if (age > 0 && age <= 5)
                    {
                        totalPrice += 0;
                        Console.WriteLine($"\n\n\t\t\t\tName: {userName} and Age: {age}");
                        Console.WriteLine("\n\t\t\t\tLittle Champs - Free Ticket ");

                    }
                    else if (age >= 60)
                    { 
                        float discount_money = trainPrice - ((trainPrice * 30) / 100);
                        Console.WriteLine($"\n\n\t\t\t\tName: {userName} and Age: {age}");
                        Console.WriteLine($"\n\t\t\t\tSenior Citizen, Price with discount: {discount_money}rs");
                        totalPrice += discount_money;

                    }
                    else
                    {
                        totalPrice += trainPrice;
                        Console.WriteLine($"\n\n\t\t\t\tName: {userName} and Age: {age}");
                        Console.WriteLine($"\n\t\t\t\tTicket Booked, Price: {trainPrice}rs");
                    }

                    Booking booking = new Booking
                    {
                        user_id = userId,
                        username = userName,
                        age = age,
                        train_id = trainId,
                        pnr_no = pnrNumber,
                        train_name = trainName,
                        travelling_date = bookingDate,
                        arrival_time = arrivalTime,
                        departure_time = departureTime,
                        trainClass = train_class,
                        total_price = totalPrice,
                        seat_no = seatNumber,
                        status = "booked"
                    };

                    db.Bookings.Add(booking);

                    
                    db.SaveChanges();

                }

                db.deductAmountFromWallet(userId, totalPrice, trainId, train_class, age);
             
                Console.WriteLine($"\n\t\t\t\tTotal Amount : {totalPrice}");


                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tError: " + e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("\n\t\t\t\tInner Exception: " + e.InnerException.Message);
                }
                return false;
            }
        }

        // Method to cancel a booking

        private static void CancelBooking(int user_id, int pnrNo)
        {
            try
            {
                // Find the booking in the database
                var booking = db.Bookings.FirstOrDefault(u => u.user_id == user_id && u.pnr_no == pnrNo);

                string status = booking.status;
                float totalPrice = (float)booking.total_price;


                if (booking != null)
                {
                    // Check if the booking is already cancelled
                    if (status == "cancelled")
                    {
                        Console.WriteLine("\n\t\t\t\tThis booking is already cancelled.");
                    }

                    else
                    {
                        // Update the status of the booking to "cancelled"
                        booking.status = "cancelled";

                        // Optionally, refund the amount to the user's wallet
                        db.refund_amount_to_wallet(user_id, totalPrice,booking.train_id,booking.trainClass);

                        db.SaveChanges();

                        Console.WriteLine("\n\t\t\t\t-----  Booking cancelled successfully !!  -----");
                        exitLoop();
                    }

                }
                else
                {
                    Console.WriteLine("\n\t\t\t\t-----  Booking not found !!  -----");
                    exitLoop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tError: " + e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("\n\t\t\t\tInner Exception: " + e.InnerException.Message);
                    exitLoop();
                }
            }
        }

        // Method to display booking and cancellation history for a user

        private static void ShowBookingCancellationHistory(int user_id)
        {
            try
            {
                // Retrieve booking and cancellation history for the given user_id
                var history = db.Bookings
                                .Where(u => u.user_id == user_id && (u.status == "booked" || u.status == "cancelled"))
                                .ToList();

                if (history.Any())
                {
                    Console.WriteLine($"\n\t\t\t\tBooking and Cancellation History for User ID: {user_id}");
                    Console.WriteLine("\n\t\t\t\t--------------------------------------------------");

                    foreach (var record in history)
                    {
                        Console.WriteLine($"\n\t\t\t\tPNR No: {record.pnr_no}");
                        Console.WriteLine($"\n\t\t\t\tUser Name: {record.username}");
                        Console.WriteLine($"\n\t\t\t\tStatus: {record.status}");
                        Console.WriteLine($"\n\t\t\t\tBooking Date: {record.travelling_date}");
                        Console.WriteLine($"\n\t\t\t\tTrain Name: {record.train_name}");
                        Console.WriteLine($"\n\t\t\t\tTotal Price: {record.total_price}");

                        if (record.status == "cancelled")
                        {
                            // If the booking is cancelled, display the cancellation date
                            Console.WriteLine($"\n\t\t\t\tCancellation Date: {record.travelling_date}");
                        }

                        Console.WriteLine("\n\t\t\t\t--------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("\n\t\t\t\t-----  No booking or cancellation history found for the given User ID !!  -----");
                    exitLoop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tError: " + e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("\n\t\t\t\tInner Exception: " + e.InnerException.Message);
                    exitLoop();
                }
            }
        }



        // Method to clear the console screen and wait before exiting

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
