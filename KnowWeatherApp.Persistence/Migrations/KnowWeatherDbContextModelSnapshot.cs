﻿// <auto-generated />
using System;
using KnowWeatherApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KnowWeatherApp.API.Migrations
{
    [DbContext(typeof(KnowWeatherDbContext))]
    partial class KnowWeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppUserCity", b =>
                {
                    b.Property<string>("CitiesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CitiesId", "UsersId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CityId");

                    b.HasIndex("UsersId");

                    b.ToTable("AppUserCity");
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
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
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeZoneOffset")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.Trigger", b =>
                {
                    b.Property<int>("TriggerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TriggerId"));

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EqualityType")
                        .HasColumnType("int");

                    b.Property<int>("Field")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NotificationTypes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Threshold")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("TimeOfDayToCheck")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TimeToNotify")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TimeToNotifyUtc")
                        .HasColumnType("time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TriggerId");

                    b.HasIndex("CityId");

                    b.HasIndex("UserId");

                    b.ToTable("Triggers");
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.Weather.WeatherReport", b =>
                {
                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("TimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeZoneOffset")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("CityId");

                    b.ToTable("WeatherReports");
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AppUserCity", b =>
                {
                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KnowWeatherApp.Domain.Entities.City", null)
                        .WithMany()
                        .HasForeignKey("CitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KnowWeatherApp.Domain.Entities.City", null)
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.Trigger", b =>
                {
                    b.HasOne("KnowWeatherApp.Domain.Entities.City", "City")
                        .WithMany("Triggers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", "User")
                        .WithMany("Triggers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.Weather.WeatherReport", b =>
                {
                    b.HasOne("KnowWeatherApp.Domain.Entities.City", "City")
                        .WithOne("WeatherReport")
                        .HasForeignKey("KnowWeatherApp.Domain.Entities.Weather.WeatherReport", "CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("KnowWeatherApp.Domain.Entities.Weather.CurrentWeatherReport", "Current", b1 =>
                        {
                            b1.Property<string>("WeatherReportCityId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Clouds")
                                .HasColumnType("int");

                            b1.Property<DateTime>("DateTime")
                                .HasColumnType("datetime2");

                            b1.Property<double>("DewPoint")
                                .HasColumnType("float");

                            b1.Property<double>("FeelsLike")
                                .HasColumnType("float");

                            b1.Property<int>("Humidity")
                                .HasColumnType("int");

                            b1.Property<int>("Pressure")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Sunrise")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Sunset")
                                .HasColumnType("datetime2");

                            b1.Property<double>("Temp")
                                .HasColumnType("float");

                            b1.Property<double>("Uvi")
                                .HasColumnType("float");

                            b1.Property<int>("Visibility")
                                .HasColumnType("int");

                            b1.Property<int>("WindDeg")
                                .HasColumnType("int");

                            b1.Property<double>("WindGust")
                                .HasColumnType("float");

                            b1.Property<double>("Windspeed")
                                .HasColumnType("float");

                            b1.HasKey("WeatherReportCityId");

                            b1.ToTable("WeatherReports");

                            b1.ToJson("Current");

                            b1.WithOwner()
                                .HasForeignKey("WeatherReportCityId");
                        });

                    b.OwnsMany("KnowWeatherApp.Domain.Entities.Weather.DailyWeatherReport", "DailyReports", b1 =>
                        {
                            b1.Property<string>("WeatherReportCityId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("Clouds")
                                .HasColumnType("int");

                            b1.Property<DateTime>("DateTime")
                                .HasColumnType("datetime2");

                            b1.Property<double>("DewPoint")
                                .HasColumnType("float");

                            b1.Property<int>("Humidity")
                                .HasColumnType("int");

                            b1.Property<DateTime>("MoonRise")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("MoonSet")
                                .HasColumnType("datetime2");

                            b1.Property<double>("Precipitation")
                                .HasColumnType("float");

                            b1.Property<int>("Pressure")
                                .HasColumnType("int");

                            b1.Property<double>("Rain")
                                .HasColumnType("float");

                            b1.Property<DateTime>("Sunrise")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Sunset")
                                .HasColumnType("datetime2");

                            b1.Property<double>("Uvi")
                                .HasColumnType("float");

                            b1.Property<double>("WindGust")
                                .HasColumnType("float");

                            b1.Property<double>("WindSpeed")
                                .HasColumnType("float");

                            b1.HasKey("WeatherReportCityId", "Id");

                            b1.ToTable("WeatherReports");

                            b1.ToJson("DailyReports");

                            b1.WithOwner()
                                .HasForeignKey("WeatherReportCityId");

                            b1.OwnsOne("KnowWeatherApp.Domain.Entities.Weather.DailyTempDto", "FeelsLike", b2 =>
                                {
                                    b2.Property<string>("DailyWeatherReportWeatherReportCityId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("DailyWeatherReportId")
                                        .HasColumnType("int");

                                    b2.Property<double>("Day")
                                        .HasColumnType("float");

                                    b2.Property<double>("Evening")
                                        .HasColumnType("float");

                                    b2.Property<double>("Max")
                                        .HasColumnType("float");

                                    b2.Property<double>("Min")
                                        .HasColumnType("float");

                                    b2.Property<double>("Morning")
                                        .HasColumnType("float");

                                    b2.Property<double>("Night")
                                        .HasColumnType("float");

                                    b2.HasKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId");

                                    b2.ToTable("WeatherReports");

                                    b2.WithOwner()
                                        .HasForeignKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId");
                                });

                            b1.OwnsOne("KnowWeatherApp.Domain.Entities.Weather.DailyTempDto", "Temp", b2 =>
                                {
                                    b2.Property<string>("DailyWeatherReportWeatherReportCityId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("DailyWeatherReportId")
                                        .HasColumnType("int");

                                    b2.Property<double>("Day")
                                        .HasColumnType("float");

                                    b2.Property<double>("Evening")
                                        .HasColumnType("float");

                                    b2.Property<double>("Max")
                                        .HasColumnType("float");

                                    b2.Property<double>("Min")
                                        .HasColumnType("float");

                                    b2.Property<double>("Morning")
                                        .HasColumnType("float");

                                    b2.Property<double>("Night")
                                        .HasColumnType("float");

                                    b2.HasKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId");

                                    b2.ToTable("WeatherReports");

                                    b2.WithOwner()
                                        .HasForeignKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId");
                                });

                            b1.OwnsMany("KnowWeatherApp.Contracts.OpenWeather.GeneralWeather", "Weather", b2 =>
                                {
                                    b2.Property<string>("DailyWeatherReportWeatherReportCityId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("DailyWeatherReportId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Icon")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Main")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId", "Id");

                                    b2.ToTable("WeatherReports");

                                    b2.WithOwner()
                                        .HasForeignKey("DailyWeatherReportWeatherReportCityId", "DailyWeatherReportId");
                                });

                            b1.Navigation("FeelsLike")
                                .IsRequired();

                            b1.Navigation("Temp")
                                .IsRequired();

                            b1.Navigation("Weather");
                        });

                    b.OwnsMany("KnowWeatherApp.Domain.Entities.Weather.HourlyWeatherReport", "HourlyReports", b1 =>
                        {
                            b1.Property<string>("WeatherReportCityId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("Clouds")
                                .HasColumnType("int");

                            b1.Property<DateTime>("DateTime")
                                .HasColumnType("datetime2");

                            b1.Property<double>("DewPoint")
                                .HasColumnType("float");

                            b1.Property<double>("FeelsLike")
                                .HasColumnType("float");

                            b1.Property<int>("Humidity")
                                .HasColumnType("int");

                            b1.Property<double>("Precipitation")
                                .HasColumnType("float");

                            b1.Property<int>("Pressure")
                                .HasColumnType("int");

                            b1.Property<double>("Temp")
                                .HasColumnType("float");

                            b1.Property<double>("Uvi")
                                .HasColumnType("float");

                            b1.Property<int>("Visibility")
                                .HasColumnType("int");

                            b1.Property<int>("WindDeg")
                                .HasColumnType("int");

                            b1.Property<double>("WindGust")
                                .HasColumnType("float");

                            b1.Property<double>("Windspeed")
                                .HasColumnType("float");

                            b1.HasKey("WeatherReportCityId", "Id");

                            b1.ToTable("WeatherReports");

                            b1.ToJson("HourlyReports");

                            b1.WithOwner()
                                .HasForeignKey("WeatherReportCityId");

                            b1.OwnsMany("KnowWeatherApp.Contracts.OpenWeather.GeneralWeather", "Weather", b2 =>
                                {
                                    b2.Property<string>("HourlyWeatherReportWeatherReportCityId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("HourlyWeatherReportId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Icon")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Main")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("HourlyWeatherReportWeatherReportCityId", "HourlyWeatherReportId", "Id");

                                    b2.ToTable("WeatherReports");

                                    b2.WithOwner()
                                        .HasForeignKey("HourlyWeatherReportWeatherReportCityId", "HourlyWeatherReportId");
                                });

                            b1.Navigation("Weather");
                        });

                    b.Navigation("City");

                    b.Navigation("Current")
                        .IsRequired();

                    b.Navigation("DailyReports");

                    b.Navigation("HourlyReports");
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
                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
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

                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KnowWeatherApp.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.AppUser", b =>
                {
                    b.Navigation("Triggers");
                });

            modelBuilder.Entity("KnowWeatherApp.Domain.Entities.City", b =>
                {
                    b.Navigation("Triggers");

                    b.Navigation("WeatherReport");
                });
#pragma warning restore 612, 618
        }
    }
}
