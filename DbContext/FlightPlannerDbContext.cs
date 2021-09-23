﻿using FlightPlanner.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.Web.DbContext
{
    public class FlightPlannerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airport { get; set; }
    }
}
