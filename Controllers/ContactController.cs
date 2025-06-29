using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IConfiguration configuration;
        private readonly IContactService contactService;

        public ContactController(IConfiguration Configuration, IContactService contact)
        {
            configuration = Configuration;
            contactService = contact;
        }

        [HttpGet]
        public string Get()
        {
            // TODO: Implement logic to retrieve all contacts
            return configuration.GetSection("ReadMe").Value;
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        [ProducesResponseType(typeof(ContactDto), 200)]
        public ContactDto GetContactById(int id)
        {
            return contactService.GetContactById(id);

        }
        [HttpPost()]
        public ContactDto CreateContact([FromBody] ContactDto contactDto)
        {
            //return contactService.CreateContact(contactDto);
            return contactDto; // For now, just return the received DTO
        }
    }
}