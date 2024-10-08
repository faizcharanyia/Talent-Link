﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sp23Team09FinalProject.DAL;

#nullable disable

namespace sp23Team09FinalProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);
            SqlServerModelBuilderExtensions.HasServiceTierSql(modelBuilder, "'Basic'");
            SqlServerModelBuilderExtensions.HasPerformanceLevelSql(modelBuilder, "'Basic'");

            modelBuilder.Entity("CompanyIndustry", b =>
                {
                    b.Property<int>("CompanysCompanyID")
                        .HasColumnType("int");

                    b.Property<int>("IndustrysIndustryID")
                        .HasColumnType("int");

                    b.HasKey("CompanysCompanyID", "IndustrysIndustryID");

                    b.HasIndex("IndustrysIndustryID");

                    b.ToTable("CompanyIndustry");
                });

            modelBuilder.Entity("MajorPosition", b =>
                {
                    b.Property<int>("MajorsMajorID")
                        .HasColumnType("int");

                    b.Property<int>("PositionsPositionID")
                        .HasColumnType("int");

                    b.HasKey("MajorsMajorID", "PositionsPositionID");

                    b.HasIndex("PositionsPositionID");

                    b.ToTable("MajorPosition");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("GPA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("GradDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("MajorID")
                        .HasColumnType("int");

                    b.Property<string>("MiddleInitial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("PositionType")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("Zip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("MajorID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Application", b =>
                {
                    b.Property<int>("ApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationID"));

                    b.Property<int?>("CurrDateID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PositionID")
                        .HasColumnType("int");

                    b.Property<int?>("Room")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplicationID");

                    b.HasIndex("CurrDateID");

                    b.HasIndex("PositionID");

                    b.HasIndex("StudentId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("CompanyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrDateID")
                        .HasColumnType("int");

                    b.HasKey("CompanyID");

                    b.HasIndex("CurrDateID");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Global", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CurrDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("CurrDate");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Industry", b =>
                {
                    b.Property<int>("IndustryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IndustryID"));

                    b.Property<string>("IndustryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IndustryID");

                    b.ToTable("Industrys");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Major", b =>
                {
                    b.Property<int>("MajorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorID"));

                    b.Property<string>("MajorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorID");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Position", b =>
                {
                    b.Property<int>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("CurrDateID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PositionID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("CurrDateID");

                    b.HasIndex("InterviewerId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("CompanyIndustry", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanysCompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sp23Team09FinalProject.Models.Industry", null)
                        .WithMany()
                        .HasForeignKey("IndustrysIndustryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MajorPosition", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Major", null)
                        .WithMany()
                        .HasForeignKey("MajorsMajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sp23Team09FinalProject.Models.Position", null)
                        .WithMany()
                        .HasForeignKey("PositionsPositionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sp23Team09FinalProject.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.AppUser", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Company", "Company")
                        .WithMany("AppUsers")
                        .HasForeignKey("CompanyID");

                    b.HasOne("sp23Team09FinalProject.Models.Major", "Major")
                        .WithMany("AppUsers")
                        .HasForeignKey("MajorID");

                    b.Navigation("Company");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Application", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Global", "CurrDate")
                        .WithMany("Applications")
                        .HasForeignKey("CurrDateID");

                    b.HasOne("sp23Team09FinalProject.Models.Position", "Position")
                        .WithMany("Applications")
                        .HasForeignKey("PositionID");

                    b.HasOne("sp23Team09FinalProject.Models.AppUser", "Student")
                        .WithMany("Applications")
                        .HasForeignKey("StudentId");

                    b.Navigation("CurrDate");

                    b.Navigation("Position");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Company", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Global", "CurrDate")
                        .WithMany("Companys")
                        .HasForeignKey("CurrDateID");

                    b.Navigation("CurrDate");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Position", b =>
                {
                    b.HasOne("sp23Team09FinalProject.Models.Company", "Company")
                        .WithMany("Positions")
                        .HasForeignKey("CompanyID");

                    b.HasOne("sp23Team09FinalProject.Models.Global", "CurrDate")
                        .WithMany("Positions")
                        .HasForeignKey("CurrDateID");

                    b.HasOne("sp23Team09FinalProject.Models.AppUser", "Interviewer")
                        .WithMany()
                        .HasForeignKey("InterviewerId");

                    b.Navigation("Company");

                    b.Navigation("CurrDate");

                    b.Navigation("Interviewer");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.AppUser", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Company", b =>
                {
                    b.Navigation("AppUsers");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Global", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Companys");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Major", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("sp23Team09FinalProject.Models.Position", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
