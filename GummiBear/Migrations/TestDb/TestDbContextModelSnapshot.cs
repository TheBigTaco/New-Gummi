using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GummiBear.Models;

namespace GummiBear.Migrations.TestDb
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("GummiBear.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cost");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture");

                    b.HasKey("ProductId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("GummiBear.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("ContentBody");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Rating");

                    b.Property<string>("Title");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("GummiBear.Models.Review", b =>
                {
                    b.HasOne("GummiBear.Models.Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");
                });
        }
    }
}
