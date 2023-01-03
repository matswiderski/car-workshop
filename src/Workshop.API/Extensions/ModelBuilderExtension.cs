using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Workshop.API.Models;

namespace Workshop.API.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Services
            Service service1 = new()
            {
                Id = "6182c48f-4e41-4609-808e-f6df95e5d85f",
                Name = "Oil change",
                Category = "Maintenance",
                Price = 201.5
            };
            Service service2 = new()
            {
                Id = "9bcc9fd5-0a5a-439d-b76a-4acc39714d60",
                Name = "Tire rotation",
                Category = "Maintenance",
                Price = 50.0
            };
            Service service3 = new()
            {
                Id = "1e256131-463d-4412-8759-37d1c640420b",
                Name = "Brake maintenance",
                Category = "Maintenance",
                Price = 100.0
            };
            Service service4 = new()
            {
                Id = "1d16eea6-efc0-4cf9-bdba-b69326495071",
                Name = "Engine repair",
                Category = "Repair",
                Price = 2000.0
            };
            Service service5 = new()
            {
                Id = "33625433-e450-4100-946c-1a8ccd8305ca",
                Name = "Transmission repair",
                Category = "Repair",
                Price = 1000.0
            };
            Service service6 = new()
            {
                Id = "ba654a63-7f40-4890-82f6-bed5037392fc",
                Name = "Suspension repair",
                Category = "Repair",
                Price = 500.0
            };
            Service service7 = new()
            {
                Id = "7f785c6b-99d2-41d3-aeea-1d54dbc7c964",
                Name = "Repair lectrical issues",
                Category = "Electrical system",
                Price = 200.0
            };
            Service service8 = new()
            {
                Id = "8f30b948-9c82-4448-90f0-e0b93dae57e8",
                Name = "Compressor repair",
                Category = "Air conditioning",
                Price = 100.0
            };
            Service service9 = new()
            {
                Id = "2e264c35-29ce-40f2-9377-cb7a6ac9fb88",
                Name = "Evaporator repair",
                Category = "Air conditioning",
                Price = 100.0
            };
            Service service10 = new()
            {
                Id = "32b98306-a9b0-4672-8203-bd8ad14fcdf7",
                Name = "Exterior wash",
                Category = "Detailing",
                Price = 200.0
            };
            Service service11 = new()
            {
                Id = "53c816fa-792f-4e2f-b35d-33a2e43f5df0",
                Name = "Safety inspection",
                Category = "Safety inspections",
                Price = 300.0
            };
            Service service12 = new()
            {
                Id = "bb8281ea-0583-4705-b8c7-9da2692d9542",
                Name = "Other",
                Category = "Other",
                Price = 50.0
            };
            modelBuilder.Entity<Service>().HasData(
                service1, service2, service3,
                service4, service5, service6,
                service7, service8, service9,
                service10, service11, service12);
        }
    }
}
