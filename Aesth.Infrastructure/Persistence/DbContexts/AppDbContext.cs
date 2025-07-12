using System;
using System.Collections.Generic;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbContexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<address> addresses { get; set; }

    public virtual DbSet<app_user> app_users { get; set; }

    public virtual DbSet<product> products { get; set; }

    public virtual DbSet<product_detail> product_details { get; set; }

    public virtual DbSet<product_image> product_images { get; set; }

    public virtual DbSet<product_size> product_sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<address>(entity =>
        {
            entity.HasKey(e => e.id).HasName("address_pkey");

            entity.ToTable("address");

            entity.Property(e => e.city).HasMaxLength(255);
            entity.Property(e => e.country).HasMaxLength(255);
            entity.Property(e => e.last_name).HasMaxLength(255);
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.neighborhood).HasMaxLength(255);
            entity.Property(e => e.postal_code).HasMaxLength(255);
            entity.Property(e => e.state).HasMaxLength(255);
            entity.Property(e => e.street).HasMaxLength(255);

            entity.HasOne(d => d.user).WithMany(p => p.addresses)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fke4etc0n243br1d15pcmbt930w");
        });

        modelBuilder.Entity<app_user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("app_user_pkey");

            entity.ToTable("app_user");

            entity.Property(e => e.email).HasMaxLength(255);
            entity.Property(e => e.last_name).HasMaxLength(255);
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.role).HasMaxLength(255);
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.color).HasMaxLength(255);
            entity.Property(e => e.description).HasMaxLength(2000);
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.release).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.type).HasMaxLength(255);
        });

        modelBuilder.Entity<product_detail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.details).HasMaxLength(255);

            entity.HasOne(d => d.product).WithMany()
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkrhahp4f26x99lqf0kybcs79rb");
        });

        modelBuilder.Entity<product_image>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.images).HasMaxLength(255);

            entity.HasOne(d => d.product).WithMany()
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fki8jnqq05sk5nkma3pfp3ylqrt");
        });

        modelBuilder.Entity<product_size>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.sizes).HasMaxLength(255);

            entity.HasOne(d => d.product).WithMany()
                .HasForeignKey(d => d.product_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk4w69qsh5hd062xv3hqkpgpdpu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
