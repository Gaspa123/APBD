using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        public void Db();
        public List<Animal> GetAnimal();

        public List<Animal> GetAnimalQuery(string query);

        public bool AddAnimal(Animal animal);

        public bool ModifyAnimal(Animal animal, int id);

        public bool DeleteAnimal(int id);
    }
}
