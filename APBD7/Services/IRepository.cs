using APBD7.DTOs;
using APBD7.DTOs.Response;
using APBD7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Services
{
    public interface IRepository
    {
        public Task<dynamic> GetTripsAsync();

        public Task DeleteClientsAsync(int id);

        public Task AddClientsAsync(int id, CreateClientAndTrip client);
    }
}
