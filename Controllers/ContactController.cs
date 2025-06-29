using System;
using System.Text.Json;
using BP.Api.Service;
using BP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IConfiguration configuration, IContactService contactService, ILogger<ContactController> logger)
        {
            _configuration = configuration;
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("GET /api/contact çağrıldı");
            return _configuration.GetSection("ReadMe").Value;
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<ContactDto> GetContactById(int id)
        {
            _logger.LogInformation("GET /api/contact/{Id} çağrıldı", id);

            var contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                _logger.LogWarning("Contact bulunamadı. Id: {Id}", id);
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(400)]
        public ActionResult<ContactDto> CreateContact([FromBody] ContactDto contactDto)
        {
 var json = JsonSerializer.Serialize(contactDto);
    _logger.LogInformation("Yeni bir contact oluşturuluyor: {ContactJson}", json);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Geçersiz model gönderildi: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            // Normalde veritabanına kaydedersin
            // var created = _contactService.CreateContact(contactDto);
            return Ok(contactDto); // Şimdilik geleni döndürüyoruz
        }
    }
}
