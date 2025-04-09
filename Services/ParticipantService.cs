using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class ParticipantService
    {
        private readonly IRepository<Participant> _participantRepository;

        public ParticipantService(IRepository<Participant> participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<IEnumerable<ParticipantDto>> GetAllParticipantsAsync()
        {
            var participants = await _participantRepository.GetAllAsync();
            return participants.Select(p => new ParticipantDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                JobTitle = p.JobTitle
            });
        }

        public async Task<ParticipantDto?> GetParticipantByIdAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            if (participant == null) return null;

            return new ParticipantDto
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                Email = participant.Email,
                Company = participant.Company,
                JobTitle = participant.JobTitle
            };
        }

        public async Task AddParticipantAsync(ParticipantDto participantDto)
        {
            var participant = new Participant
            {
                FirstName = participantDto.FirstName,
                LastName = participantDto.LastName,
                Email = participantDto.Email,
                Company = participantDto.Company,
                JobTitle = participantDto.JobTitle
            };

            await _participantRepository.AddAsync(participant);
        }

        public async Task UpdateParticipantAsync(int id, ParticipantDto participantDto)
        {
            var existingParticipant = await _participantRepository.GetByIdAsync(id);
            if (existingParticipant == null) throw new Exception("Participant not found");

            existingParticipant.FirstName = participantDto.FirstName;
            existingParticipant.LastName = participantDto.LastName;
            existingParticipant.Email = participantDto.Email;
            existingParticipant.Company = participantDto.Company;
            existingParticipant.JobTitle = participantDto.JobTitle;

            await _participantRepository.UpdateAsync(existingParticipant);
        }

        public async Task DeleteParticipantAsync(int id)
        {
            await _participantRepository.DeleteAsync(id);
        }
    }
}