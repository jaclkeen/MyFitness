using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyFitness.Data;

namespace MyFitness.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170116211615_UpdatedMigrations")]
    partial class UpdatedMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyFitness.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Age");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<double>("CurrentWeight");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<double>("GoalWeight");

                    b.Property<int>("HeightFeet");

                    b.Property<int>("HeightInches");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<double>("MonthlyWeightLost");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfileImg")
                        .IsRequired();

                    b.Property<string>("SecurityStamp");

                    b.Property<double>("TotalWeightLost");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<double>("WeeklyWeightLost");

                    b.Property<double>("WeightLostToday");

                    b.Property<double>("YearlyWeightLost");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MyFitness.Models.DailyNutrition", b =>
                {
                    b.Property<int>("DailyNutritionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DailyNutritionDate");

                    b.Property<double>("TotalCaloriesRemaining");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("DailyNutritionId");

                    b.HasIndex("UserId");

                    b.ToTable("DailyNutrition");
                });

            modelBuilder.Entity("MyFitness.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaloriesBurned");

                    b.Property<int>("DailyNutritionId");

                    b.Property<double>("DistanceTraveled");

                    b.Property<double>("ExerciseLengthInHours");

                    b.Property<string>("ExerciseType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Reps");

                    b.Property<int>("Sets");

                    b.Property<string>("UserId");

                    b.Property<int>("WeightLifted");

                    b.HasKey("ExerciseId");

                    b.HasIndex("DailyNutritionId");

                    b.HasIndex("UserId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("MyFitness.Models.Foods", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Calories");

                    b.Property<int>("DailyNutritionId");

                    b.Property<DateTime>("DateEaten");

                    b.Property<double>("FoodCarbs");

                    b.Property<double>("FoodFat");

                    b.Property<string>("FoodName")
                        .IsRequired();

                    b.Property<double>("FoodProtein");

                    b.Property<int>("Servings");

                    b.Property<string>("UserId");

                    b.HasKey("FoodId");

                    b.HasIndex("DailyNutritionId");

                    b.HasIndex("UserId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("MyFitness.Models.Relationship", b =>
                {
                    b.Property<int>("RelationshipId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RecievingUserId")
                        .IsRequired();

                    b.Property<string>("RecievingUserId1");

                    b.Property<int>("RelationshipStatus");

                    b.Property<int?>("SendingUserId")
                        .IsRequired();

                    b.Property<string>("SendingUserId1");

                    b.HasKey("RelationshipId");

                    b.HasIndex("RecievingUserId1");

                    b.HasIndex("SendingUserId1");

                    b.ToTable("Relationship");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyFitness.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyFitness.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyFitness.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyFitness.Models.DailyNutrition", b =>
                {
                    b.HasOne("MyFitness.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyFitness.Models.Exercise", b =>
                {
                    b.HasOne("MyFitness.Models.DailyNutrition")
                        .WithMany("DailyExercises")
                        .HasForeignKey("DailyNutritionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyFitness.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyFitness.Models.Foods", b =>
                {
                    b.HasOne("MyFitness.Models.DailyNutrition", "DailyNutrition")
                        .WithMany("DailyFoods")
                        .HasForeignKey("DailyNutritionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyFitness.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyFitness.Models.Relationship", b =>
                {
                    b.HasOne("MyFitness.Models.ApplicationUser", "RecievingUser")
                        .WithMany()
                        .HasForeignKey("RecievingUserId1");

                    b.HasOne("MyFitness.Models.ApplicationUser", "SendingUser")
                        .WithMany()
                        .HasForeignKey("SendingUserId1");
                });
        }
    }
}
