namespace FamilyTree.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;
    using Microsoft.EntityFrameworkCore;

    public partial class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext()
        {
        }

        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<MediaEvent> MediaEvents { get; set; }

        public virtual DbSet<MediaPerson> MediaPeople { get; set; }

        public virtual DbSet<Media> Media { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Relationship> Relationships { get; set; }

        public virtual DbSet<SpecialRecord> SpecialRecords { get; set; }

        public virtual DbSet<Tree> Trees { get; set; }

        public virtual DbSet<TreePerson> TreePeople { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserTree> UserTrees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("FamilyTreeDbConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Рядок підключення до бази даних не знайдено.");
                }

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("event_pkey");

                entity.ToTable("Event");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('event_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.Age).HasColumnName("age");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");
                entity.Property(e => e.EventDate).HasColumnName("event_date");
                entity.Property(e => e.EventPlace)
                    .HasColumnType("character varying")
                    .HasColumnName("event_place");
                entity.Property(e => e.EventType)
                    .HasColumnType("character varying")
                    .HasColumnName("event_type");
                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Person).WithMany(p => p.Events)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("event_person_id_fkey");
            });

            modelBuilder.Entity<MediaEvent>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("media_event_pkey");

                entity.ToTable("Media_Event");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('media_event_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.EventId).HasColumnName("event_id");
                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.HasOne(d => d.Event).WithMany(p => p.MediaEvents)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("media_event_event_id_fkey");

                entity.HasOne(d => d.Media).WithMany(p => p.MediaEvents)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("media_event_media_id_fkey");
            });

            modelBuilder.Entity<MediaPerson>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("media_person_pkey");

                entity.ToTable("Media_Person");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('media_person_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.MediaId).HasColumnName("media_id");
                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Media).WithMany(p => p.MediaPeople)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("media_person_media_id_fkey");

                entity.HasOne(d => d.Person).WithMany(p => p.MediaPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("media_person_person_id_fkey");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("media_pkey");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('media_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");
                entity.Property(e => e.FilePath)
                    .HasColumnType("character varying")
                    .HasColumnName("file_path");
                entity.Property(e => e.MainPhoto).HasColumnName("main_photo");
                entity.Property(e => e.MediaType)
                    .HasColumnType("character varying")
                    .HasColumnName("media_type");
                entity.Property(e => e.Place)
                    .HasColumnType("character varying")
                    .HasColumnName("place");
                entity.Property(e => e.TaggedPerson).HasColumnName("tagged_person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("person_pkey");

                entity.ToTable("Person");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('person_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                entity.Property(e => e.DeathDate).HasColumnName("death_date");
                entity.Property(e => e.FirstName)
                    .HasColumnType("character varying")
                    .HasColumnName("first_name");
                entity.Property(e => e.Gender)
                    .HasColumnType("character varying")
                    .HasColumnName("gender");
                entity.Property(e => e.LastName)
                    .HasColumnType("character varying")
                    .HasColumnName("last_name");
                entity.Property(e => e.MaidenName)
                    .HasColumnType("character varying")
                    .HasColumnName("maiden_name");
                entity.Property(e => e.OtherNameVariants)
                    .HasColumnType("character varying")
                    .HasColumnName("other_name_variants");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("relationship_pkey");

                entity.ToTable("Relationship");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('relationship_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.PersonId1).HasColumnName("person_id1");
                entity.Property(e => e.PersonId2).HasColumnName("person_id2");
                entity.Property(e => e.RelationshipType)
                    .HasColumnType("character varying")
                    .HasColumnName("relationship_type");

                entity.HasOne(d => d.PersonId1Navigation).WithMany(p => p.RelationshipPersonId1Navigations)
                    .HasForeignKey(d => d.PersonId1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("relationship_person_id1_fkey");

                entity.HasOne(d => d.PersonId2Navigation).WithMany(p => p.RelationshipPersonId2Navigations)
                    .HasForeignKey(d => d.PersonId2)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("relationship_person_id2_fkey");
            });

            modelBuilder.Entity<SpecialRecord>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("special_record_pkey");

                entity.ToTable("Special_record");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('special_record_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.EventId).HasColumnName("event_id");
                entity.Property(e => e.HouseNumber).HasColumnName("house_number");
                entity.Property(e => e.Priest)
                    .HasColumnType("character varying")
                    .HasColumnName("priest");
                entity.Property(e => e.Record)
                    .HasColumnType("character varying")
                    .HasColumnName("record");
                entity.Property(e => e.RecordType)
                    .HasColumnType("character varying")
                    .HasColumnName("record_type");

                entity.HasOne(d => d.Event).WithMany(p => p.SpecialRecords)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("special_record_event_id_fkey");
            });

            modelBuilder.Entity<Tree>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("tree_pkey");

                entity.ToTable("Tree");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('tree_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
                entity.Property(e => e.PrimaryPerson).HasColumnName("primary_person");
            });

            modelBuilder.Entity<TreePerson>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("tree_person_pkey");

                entity.ToTable("Tree_Person");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('tree_person_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.PersonId).HasColumnName("person_id");
                entity.Property(e => e.TreeId).HasColumnName("tree_id");

                entity.HasOne(d => d.Person).WithMany(p => p.TreePeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tree_person_person_id_fkey");

                entity.HasOne(d => d.Tree).WithMany(p => p.TreePeople)
                    .HasForeignKey(d => d.TreeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tree_person_tree_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login).HasName("users_pkey");

                entity.Property(e => e.Login)
                    .HasColumnType("character varying")
                    .HasColumnName("login");
                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UserTree>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_tree_pkey");

                entity.ToTable("User_Tree");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('user_tree_id_seq'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.AccessType)
                    .HasColumnType("character varying")
                    .HasColumnName("access_type");
                entity.Property(e => e.TreeId).HasColumnName("tree_id");
                entity.Property(e => e.UserLogin)
                    .HasColumnType("character varying")
                    .HasColumnName("user_login");

                entity.HasOne(d => d.Tree).WithMany(p => p.UserTrees)
                    .HasForeignKey(d => d.TreeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_tree_tree_id_fkey");

                entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.UserTrees)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("user_tree_user_login_fkey");
            });
            modelBuilder.HasSequence("special_record_id_seq");
            modelBuilder.HasSequence("media_id_seq");
            modelBuilder.HasSequence("user_tree_id_seq");
            modelBuilder.HasSequence("tree_person_id_seq");
            modelBuilder.HasSequence("tree_id_seq");
            modelBuilder.HasSequence("relationship_id_seq");
            modelBuilder.HasSequence("person_id_seq");
            modelBuilder.HasSequence("event_id_seq");
            modelBuilder.HasSequence("media_event_id_seq");
            modelBuilder.HasSequence("media_person_id_seq");
            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
