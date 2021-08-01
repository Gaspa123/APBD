using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        private static string connectionString = 
            "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";
        static int counterId = 0;

        //private List<Animal> animalList;

        public void Db()
        {
           // animalList = new();
            var sql = "select * from animal;";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    counterId++;
                }
            }
        }

        public bool AddAnimal(Animal animal)
        {
            if (!AnimalExists(animal))
                return false;
          
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@name", animal.Name);
                cmd.Parameters.AddWithValue("@description", animal.Description);
                cmd.Parameters.AddWithValue("@area", animal.Area);
                cmd.Parameters.AddWithValue("@category", animal.Category);
                cmd.Connection = conn;
                cmd.CommandText = $"insert into Animal(Name, Description, Category, Area)" +
                    $" values(@name,@description,@category,@area);";
                conn.Open();
                Console.WriteLine(cmd.ExecuteNonQuery());

            }
                return true;
        }

       

        public List<Animal> GetAnimal()
        {
            var animalList = new List<Animal>();
            var sql = "select * from animal order by name ASC;";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {

                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                    animalList.Add(new Animal
                    {
                        Name = (dr["Name"].ToString()),
                        Description = (dr["Description"].ToString()),
                        Category = (dr["Category"].ToString()),
                        Area = (dr["Area"].ToString())
                    }) ; 
                }
            }
            return animalList;
        }

        public List<Animal> GetAnimalQuery(string query)
        {
            var animalList = new List<Animal>();
                      using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@query", query);
                cmd.Connection = conn;
                cmd.CommandText = "select * from animal order by @query ASC;";
                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    animalList.Add(new Animal
                    {
                        Name = (dr["Name"].ToString()),
                        Description = (dr["Description"].ToString()),
                        Category = (dr["Category"].ToString()),
                        Area = (dr["Area"].ToString())
                    });
                }
            }
            return animalList;
        }

        public bool ModifyAnimal(Animal animal,int id)
        {
            var animalList = new List<Animal>();
            if (!AnimalExists(animal))
                return false;
            
            Animal tmpAnimal = null;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                cmd.CommandText = $"select * from animal where IdAnimal = @id";
                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tmpAnimal = new Animal
                    {
                        Name = (dr["Name"].ToString()),
                        Description = (dr["Description"].ToString()),
                        Category = (dr["Category"].ToString()),
                        Area = (dr["Area"].ToString())
                    };
                }
            }
            if (tmpAnimal.Equals(null))
                return false;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@name", animal.Name);
                cmd.Parameters.AddWithValue("@description", animal.Description);
                cmd.Parameters.AddWithValue("@area", animal.Area);
                cmd.Parameters.AddWithValue("@category", animal.Category);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE animal SET Name=@name,Description=@description," +
              $"Area=@area,Category=@category WHERE IdAnimal = @id; ";
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteAnimal(int id)
        {
            int flag;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM ANIMAL WHERE IdAnimal = @id;";
                conn.Open();
                flag = cmd.ExecuteNonQuery();               
            }
            if (flag == 0)
                return false;
            return true;

        }

        public bool AnimalExists(Animal animal)
        {
            var animalList = new List<Animal>();
            var sql = "select * from animal;";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    animalList.Add(new Animal
                    {
                        Name = (dr["Name"].ToString()),
                        Description = (dr["Description"].ToString()),
                        Category = (dr["Category"].ToString()),
                        Area = (dr["Area"].ToString())
                    });
                }
            }
            foreach (Animal a in animalList)
            {
                if (a.Name == animal.Name && a.Description == animal.Description && a.Category == animal.Category && a.Area == animal.Area)
                    return false;
            }
            return true;
        }
    }
}
