using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BP.Data.Models;
using BP.Models;


namespace BP.Api.Service
{
    public interface IContactService
    {
        public ContactDto GetContactById(int id);
    }
}