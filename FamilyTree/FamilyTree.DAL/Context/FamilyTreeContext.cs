using System;
using System.Collections.Generic;
using FamilyTree.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.DAL.Context
{
    public partial class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext()
        {
        }

        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Дерево> Деревоs { get; set; }

        public virtual DbSet<ДеревоОсоба> ДеревоОсобаs { get; set; }

        public virtual DbSet<Звязок> Звязокs { get; set; }

        public virtual DbSet<Користувач> Користувачі { get; set; }

        public virtual DbSet<КористувачДерево> КористувачДеревоs { get; set; }

        public virtual DbSet<Медіа> Медіаs { get; set; }

        public virtual DbSet<МедіаОсоба> МедіаОсобаs { get; set; }

        public virtual DbSet<МедіаПодія> МедіаПодіяs { get; set; }

        public virtual DbSet<Особа> Особаs { get; set; }

        public virtual DbSet<Подія> Подіяs { get; set; }

        public virtual DbSet<СпеціальнийЗапис> СпеціальнийЗаписs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=FamilyTree;Username=postgres;Password=-----");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Дерево>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Дерево_pkey");

                entity.ToTable("Дерево");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Назва).HasColumnType("character varying");
            });

            modelBuilder.Entity<ДеревоОсоба>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Дерево_Особа_pkey");

                entity.ToTable("Дерево_Особа");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdДерева).HasColumnName("id_дерева");
                entity.Property(e => e.IdОсоби).HasColumnName("id_особи");

                entity.HasOne(d => d.IdДереваNavigation).WithMany(p => p.ДеревоОсобаs)
                    .HasForeignKey(d => d.IdДерева)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Дерево_Особа_id_дерева_fkey");

                entity.HasOne(d => d.IdОсобиNavigation).WithMany(p => p.ДеревоОсобаs)
                    .HasForeignKey(d => d.IdОсоби)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Дерево_Особа_id_особи_fkey");
            });

            modelBuilder.Entity<Звязок>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Звязок_pkey");

                entity.ToTable("Звязок");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdОсоби1).HasColumnName("id_особи1");
                entity.Property(e => e.IdОсоби2).HasColumnName("id_особи2");
                entity.Property(e => e.ТипЗвязку)
                    .HasColumnType("character varying")
                    .HasColumnName("Тип_звязку");

                entity.HasOne(d => d.IdОсоби1Navigation).WithMany(p => p.ЗвязокIdОсоби1Navigations)
                    .HasForeignKey(d => d.IdОсоби1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Звязок_id_особи1_fkey");

                entity.HasOne(d => d.IdОсоби2Navigation).WithMany(p => p.ЗвязокIdОсоби2Navigations)
                    .HasForeignKey(d => d.IdОсоби2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Звязок_id_особи2_fkey");
            });

            modelBuilder.Entity<Користувач>(entity =>
            {
                entity.HasKey(e => e.Логін).HasName("Користувач_pkey");

                entity.ToTable("Користувач");

                entity.Property(e => e.Логін).HasColumnType("character varying");
                entity.Property(e => e.Пароль).HasColumnType("character varying");
            });

            modelBuilder.Entity<КористувачДерево>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Користувач_Дерево_pkey");

                entity.ToTable("Користувач_Дерево");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdДерева).HasColumnName("id_дерева");
                entity.Property(e => e.ЛогінКористувача)
                    .HasColumnType("character varying")
                    .HasColumnName("логін_користувача");
                entity.Property(e => e.ТипДоступу)
                    .HasColumnType("character varying")
                    .HasColumnName("Тип_доступу");

                entity.HasOne(d => d.IdДереваNavigation).WithMany(p => p.КористувачДеревоs)
                    .HasForeignKey(d => d.IdДерева)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Користувач_Дерево_id_дерева_fkey");

                entity.HasOne(d => d.ЛогінКористувачаNavigation).WithMany(p => p.КористувачДеревоs)
                    .HasForeignKey(d => d.ЛогінКористувача)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Користувач_Дерев_id_користувача_fkey");
            });

            modelBuilder.Entity<Медіа>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Медіа_pkey");

                entity.ToTable("Медіа");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ГоловнеФото).HasColumnName("Головне_фото");
                entity.Property(e => e.Місце).HasColumnType("character varying");
                entity.Property(e => e.Опис).HasColumnType("character varying");
                entity.Property(e => e.ПозначеніОсоби).HasColumnName("Позначені_особи");
                entity.Property(e => e.ТипМедіа)
                    .HasColumnType("character varying")
                    .HasColumnName("Тип_медіа");
                entity.Property(e => e.ШляхДоФайлу)
                    .HasColumnType("character varying")
                    .HasColumnName("Шлях_до_файлу");
            });

            modelBuilder.Entity<МедіаОсоба>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Медіа_Особа_pkey");

                entity.ToTable("Медіа_Особа");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdМедіа).HasColumnName("id_медіа");
                entity.Property(e => e.IdОсоби).HasColumnName("id_особи");

                entity.HasOne(d => d.IdМедіаNavigation).WithMany(p => p.МедіаОсобаs)
                    .HasForeignKey(d => d.IdМедіа)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Медіа_Особа_id_медіа_fkey");

                entity.HasOne(d => d.IdОсобиNavigation).WithMany(p => p.МедіаОсобаs)
                    .HasForeignKey(d => d.IdОсоби)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Медіа_Особа_id_особи_fkey");
            });

            modelBuilder.Entity<МедіаПодія>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Медіа_Подія_pkey");

                entity.ToTable("Медіа_Подія");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdМедіа).HasColumnName("id_медіа");
                entity.Property(e => e.IdПодії).HasColumnName("id_події");

                entity.HasOne(d => d.IdМедіаNavigation).WithMany(p => p.МедіаПодіяs)
                    .HasForeignKey(d => d.IdМедіа)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Медіа_Подія_id_медіа_fkey");

                entity.HasOne(d => d.IdПодіїNavigation).WithMany(p => p.МедіаПодіяs)
                    .HasForeignKey(d => d.IdПодії)
                    .HasConstraintName("Медіа_Подія_id_події_fkey");
            });

            modelBuilder.Entity<Особа>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Особа_pkey");

                entity.ToTable("Особа");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Імя).HasColumnType("character varying");
                entity.Property(e => e.ІншіВаріантиІмені)
                    .HasColumnType("character varying")
                    .HasColumnName("Інші_варіанти_імені");
                entity.Property(e => e.ГоловнаОсоба).HasColumnName("Головна_особа");
                entity.Property(e => e.ДатаНародження).HasColumnName("Дата_народження");
                entity.Property(e => e.ДатаСмерті).HasColumnName("Дата_смерті");
                entity.Property(e => e.ДівочеПрізвище)
                    .HasColumnType("character varying")
                    .HasColumnName("Дівоче_прізвище");
                entity.Property(e => e.Прізвище).HasColumnType("character varying");
                entity.Property(e => e.Стать).HasColumnType("character varying");
            });

            modelBuilder.Entity<Подія>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Подія_pkey");

                entity.ToTable("Подія");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ДатаПодії).HasColumnName("Дата_події");
                entity.Property(e => e.МісцеПодії)
                    .HasColumnType("character varying")
                    .HasColumnName("Місце_події");
                entity.Property(e => e.Опис).HasColumnType("character varying");
                entity.Property(e => e.ОсобаId).HasColumnName("Особа_id");
                entity.Property(e => e.ТипПодії)
                    .HasColumnType("character varying")
                    .HasColumnName("Тип_події");

                entity.HasOne(d => d.Особа).WithMany(p => p.Подіяs)
                    .HasForeignKey(d => d.ОсобаId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Подія_Особа_id_fkey");
            });

            modelBuilder.Entity<СпеціальнийЗапис>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Спеціальний_запис_pkey");

                entity.ToTable("Спеціальний_запис");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Запис).HasColumnType("character varying");
                entity.Property(e => e.НомерБудинку).HasColumnName("Номер_будинку");
                entity.Property(e => e.ПодіяId).HasColumnName("Подія_id");
                entity.Property(e => e.Священик).HasColumnType("character varying");
                entity.Property(e => e.ТипЗапису)
                    .HasColumnType("character varying")
                    .HasColumnName("Тип_запису");

                entity.HasOne(d => d.Подія).WithMany(p => p.СпеціальнийЗаписs)
                    .HasForeignKey(d => d.ПодіяId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Спеціальний_запис_Подія_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}