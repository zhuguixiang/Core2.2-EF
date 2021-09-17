﻿// <auto-generated />
using System;
using Core.EF.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace Core.EF.Application.Models.Migrations
{
    [DbContext(typeof(EFCoreContex))]
    partial class EFCoreContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("Core.EF.Application.Models.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CREATETIME");

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL")
                        .HasMaxLength(100);

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("ISENABLED");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnName("PASSWORD")
                        .HasMaxLength(100);

                    b.Property<bool>("Removed")
                        .HasColumnName("REMOVED");

                    b.Property<string>("UserCode")
                        .IsRequired()
                        .HasColumnName("USERCDE")
                        .HasMaxLength(20);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("USERNAME")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("USERINFOS");
                });
#pragma warning restore 612, 618
        }
    }
}