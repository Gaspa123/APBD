using APBD7.DTOs;
using APBD7.DTOs.Response;
using APBD7.Exceptions;
using APBD7.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Services
{
    public class Repository : IRepository
    {
        private readonly _2019SBDContext _context;

        public Repository(_2019SBDContext context)
        {
            _context = context;
        }

        public async Task<dynamic> GetTripsAsync()
        {
            return  (from s in _context.Trips.Select
                         (s => new  GetClientAndTripAndCountry
                         {
                             Name = s.Name,
                             Description = s.Description,
                             DateFrom = s.DateFrom,
                             DateTo = s.DateTo,
                             MaxPeople = s.MaxPeople,
                             Countries = s.CountryTrips.Select(c => new { c.IdCountryNavigation.Name }).ToList(),
                             Clients = s.ClientTrips.Select(c => new { c.IdClientNavigation.FirstName, c.IdClientNavigation.LastName }).ToList()

                         }).OrderByDescending(d => d.DateFrom)
                         select s);

        }
        public async Task DeleteClientsAsync(int id)
        {
            
            if (!await _context.Clients.AnyAsync(w => w.IdClient == id))
                throw new DeleteClientException("I can not");
            if (!(_context.ClientTrips.Count(c => c.IdClientNavigation.IdClient == id) == 0))
                 throw new DeleteClientException("I can not");
            Client client = _context.Clients.Where(w => w.IdClient == id).Single();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }


        public async Task AddClientsAsync(int id, CreateClientAndTrip client)
        {
            if (!_context.Clients.Select(s => s.Pesel).Contains(client.Pesel))
            {
                Client newClient = new Client
                {
                    IdClient = await _context.Clients.MaxAsync(max => max.IdClient) + 1,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Pesel = client.Pesel,
                    Telephone = client.Telephone

                };
                await _context.Clients.AddAsync(newClient);
                await _context.SaveChangesAsync();
            }
            else
            {
                if (await _context.ClientTrips.AnyAsync(a => a.IdTrip == id && client.TripName== a.IdTripNavigation.Name && a.IdClient == _context.Clients.Where(w => w.Pesel == client.Pesel).Select(s => s.IdClient).Single()))
                    throw new AlreadySignException("Already sign on this trip");
                else 
                {
                    if (!await _context.Trips.AnyAsync(a => a.IdTrip == client.IdTrip && a.Name == client.TripName ))
                        throw new NotExistTripException("This trip does not exist!");
                }
            }
            var culture = new CultureInfo("pl-PL");
            ClientTrip newClientTrip = new ClientTrip
            {
                IdTrip = id,
                IdClient = _context.Clients.Where(w=>w.Pesel == client.Pesel).Select(s=>s.IdClient).Single(),
                PaymentDate = client.PaymentDate,
                RegisteredAt = DateTime.Now
            };
            await _context.ClientTrips.AddAsync(newClientTrip);
            await _context.SaveChangesAsync(); 
        }
    }
}
