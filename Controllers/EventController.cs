using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;
using Swashbuckle.AspNetCore.Annotations;


namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Récupère tout les évènements", Description = "Retourne les détails de tous les évènements.")]
        [SwaggerResponse(200, "Liste des évènements récupérée avec succès.")]
        [SwaggerResponse(404, "Aucun évènements non trouvés.")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupère un évènement par ID", Description = "Retourne les détails d'un évènement spécifique.")]
        [SwaggerResponse(200, "Evènement récupéré avec succès.")]
        [SwaggerResponse(404, "Evènement non trouvé.")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Ajoute un nouvel évènement", Description = "Crée un nouvel évènement.")]
        [SwaggerResponse(201, "Evènement créé avec succès.")]
        [SwaggerResponse(400, "Erreur lors de la création de l'évènement.")]
        public async Task<IActionResult> AddEvent(EventCreateDto eventDto)
        {
            await _eventService.AddEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDto.Title }, eventDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Met à jour un évènement", Description = "Met à jour les détails d'un évènement existant.")]
        [SwaggerResponse(204, "Evènement mis à jour avec succès.")]
        [SwaggerResponse(404, "Evènement non trouvé.")]
        [SwaggerResponse(400, "Erreur lors de la mise à jour de l'évènement.")]
        public async Task<IActionResult> UpdateEvent(int id, EventCreateDto eventDto)
        {
            await _eventService.UpdateEventAsync(id, eventDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprime un évènement", Description = "Supprime un évènement spécifique.")]
        [SwaggerResponse(204, "Evènement supprimé avec succès.")]
        [SwaggerResponse(404, "Evènement non trouvé.")]
        [SwaggerResponse(400, "Erreur lors de la suppression de l'évènement.")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}