using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WeDoDigi_intern.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Permissions;

namespace WeDoDigi_intern.CRUD_Service
{
    public class RecipeDbService
    {
        private readonly IMongoCollection<RecipeDb> recipes;
        // Skaber forbindelse til Databasen
        public RecipeDbService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            recipes = database.GetCollection<RecipeDb>("Recipes");
        }
        // Får alle opskrifter
        public List<RecipeDb> Get()
        {
            return recipes.Find(rec => true).ToList();
        }
        // Får alle baseret på tags
        public List<RecipeDb> GetByTag(List<string> tagIds)
        {
            List<RecipeDb> kek = new List<RecipeDb>();

            foreach(string s in tagIds)
            {
                kek.Add(recipes.Find(rec => rec.Id == s).FirstOrDefault());
            }
            return kek;
        }
        // Finder en specifik opskrift
        public RecipeDb Get(string id)
        {
            return recipes.Find(rec => rec.Id == id).FirstOrDefault();
        }
        // Finder alle tags på en opskrift
        public List<string> GetTags(string id)
        {
            var rec = recipes.Find(rec => rec.Id == id).FirstOrDefault();
            return rec.Tags;
        }
        //Laver en ny opskrift

        public RecipeDb Create(RecipeDb recDb)
        {
            recipes.InsertOne(recDb);
            return recDb;
        }
        // Efter opdatering finder og erstater den nye opskrift den gamle

        public void Update(string id, RecipeDb recDbIn)
        {
            recipes.ReplaceOne(rec => rec.Id == id, recDbIn);
        }
        // Fjern opskrift (Ikke brugt i projektet noget sted)
        public void Remove (RecipeDb recDbIn)
        {
            recipes.DeleteOne(rec => rec.Id == recDbIn.Id);
        }

    }
}
