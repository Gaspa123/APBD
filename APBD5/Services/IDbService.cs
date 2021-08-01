using APBD5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Services
{
    public interface IDbService
    {
        public Task<string> AddItem(Item item);

        public Task<string> AddItem2(Item item);

    }
}
