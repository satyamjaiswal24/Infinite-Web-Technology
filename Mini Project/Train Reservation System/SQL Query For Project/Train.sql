	create database Train_Reservation_System	

	use Train_Reservation_System


	-- Create the Users table
	create table UserAccount (
	 user_id int primary key identity,
	 username varchar(50) unique,
	 user_email varchar(50),
	 user_password varchar(50),
	 age int,
	 phone_number varchar(20),
	 address varchar(100),
	)

	create table AdminAccount(
	  admin_id int primary key identity,
	  admin_name varchar(40),
	  admin_email_id varchar(40),
	  admin_password varchar(40)
	)


	drop table Account
	drop table bookings
	drop table Wallet
	drop table Trains
	drop table TrainPrice

	create table Trains (
	  train_id int primary key identity(10001,1),
	  train_name varchar(50),
	  source varchar(50),
	  destination varchar(50),
	  arrival_time TIME,
	  departure_time TIME,
	  is_active bit default 1
	)


	CREATE TABLE TrainPrice (
	  id INT PRIMARY KEY IDENTITY,
	  train_id INT FOREIGN KEY REFERENCES Trains(train_id),
	  train_name VARCHAR(50),
	  first_class_price float,
	  second_class_price float,
	  sleeper_class_price float,
	  first_class_tickets_available int,
	  second_class_tickets_available int,
	  sleeper_class_tickets_available int,
	  total_available_tickets AS (first_class_tickets_available + second_class_tickets_available + sleeper_class_tickets_available)
	)

	select * from TrainPrice
	insert into AdminAccount values
	('alpha','alpha@gmail.com','alpha123')




	create table Bookings (
    id int primary key identity,
    user_id int foreign key references useraccount(user_id),
    username varchar(50),
	age int,
    train_id int foreign key references trains(train_id),
	pnr_no int unique,
    train_name varchar(50),
    booking_date date,
    arrival_time time,
    departure_time time,
    trainClass varchar(20) check (trainClass in ('First class', 'Second class', 'Sleeper class')),
    total_price float,
    seat_no int unique,
    status varchar(10) check (status in ('booked', 'cancelled'))
)

