﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Train_Reservation_System
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Train_Reservation_System_Entities : DbContext
    {
        public Train_Reservation_System_Entities()
            : base("name=Train_Reservation_System_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminAccount> AdminAccounts { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<TrainPrice> TrainPrices { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
    
        public virtual int deductAmountFromWallet(Nullable<int> userId, Nullable<double> totalPrice, Nullable<int> trainId, string trainClass, Nullable<int> age)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var totalPriceParameter = totalPrice.HasValue ?
                new ObjectParameter("totalPrice", totalPrice) :
                new ObjectParameter("totalPrice", typeof(double));
    
            var trainIdParameter = trainId.HasValue ?
                new ObjectParameter("trainId", trainId) :
                new ObjectParameter("trainId", typeof(int));
    
            var trainClassParameter = trainClass != null ?
                new ObjectParameter("trainClass", trainClass) :
                new ObjectParameter("trainClass", typeof(string));
    
            var ageParameter = age.HasValue ?
                new ObjectParameter("age", age) :
                new ObjectParameter("age", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deductAmountFromWallet", userIdParameter, totalPriceParameter, trainIdParameter, trainClassParameter, ageParameter);
        }
    
        public virtual int refund_amount_to_wallet(Nullable<int> user_id, Nullable<double> amount, Nullable<int> train_id, string class_type)
        {
            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(double));
    
            var train_idParameter = train_id.HasValue ?
                new ObjectParameter("train_id", train_id) :
                new ObjectParameter("train_id", typeof(int));
    
            var class_typeParameter = class_type != null ?
                new ObjectParameter("class_type", class_type) :
                new ObjectParameter("class_type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("refund_amount_to_wallet", user_idParameter, amountParameter, train_idParameter, class_typeParameter);
        }
    }
}
