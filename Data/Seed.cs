using MyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Locations.Any())
                return;

            var locations = new List<Location>
            {
                new Location
                {
                    Name = "Centre de Conférence",
                    Address = "123 Rue A",
                    City = "Paris",
                    Country = "France",
                    Capacity = 500
                },
                new Location
                {
                    Name = "Hôtel de Ville",
                    Address = "456 Rue B",
                    City = "Lyon",
                    Country = "France",
                    Capacity = 300
                }
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();

            var rooms = new List<Room>
            {
                new Room { Name = "Salle A", Capacity = 100, LocationId = locations[0].Id },
                new Room { Name = "Salle B", Capacity = 200, LocationId = locations[0].Id },
                new Room { Name = "Salle C", Capacity = 150, LocationId = locations[1].Id }
            };
            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            var participants = new List<Participant>
            {
                new Participant { FirstName = "Alice", LastName = "Dupont", Email = "alice@example.com", Company = "Company A", JobTitle = "Manager" },
                new Participant { FirstName = "Bob", LastName = "Martin", Email = "bob@example.com", Company = "Company B", JobTitle = "Developer" }
            };
            context.Participants.AddRange(participants);
            context.SaveChanges();

            var speakers = new List<Speaker>
            {
                new Speaker { FirstName = "John", LastName = "Doe", Bio = "Expert en technologie", Email = "john@example.com", Company = "Tech Corp" },
                new Speaker { FirstName = "Jane", LastName = "Smith", Bio = "Experte en design", Email = "jane@example.com", Company = "Design Inc" }
            };
            context.Speakers.AddRange(speakers);
            context.SaveChanges();

            var events = new List<Event>
            {
                new Event
                {
                    Title = "Conférence Tech",
                    Description = "Conférence sur les technologies de pointe",
                    StartDate = DateTime.UtcNow.AddDays(10),
                    EndDate = DateTime.UtcNow.AddDays(11),
                    Status = "Programmé",
                    Category = "Technologie",
                    LocationId = locations[0].Id
                },
                new Event
                {
                    Title = "Atelier Design",
                    Description = "Atelier sur le design moderne",
                    StartDate = DateTime.UtcNow.AddDays(20),
                    EndDate = DateTime.UtcNow.AddDays(21),
                    Status = "Programmé",
                    Category = "Design",
                    LocationId = locations[1].Id
                }
            };
            context.Events.AddRange(events);
            context.SaveChanges();

            var sessions = new List<Session>
            {
                new Session
                {
                    Title = "Introduction à l'IA",
                    Description = "Les bases de l'intelligence artificielle",
                    StartTime = DateTime.UtcNow.AddDays(10).AddHours(1),
                    EndTime = DateTime.UtcNow.AddDays(10).AddHours(2),
                    EventId = events[0].Id,
                    RoomId = rooms[0].Id
                },
                new Session
                {
                    Title = "Design Thinking",
                    Description = "Processus de design thinking",
                    StartTime = DateTime.UtcNow.AddDays(20).AddHours(1),
                    EndTime = DateTime.UtcNow.AddDays(20).AddHours(3),
                    EventId = events[1].Id,
                    RoomId = rooms[2].Id
                }
            };
            context.Sessions.AddRange(sessions);
            context.SaveChanges();

            var eventParticipants = new List<EventParticipant>
            {
                new EventParticipant { EventId = events[0].Id, ParticipantId = participants[0].Id, RegistrationDate = DateTime.UtcNow },
                new EventParticipant { EventId = events[0].Id, ParticipantId = participants[1].Id, RegistrationDate = DateTime.UtcNow }
            };
            context.EventParticipants.AddRange(eventParticipants);
            context.SaveChanges();

            var ratings = new List<Rating>
            {
                new Rating { Score = 5, Comment = "Session excellente !", SessionId = sessions[0].Id, ParticipantId = participants[0].Id },
                new Rating { Score = 4, Comment = "Très intéressant.", SessionId = sessions[1].Id, ParticipantId = participants[1].Id }
            };
            context.Ratings.AddRange(ratings);
            context.SaveChanges();

            var sessionSpeakers = new List<SessionSpeaker>
            {
                new SessionSpeaker { SessionId = sessions[0].Id, SpeakerId = speakers[0].Id, Role = "Intervenant" },
                new SessionSpeaker { SessionId = sessions[1].Id, SpeakerId = speakers[1].Id, Role = "Intervenant" }
            };
            context.SessionSpeakers.AddRange(sessionSpeakers);
            context.SaveChanges();
        }
    }
}