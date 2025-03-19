using System;
using System.Collections.Generic;
using System.Text;

using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;

using Microsoft.EntityFrameworkCore;

namespace Accounting.Quartz;

public class AccountingQuartzDbContext : DbContext
{
    public AccountingQuartzDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddQuartz(builder => builder.UsePostgreSql());
    }
}
