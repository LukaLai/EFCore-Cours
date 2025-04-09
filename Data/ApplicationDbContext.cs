using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;

namespace MyWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SessionSpeaker> SessionSpeakers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(true);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.ParticipantId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Participant)
                .WithMany(p => p.EventParticipants)
                .HasForeignKey(ep => ep.ParticipantId);

            modelBuilder.Entity<SessionSpeaker>()
                .HasKey(ss => new { ss.SessionId, ss.SpeakerId });

            modelBuilder.Entity<SessionSpeaker>()
                .HasOne(ss => ss.Session)
                .WithMany(s => s.SessionSpeakers)
                .HasForeignKey(ss => ss.SessionId);

            modelBuilder.Entity<SessionSpeaker>()
                .HasOne(ss => ss.Speaker)
                .WithMany(sp => sp.SessionSpeakers)
                .HasForeignKey(ss => ss.SpeakerId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Location)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Rooms)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Sessions)
                .HasForeignKey(s => s.EventId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Sessions)
                .HasForeignKey(s => s.RoomId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Session)
                .WithMany(s => s.Ratings)
                .HasForeignKey(r => r.SessionId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Participant)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ParticipantId);
        }
    }
}