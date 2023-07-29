using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObjects.Model
{
    public partial class FPTHCMAdventuresDBContext : DbContext
    {
        public FPTHCMAdventuresDBContext()
        {
        }

        public FPTHCMAdventuresDBContext(DbContextOptions<FPTHCMAdventuresDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTask> EventTasks { get; set; }
        public virtual DbSet<ExchangeHistory> ExchangeHistories { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemIventory> ItemIventories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Npc> Npcs { get; set; }
        public virtual DbSet<PlayHistory> PlayHistories { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolEvent> SchoolEvents { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CapstonProjectDbConnectionString"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerName)
                    .HasMaxLength(100)
                    .HasColumnName("answer_name");

                entity.Property(e => e.IsRight).HasColumnName("is_right");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<EventTask>(entity =>
            {
                entity.ToTable("EventTask");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventTask.event_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EventTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_EventTask.task_id");
            });

            modelBuilder.Entity<ExchangeHistory>(entity =>
            {
                entity.ToTable("ExchangeHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ExchangeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exchange_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ExchangeHistory.item_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ExchangeHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_ExchangeHistory.player_id");
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.ToTable("Gift");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Decription)
                    .HasMaxLength(500)
                    .HasColumnName("decription");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Gift.event_id");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_Inventory.player_id");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.LimitExchange).HasColumnName("limit_exchange");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<ItemIventory>(entity =>
            {
                entity.ToTable("ItemIventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ItemIventories)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_ItemIventory.inventory_id");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemIventories)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemIventory.item_id");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(100)
                    .HasColumnName("location_name");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status ");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Npc>(entity =>
            {
                entity.ToTable("NPC");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Introduce)
                    .HasMaxLength(500)
                    .HasColumnName("introduce");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<PlayHistory>(entity =>
            {
                entity.ToTable("PlayHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompletedTime).HasColumnName("completed_time");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TaskPoint).HasColumnName("task_point");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_PlayHistory.player_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.PlayHistories)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_PlayHistory.task_id");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(100)
                    .HasColumnName("nickname");

                entity.Property(e => e.TotalPoint).HasColumnName("total_point");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Player.event_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Player.user_id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_Question.answer_id");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Question.major_id");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK__RefreshT__CB3C9E175E8A24CD");

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId)
                    .ValueGeneratedNever()
                    .HasColumnName("token_id");

                entity.Property(e => e.Expirydate)
                    .HasColumnType("datetime")
                    .HasColumnName("expirydate");

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RefreshToken.user_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name ");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<SchoolEvent>(entity =>
            {
                entity.ToTable("SchoolEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_SchoolEvent.event_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolEvents)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolEvent.school_id");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DurationCheckin).HasColumnName("duration_checkin");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MajorId).HasColumnName("major_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.NpcId).HasColumnName("npc_id");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.TimeOutAmount).HasColumnName("time_out_amount");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_Task.item_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Task.location_id");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Task.major_id");

                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.NpcId)
                    .HasConstraintName("FK_Task.npc_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(100)
                    .HasColumnName("gender");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasColumnName("status ");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User.role_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_User.school_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
