using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MGC.Models;

namespace MGC.Migrations
{
    [DbContext(typeof(MyGiftClosetContext))]
    [Migration("20170112192356_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MGC.Models.Gift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HolidayId");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Purchased");

                    b.Property<int?>("RecipientId");

                    b.Property<string>("StoreLink");

                    b.Property<bool>("Wrapped");

                    b.HasKey("Id");

                    b.HasIndex("HolidayId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("MGC.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("MGC.Models.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("MGC.Models.Gift", b =>
                {
                    b.HasOne("MGC.Models.Holiday", "Holiday")
                        .WithMany("Gifts")
                        .HasForeignKey("HolidayId");

                    b.HasOne("MGC.Models.Recipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");
                });
        }
    }
}
