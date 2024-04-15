using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Train_Reservation_System.Admin
{
    public class AdminClass
    {
        static Train_Reservation_System_Entities db = new Train_Reservation_System_Entities();

        // Method to handle existing admin login
        public static void existing_admin()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t-----  Admin LogIn Section  -----");
            //Console.WriteLine("\t\t\t\t**** Admin Login ****\n\n");

            bool flag = true;
            Console.WriteLine("\n\n\n\n\n\n");
            Console.Write("\t\t\t\tEnter your Email ID: ");
            string email = Console.ReadLine();
            Console.Write("\n\t\t\t\tEnter your Password: ");
            string password = Console.ReadLine();

            var admin = db.AdminAccounts.FirstOrDefault(a => a.admin_email_id == email && a.admin_password == password);

            if (admin != null)
            {
                Console.WriteLine("\n\n\n\t\t\t\t\t-----   Admin Login Successful  ------\n\n\n\n\n\n");
                exitLoop();
                // Admin logged in successfully, proceed with admin functionalities

                while (flag)
                {
                    Console.WriteLine(" \t\t\t\t\t-----   Admin Section  ------\n\n\n\n\n\n");

                    Console.WriteLine("\t\t\t\t" +
                        "1.Add Trains" +
                        "\n\n\t\t\t\t" +
                        "2.Modify Train" +
                        "\n\n\t\t\t\t" +
                        "3.Delete Train" +
                        "\n\n\t\t\t\t" +
                        "4.Log Out");
                    try
                    {
                        Console.Write("\n\n\t\t\t\t" +
                            "Choose an option: ");
                        int opt = int.Parse(Console.ReadLine());

                        switch (opt)
                        {
                            case 1:
                                addTrains();
                                break;

                            case 2:
                                ModifyTrainsByAdmin();
                                break;

                            case 3:
                                SoftTrainDelete();
                                break;

                            case 4:
                                flag = false;
                                Console.WriteLine("\n\n\n\t\t\t\t-----  Log Out Successfully  -----n\n\n\n");
                                exitLoop();
                                break;

                            default:
                                Console.WriteLine("\n\n\t\t\t\tEnter a valid option\n\n");

                                exitLoop();


                                break;
                        }


                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n" + e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\t\t\t\t-----  Invalid Username or Password !!  ------");

                exitLoop();

            }
        }

        // Method to add a new train
        private static void addTrains()
        {
            // Collect train information from the admin
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t-----  Add a New Train  -----\n\n\n\n");

            try
            {
                Console.Write("\n\t\t\t\tEnter Train Name: ");
                string trainName = Console.ReadLine();

                Console.Write("\n\t\t\t\tEnter Source: ");
                string source = Console.ReadLine();

                Console.Write("\n\t\t\t\tEnter Destination: ");
                string destination = Console.ReadLine();

                Console.Write("\n\t\t\t\tEnter Arrival Time (HH:MM):");
                string arrivalTimeString = Console.ReadLine();
                TimeSpan arrivalTime = TimeSpan.ParseExact(arrivalTimeString, "hh\\:mm", null);

                Console.Write("\n\t\t\t\tEnter Departure Time (HH:MM):");
                string departureTimeString = Console.ReadLine();
                TimeSpan departureTime = TimeSpan.ParseExact(departureTimeString, "hh\\:mm", null);


                Console.Write("\n\t\t\t\tEnter First Class Price: ");
                double firstClassPrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("\n\t\t\t\tEnter Second Class Price: ");
                double secondClassPrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("\n\t\t\t\tEnter Sleeper Class Price: ");
                double sleeperClassPrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("\n\t\t\t\tEnter First Class Tickets Available: ");
                int firstClassTicketsAvailable = Convert.ToInt32(Console.ReadLine());

                Console.Write("\n\t\t\t\tEnter Second Class Tickets Available: ");
                int secondClassTicketsAvailable = Convert.ToInt32(Console.ReadLine());

                Console.Write("\n\t\t\t\tEnter Sleeper Class Tickets Available: ");
                int sleeperClassTicketsAvailable = Convert.ToInt32(Console.ReadLine());




                Console.Write("\n\t\t\t\tCreate status of Train that Train is Active or Not: \n\n\t\t\t\t1.For Active\n\t\t\t\t2.For Inactive");
                int opt;
                bool status = false;
                do
                {
                    Console.Write("\n\n\t\t\t\tChoose Option: ");
                    opt = Convert.ToInt32(Console.ReadLine());

                    if (opt == 1 || opt == 2)
                    {
                        if (opt == 1)
                        {
                            status = true;
                        }
                        else if (opt == 2)
                        {
                            status = false;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t\t\tChoose valid option");
                        for (int i = 2; i >= 0; i--)
                        {
                            Thread.Sleep(1000);
                        }
                        Console.Clear();

                    }
                } while (true);

                var newTrain = new Train
                {
                    train_name = trainName,
                    source = source,
                    destination = destination,
                    arrival_time = arrivalTime, // 12:00:
                    departure_time = departureTime, // 08:00:
                    is_active = status
                };

                // Add the newTrain object to the Trains DbSet
                db.Trains.Add(newTrain);
                db.SaveChanges();

                //********************************Adding Price also in Table in Train Price*****************************

                int trainId = newTrain.train_id;
                var newTrainPrice = new TrainPrice
                {
                    train_id = trainId,
                    train_name = trainName,
                    first_class_price = firstClassPrice,
                    second_class_price = secondClassPrice,
                    sleeper_class_price = sleeperClassPrice,
                    first_class_tickets_available = firstClassTicketsAvailable,
                    second_class_tickets_available = secondClassTicketsAvailable,
                    sleeper_class_tickets_available = sleeperClassTicketsAvailable
                };

                // Add the newTrainPrice object to the TrainPrices DbSet
                db.TrainPrices.Add(newTrainPrice);
                // Save changes to the database
                db.SaveChanges();

                Console.WriteLine("\n\n\t\t\t\t-----  Train added successfully!!  -----");
                exitLoop();

            }

            catch (Exception e)
            {
                Console.WriteLine("\n\n\t\t\t\tError: " + e.Message);
                exitLoop();

            }


        }

        private static void ModifyTrainsByAdmin()
        {
            bool flag = true;
            Console.Clear();
            while (flag)
            {
                Console.WriteLine("\n\t\t\t\t-----  What do You want to Update  ***********************************\n\n\n");
                showAllTrains();
                Console.WriteLine("\n\n\t\t\t\t" +
                    "1. Arrival and Departure time" +
                    "\n\n\t\t\t\t" +
                    "2. Source and Destination" +
                    "\n\n\t\t\t\t" +
                    "3. Update Tickets Availability" +
                    "\n\n\t\t\t\t" +
                    "4. Train Status" +
                    "\n\n\t\t\t\t" +
                    "5. Exit");
                try
                {
                    Console.Write("\n\n\t\t\t\tChoose an option: ");
                    int opt = int.Parse(Console.ReadLine());

                    switch (opt)
                    {
                        case 1:
                            UpdateArrivalAndDepartureTime();
                            break;

                        case 2:
                            UpdateSourceAndDestination();
                            break;

                        case 3:
                            UpdateTicketsAvailability();
                            break;

                        case 4:
                            UpdateTrainStatus();
                            break;

                        case 5:
                            flag = false;
                            Console.WriteLine("\n\n\n");
                            exitLoop();
                            break;

                        default:
                            Console.WriteLine("\n\n\t\t\t\tChoose valid Option\n\n");
                            exitLoop();
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\n\t\t\t\t\tError: " + e.Message);
                    exitLoop();
                }
            }
        }
        private static void UpdateArrivalAndDepartureTime()
        {

            Console.Write("\n\n\t\t\t\tEnter train ID:");
            int trainId = int.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t\tEnter updated arrival time:");
            TimeSpan updatedArrivalTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t\t updated departure time:");
            TimeSpan updatedDepartureTime = TimeSpan.Parse(Console.ReadLine());



            var train = db.Trains.FirstOrDefault(t => t.train_id == trainId);
            if (train != null)
            {
                train.arrival_time = updatedArrivalTime;
                train.departure_time = updatedDepartureTime;
                db.SaveChanges();
            }
            exitLoop();

        }

        // Function to update Source and Destination
        private static void UpdateSourceAndDestination()
        {
            Console.Write("\n\n\t\t\t\t train ID:");
            int trainId = int.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t\tEnter updated source:");
            string updatedSource = Console.ReadLine();

            Console.Write("\n\t\t\t\tEnter updated destination:");
            string updatedDestination = Console.ReadLine();


            var train = db.Trains.FirstOrDefault(t => t.train_id == trainId);
            if (train != null)
            {
                train.source = updatedSource;
                train.destination = updatedDestination;
                db.SaveChanges();
            }
            exitLoop();
        }

        // Function to update Tickets Availability
        private static void UpdateTicketsAvailability()
        {
            Console.Write("\n\t\t\t\tEnter train ID:");
            int trainId = int.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t\tEnter updated available tickets for First Class:");
            int first_class_ticket = int.Parse(Console.ReadLine());
            Console.Write("\n\t\t\t\tEnter updated available tickets for Second Class:");
            int secon_class_ticket = int.Parse(Console.ReadLine());
            Console.Write("\n\t\t\t\tEnter updated available tickets for Sleeper Class:");
            int sleeper_class_ticket = int.Parse(Console.ReadLine());


            var train = db.TrainPrices.FirstOrDefault(t => t.train_id == trainId);
            if (train != null)
            {
                train.first_class_tickets_available = first_class_ticket;
                train.second_class_tickets_available = secon_class_ticket;
                train.sleeper_class_tickets_available = sleeper_class_ticket;
                db.SaveChanges();
            }

            exitLoop();
        }

        // Method to update status of a train
        private static void UpdateTrainStatus()
        {
            Console.Write("\n\t\t\t\tEnter train ID:");
            int trainId = int.Parse(Console.ReadLine());

            Console.Write("\n\t\t\t\tEnter updated status (1 for active, 0 for inactive):");
            bool updatedIsActive = Convert.ToBoolean(int.Parse(Console.ReadLine()));


            var train = db.Trains.FirstOrDefault(t => t.train_id == trainId);
            if (train != null)
            {
                train.is_active = updatedIsActive;
                db.SaveChanges();
                Console.WriteLine("\n\n\t\t\t\t-----  Train status updated successfully !!  ------");
            }
            else
            {
                Console.WriteLine("\n\n\t\t\t\t-----  Train not found !!  -----");
                exitLoop();

            }

        }

        // Method to mark a train as inactive (soft delete)
        private static void SoftTrainDelete()
        {
            bool isTrainFound = false;
            int trainID;

            while (!isTrainFound)
            {
                showAllTrains();
                Console.Write("\n\n\t\t\t\tEnter Train Id which you want to Delete: ");
                trainID = int.Parse(Console.ReadLine());
                // Retrieve the train by its ID
                var train = db.Trains.FirstOrDefault(t => t.train_id == trainID);

                if (train != null)
                {
                    // Mark the train as inactive
                    train.is_active = false;

                    // Save changes to the database
                    db.SaveChanges();

                    Console.WriteLine($"\n\n\t\t\t\tTrain with ID {trainID} has been marked as inactive.");
                    isTrainFound = true; // Exit the loop

                    exitLoop();

                }
                else
                {
                    Console.WriteLine($"\n\t\t\t\tTrain with ID {trainID} does not exist.");

                    exitLoop();
                }
            }

        }

        // Method to display all trains
        private static void showAllTrains()
        {

            // Display all trains to the admin

            var activeTrains = db.Trains.ToList();
            Console.WriteLine("\n\n\n\t\t------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t\t|\tTrain Number\t| \tTrain Name\t| Source\t| Destination\t| Arrival Time | Departure Time | Is Active |");
            Console.WriteLine("\t\t------------------------------------------------------------------------------------------------------------\n");

            foreach (var train in activeTrains)
            {
                var trainPrices = db.TrainPrices.FirstOrDefault(tp => tp.train_id == train.train_id);
                Console.WriteLine($"\t\t|\t{train.train_id}\t\t| {train.train_name}\t| {train.source}\t| {train.destination}\t| {train.arrival_time}\t| {train.departure_time}      | {train.is_active} |");
                Console.WriteLine($"\n\t\t    First Class: {trainPrices.first_class_price}rs ({trainPrices.first_class_tickets_available} seats)" +
                                    $"\tSecond Class: {trainPrices.second_class_price}rs ({trainPrices.second_class_tickets_available} seats)" +
                                    $"\tSleeper Class: {trainPrices.sleeper_class_price}rs ({trainPrices.sleeper_class_tickets_available} seats)");
                Console.WriteLine("\t\t------------------------------------------------------------------------------------------------------------");
            }


        }

        // Method to clear console after some delay
        private static void exitLoop()
        {

            for (int i = 2; i >= 0; i--)
            {
                Thread.Sleep(1200);
            }
            Console.Clear();
        }
    }
}
