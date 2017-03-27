namespace VSMS.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VSMS.Data.VSMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "VSMS.Data.VSMSContext";
        }

        protected override void Seed(VSMS.Data.VSMSContext context)
        {

            context.users.AddOrUpdate(t => t.userId,
                new user() { userId = 1, userName = "admin", userPass = "123", userType = "Admin", sQuestion = "how are you?", aQuestion = "Not Good" });

            context.vehicles.AddOrUpdate(t => t.vehicleId,
                new vehicle() { vehicleId = 1, bprice = 5000000, vehiclebrand = "Allion", chasisNo = "99F945JH", color = "Black", engineNo = "883400234023",bdate=new DateTime(2016, 1, 1), vehiclemanufacturar = "Toyota", model = "T2002", mYear = "2002", status = "Available" });


       
            context.manufacturars.AddOrUpdate(x => x.manufacturarId,
                new manufacturar() { manufacturarId = 1,  vehiclemanufacturar = "Toyota "},
                new manufacturar() { manufacturarId = 2, vehiclemanufacturar = "Nissan" },
                new manufacturar() { manufacturarId = 3, vehiclemanufacturar = "Audi" });

          
        }
    }
}
