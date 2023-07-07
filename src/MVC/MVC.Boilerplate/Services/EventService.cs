using AspNetCoreHero.ToastNotification.Abstractions;
using MVC.Boilerplate.Common.Helpers.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Boilerplate.Services
{
    public class EventService : IEventService
    {
        private readonly IApiClient<Events> _client;
        private readonly IApiClient<Guid> _eventClient;
        private readonly IApiClient<GetByIdEvent> _getByIdeventClient;
        public readonly ILogger<EventService> _logger;
        public EventService(IApiClient<Events> client, IApiClient<GetByIdEvent> getByIdeventClient, IApiClient<Guid> eventClient, ILogger<EventService> logger)
        {
            _client = client;
            _logger = logger;
            _eventClient = eventClient;
            _getByIdeventClient = getByIdeventClient;
            
        }
        public async Task<IEnumerable<Events>> GetEventList()
        {

            _logger.LogInformation("GetEventList Service initiated");
            var Events = await _client.GetAllAsync("Events");
            _logger.LogInformation("GetEventList Service conpleted");
            return Events.Data;
        }
        public async Task<Guid> CreateEvent(CreateEvent createEvent)
        {
            _logger.LogInformation("CreateEvents Service initiated");
            var Events = await _eventClient.PostAsync("Events", createEvent);
            _logger.LogInformation("CreateEvents Service conpleted");
            return Events.Data;
        }
        public async Task<GetByIdEvent> GetEventById(string eventId)
        {
            _logger.LogInformation("GetEventById Service initiated");
            var Events = await _getByIdeventClient.GetByIdAsync("Events/" + eventId);
            _logger.LogInformation("GetEventById Service conpleted");
            return Events.Data;
        }

        public async Task<string> DeleteEvent(string eventId)
        {
            _logger.LogInformation("DeleteEvent Service initiated");
            var Events = await _client.DeleteAsync("Events/" + eventId);
            _logger.LogInformation("DeleteEvent Service conpleted");
            return Events;
        }

        public async Task<Guid> UpdateEvent(GetByIdEvent updateEvent)
        {
            _logger.LogInformation("UpdateEvent Service initiated");
            var Events = await _eventClient.PutAsync("Events", updateEvent);
            _logger.LogInformation("UpdateEvent Service conpleted");
            return Events.Data;
        }
    }

    

}
