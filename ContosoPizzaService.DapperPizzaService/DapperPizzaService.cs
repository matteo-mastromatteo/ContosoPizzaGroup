using ContosoPizza.Entities.Models;
using ContosoPizzaService.Abstraction;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ContosoPizzaService.DapperPizzaService
{
    public class DapperPizzaService : IPizzaService
    {
        public string Name => "DapperPizzaService";
        protected DapperPizzaServiceConfiguration dapperServiceSettings { get; set; }

        public DapperPizzaService(IConfiguration configuration)
        {
            var section = configuration.GetSection(Name + "Configuration");
            if (!section.Exists())
            {
                throw new ArgumentNullException($"Configurazione {Name}Configuration mancante");
            }
            section.Bind(dapperServiceSettings);
        }
        public void Add(Pizza pizza)
        {
            using(SqlConnection conn = new SqlConnection(dapperServiceSettings.ConnectionString))
            {
                using (SqlCommand cmd=new SqlCommand())
                {
                    conn.Open();
                    pizza.Id = conn.QuerySingle<int>("Insert into Pizza(Nome, IsGlutenFree) values(@nome,@isGlutenFree);" +
                        "select scope_identity();", new { nome = pizza.Name, isGlutenFree = pizza.IsGlutenFree });

                }
            }
        }

        public void Delete(int id)
        {
            using(SqlConnection conn = new SqlConnection(dapperServiceSettings.ConnectionString))
            {
                conn.Execute("Delete from Pizza where id=@id", new { id = id });
            }
        }

        public Pizza Get(int id)
        {
            Pizza result = null;
            using (SqlConnection conn = new SqlConnection(dapperServiceSettings.ConnectionString))
            {
                conn.Open();
                result = conn.QuerySingle<Pizza>("Select * from Pizza where id=@id", new { id = id });
                
            }
            return result;
        }

        public List<Pizza> GetAll()
        {
            List<Pizza> result = null;
            using (SqlConnection conn = new SqlConnection(dapperServiceSettings.ConnectionString))
            {
                conn.Open();
                result = conn.Query<Pizza>("Select * from Pizza").ToList();
            }
            return result;
        }

        public void Update(Pizza pizza)
        {
            using (SqlConnection conn = new SqlConnection(dapperServiceSettings.ConnectionString))
            {
                conn.Execute("Update Pizza set Nome=@Nome, isGlutenFree=@IsGlutenFree where id=@Id",
                    pizza);
            }
        }
    }
}
