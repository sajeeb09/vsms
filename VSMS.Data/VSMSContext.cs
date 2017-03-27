using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VSMS.Data
{
    public class VSMSContext : DbContext
    {

        public VSMSContext() : base("VSMSConnection") 
        { 
    
        }
      
        public DbSet<user> users { get; set; }
        public DbSet<vehicle> vehicles { get; set; }
        public DbSet<customer> customers { get; set; }
        public DbSet<brand> brands { get; set; }
        public DbSet<manufacturar> manufacturars { get; set; }
    }
}