alter table bookings drop column number_of_tickets
select * from Bookings

	 

	create table Wallet (
	  wallet_id int primary key identity,
	  user_id int foreign key references UserAccount(user_id),
	  user_name varchar(20),
	  balance float default 0
	)
	select * from Wallet

	insert into Trains values
	('Vande Bharat Express', 'Varanasi', 'New Delhi', '08:30','08:35', 100, 1),
	('Kashi Vishwanath', 'Bhadohi', 'Lucknow', '01:00', '01:04',200, 1),
	('Intercity Express', 'Gorakhpur', 'Lucknow', '14:25', '14:30',140, 0),
	('Sambhavana Express', 'Varanasi', 'Ayodhya', '22:15', '22:20',130, 1)

	select * from TrainPrice
	delete Trains

	INSERT INTO TrainPrice (train_id, train_name, first_class_price, second_class_price, sleeper_class_price, first_class_tickets_available, second_class_tickets_available, sleeper_class_tickets_available)
	VALUES
	(10001, 'Vande Bharat Express', 850, 730, 520, 50, 50, 60),
	(10002, 'Kashi Vishwanath', 1200, 725, 415, 50, 50, 100),
	(10003, 'Intercity Express', 935, 620, 310, 70, 70, 80),
	(10004, 'Sambhavana Express', 845, 535, 425, 60, 40, 30)



	select * from Bookings
	truncate table Bookings

	select * from Wallet
	select * from TrainPrice


	create or alter procedure deductAmountFromWallet
    @userId int,
    @totalPrice float,
    @trainId int,
    @trainClass varchar(20)
	as
	begin
		declare @currentBalance float
		declare @seatsAvailable int

		-- Get the current balance from the wallet
		select @currentBalance = balance
		from Wallet
		where user_id = @userId

		-- Get the available seats for the chosen class
		select @seatsAvailable = case @trainClass
								  when 'First class' then first_class_tickets_available
								  when 'Second class' then second_class_tickets_available
								  when 'Sleeper class' then sleeper_class_tickets_available
								  end
		from TrainPrice
		where train_id = @trainId

		-- Check if the user has sufficient balance and available seats
		if @currentBalance >= @totalPrice and @seatsAvailable > 0
		begin
			-- Deduct the amount from the balance
			update Wallet
			set balance = balance - @totalPrice
			where user_id = @userId

			-- Reduce the available seats for the chosen class
			update TrainPrice
			set
				first_class_tickets_available = case @trainClass when 'First class' then first_class_tickets_available - 1 else first_class_tickets_available end,
				second_class_tickets_available = case @trainClass when 'Second class' then second_class_tickets_available - 1 else second_class_tickets_available end,
				sleeper_class_tickets_available = case @trainClass when 'Sleeper class' then sleeper_class_tickets_available - 1 else sleeper_class_tickets_available end
			where train_id = @trainId;

			print 'Amount deducted successfully.'
		end
		else
		begin
			print 'Insufficient balance or no seats available. Transaction failed.'
		end
	end

	drop proc deductAmountFromWallet


	create or alter procedure deductAmountFromWallet
    @userId int,
    @totalPrice float,
    @trainId int,
    @trainClass varchar(20),
    @age int
	as
	begin
		declare @currentBalance float
		declare @seatsAvailable int
		declare @ticketPrice float

		-- Get the current balance from the wallet
		select @currentBalance = balance
		from Wallet
		where user_id = @userId

		-- Get the available seats for the chosen class
		select @seatsAvailable = case @trainClass
									  when 'First class' then first_class_tickets_available
									  when 'Second class' then second_class_tickets_available
									  when 'Sleeper class' then sleeper_class_tickets_available
									  end
		from TrainPrice
		where train_id = @trainId

		-- Calculate ticket price with concession based on age
		if @age <= 5
		begin
			set @ticketPrice = 0; -- Free ticket for children aged 5 or younger
		end
		else if @age >= 60
		begin
			set @ticketPrice = @totalPrice * 0.7; -- 30% discount for seniors aged 60 or older
		end
		else
		begin
			set @ticketPrice = @totalPrice; -- Original ticket price for others
		end

		-- Check if the user has sufficient balance and available seats
		if @currentBalance >= @ticketPrice and @seatsAvailable > 0
		begin
			-- Deduct the amount from the balance
			update Wallet
			set balance = balance - @ticketPrice
			where user_id = @userId

			-- Reduce the available seats for the chosen class
			update TrainPrice
			set
				first_class_tickets_available = case @trainClass when 'First class' then first_class_tickets_available - 1 else first_class_tickets_available end,
				second_class_tickets_available = case @trainClass when 'Second class' then second_class_tickets_available - 1 else second_class_tickets_available end,
				sleeper_class_tickets_available = case @trainClass when 'Sleeper class' then sleeper_class_tickets_available - 1 else sleeper_class_tickets_available end
			where train_id = @trainId;

			print 'Amount deducted successfully.'
		end
		else
		begin
			print 'Insufficient balance or no seats available. Transaction failed.'
		end
	end



	exec deductAmountFromWallet 1,850,10001,'First Class'

	create or alter procedure refund_amount_to_wallet
    @user_id int,
    @amount float,
	@train_id int,
	@class_type varchar(20)
	as
	begin

		update Wallet
		set balance = balance + @amount
		where user_id = @user_id;
		print 'refund successful.'

		 -- Update seat availability in TrainPrice table
		update TrainPrice
		set 
			first_class_tickets_available = 
				CASE WHEN @class_type = 'First Class' THEN first_class_tickets_available + 1 ELSE first_class_tickets_available END,
			second_class_tickets_available = 
				CASE WHEN @class_type = 'Second Class' THEN second_class_tickets_available + 1 ELSE second_class_tickets_available END,
			sleeper_class_tickets_available = 
				CASE WHEN @class_type = 'Sleeper Class' THEN sleeper_class_tickets_available + 1 ELSE sleeper_class_tickets_available END
		where train_id = @train_id
	end

	drop proc refund_amount_to_wallet

	exec refund_amount_to_wallet 1,850 ,10001,'First Class'
	select * from TrainPrice
	select * from Wallet
	sp_helptext deductAmountFromWallet