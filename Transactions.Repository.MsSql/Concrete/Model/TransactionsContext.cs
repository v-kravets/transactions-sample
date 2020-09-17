using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transactions.Model.Concrete;
using Transactions.Services.Abstract;

namespace Transactions.Repository.MsSql.Concrete.Model
{
    public partial class TransactionsContext : DbContext, ICurrencyTransactionRepository
    {
        public TransactionsContext()
        {
        }

        public TransactionsContext(DbContextOptions<TransactionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CurrencyTransaction> CurrencyTransaction { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=WINSERV;Initial Catalog=Transactions;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CurrencyTransaction>(entity =>
            {
                entity.HasIndex(e => e.CurrencyId)
                    .HasName("CurrencyIdx");

                entity.HasIndex(e => e.TimestampUtc)
                    .HasName("DateTimeUtcIdx");

                entity.HasIndex(e => e.StatusId)
                    .HasName("StatusIdx");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TimestampUtc).HasColumnType("datetime");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CurrencyTransaction)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK__CurrencyT__Curre__3F466844");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CurrencyTransaction)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__CurrencyT__Statu__403A8C7D");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
