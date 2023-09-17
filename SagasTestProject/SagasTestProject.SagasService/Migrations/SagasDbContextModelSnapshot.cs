﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SagasTestProject.SagasService;

#nullable disable

namespace SagasTestProject.SagasService.Migrations
{
    [DbContext(typeof(SagasDbContext))]
    partial class SagasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("SagasTestProject.SagasService.States.BuyItemsSagaState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentState")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseAddress")
                        .HasColumnType("TEXT");

                    b.HasKey("CorrelationId");

                    b.ToTable("BuyItemsSagaState");
                });
#pragma warning restore 612, 618
        }
    }
}