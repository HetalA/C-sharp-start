using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apiproj.Models;

    public class Ace5Context : DbContext
    {
        public Ace5Context (DbContextOptions<Ace5Context> options)
            : base(options)
        {
        }

        public DbSet<apiproj.Models.HetalBooking> HetalBooking { get; set; } = default!;
    }
