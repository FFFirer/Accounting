﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounting.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    [Migration("20250317154329_Add_Ledger_And_AssetAccount")]
    partial class Add_Ledger_And_AssetAccount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.0-preview.1.25081.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Accounting.Asset.AssetAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<bool>("IncludedInTotalAssets")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LedgerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("AssetAccounts");
                });

            modelBuilder.Entity("Accounting.Books.Ledger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MonthStartDay")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ledgers");
                });

            modelBuilder.Entity("Accounting.Books.LedgerCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LedgerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.HasIndex("Name", "LedgerId")
                        .IsUnique();

                    b.ToTable("LedgerCategories");
                });

            modelBuilder.Entity("Accounting.Books.LedgerRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("AssetAccountId")
                        .HasColumnType("text");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FlowDirection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LedgerId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("RecordTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Remark")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssetAccountId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LedgerId");

                    b.ToTable("LedgerRecords");
                });

            modelBuilder.Entity("Accounting.Books.LedgerRecordAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("LastModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LedgerRecordId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("LedgerRecordId");

                    b.ToTable("LedgerRecordAttachments");
                });

            modelBuilder.Entity("Accounting.Books.LedgerTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LedgerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.HasIndex("Name", "LedgerId")
                        .IsUnique();

                    b.ToTable("LedgerTags");
                });

            modelBuilder.Entity("Accounting.Documents.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Content");

                    b.Property<DateTimeOffset>("LastModified")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LastModified");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Type");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("Accounting.Documents.Document<Accounting.Documents.SiteSettings>", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    b.Property<string>("Content")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Content");

                    b.Property<DateTimeOffset>("LastModified")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LastModified");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Type");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("Accounting.FileStorage.FileInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BucketName")
                        .HasColumnType("character varying(5000)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeleteWhenExpired")
                        .HasColumnType("boolean");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("text");

                    b.Property<long?>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("StoragePath")
                        .HasColumnType("text");

                    b.Property<string>("StorageProvider")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.PrimitiveCollection<List<string>>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTimeOffset>("UploadTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BucketName");

                    b.ToTable("FileInformations");
                });

            modelBuilder.Entity("Accounting.FileStorage.StorageBucket", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<DateTimeOffset>("CreatationTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Name");

                    b.ToTable("StorageBuckets");
                });

            modelBuilder.Entity("Accounting.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Accounting.UserRefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Revoked")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRefreshTokens");
                });

            modelBuilder.Entity("LedgerRecordLedgerTag", b =>
                {
                    b.Property<long>("LedgerRecordId")
                        .HasColumnType("bigint");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint");

                    b.HasKey("LedgerRecordId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("LedgerRecordLedgerTag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Accounting.Asset.AssetAccount", b =>
                {
                    b.HasOne("Accounting.Books.Ledger", null)
                        .WithMany("AssetAccounts")
                        .HasForeignKey("LedgerId");
                });

            modelBuilder.Entity("Accounting.Books.LedgerCategory", b =>
                {
                    b.HasOne("Accounting.Books.Ledger", null)
                        .WithMany("Categories")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.Books.LedgerRecord", b =>
                {
                    b.HasOne("Accounting.Asset.AssetAccount", "AssetAccount")
                        .WithMany("Records")
                        .HasForeignKey("AssetAccountId");

                    b.HasOne("Accounting.Books.LedgerCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Books.Ledger", "Ledger")
                        .WithMany("Records")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetAccount");

                    b.Navigation("Category");

                    b.Navigation("Ledger");
                });

            modelBuilder.Entity("Accounting.Books.LedgerRecordAttachment", b =>
                {
                    b.HasOne("Accounting.FileStorage.FileInformation", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("Accounting.Books.LedgerRecord", null)
                        .WithMany("Attachments")
                        .HasForeignKey("LedgerRecordId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("Accounting.Books.LedgerTag", b =>
                {
                    b.HasOne("Accounting.Books.Ledger", null)
                        .WithMany("Tags")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.Documents.Document<Accounting.Documents.SiteSettings>", b =>
                {
                    b.HasOne("Accounting.Documents.Document", null)
                        .WithOne()
                        .HasForeignKey("Accounting.Documents.Document<Accounting.Documents.SiteSettings>", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.FileStorage.FileInformation", b =>
                {
                    b.HasOne("Accounting.FileStorage.StorageBucket", "Bucket")
                        .WithMany("Files")
                        .HasForeignKey("BucketName");

                    b.Navigation("Bucket");
                });

            modelBuilder.Entity("LedgerRecordLedgerTag", b =>
                {
                    b.HasOne("Accounting.Books.LedgerRecord", null)
                        .WithMany()
                        .HasForeignKey("LedgerRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Books.LedgerTag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
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
                    b.HasOne("Accounting.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Accounting.User", null)
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

                    b.HasOne("Accounting.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Accounting.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.Asset.AssetAccount", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("Accounting.Books.Ledger", b =>
                {
                    b.Navigation("AssetAccounts");

                    b.Navigation("Categories");

                    b.Navigation("Records");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Accounting.Books.LedgerRecord", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Accounting.FileStorage.StorageBucket", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
