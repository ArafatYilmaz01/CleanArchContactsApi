using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BP.Data.Models;
using BP.Models;

namespace BP.Api.Service
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ContactDto GetContactById(int id)
        {
            // Örnek veri
            Contact dbContact = getDummyContact();

            ContactDto resultContact = new ContactDto();
           
            _mapper.Map(dbContact,resultContact);
            return resultContact;
        }
        private Contact getDummyContact()
        {
            return new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}