using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WeDoDigi_intern.Controllers;
using WeDoDigi_intern.Models;


namespace WeDoDigi_intern.CRUD_Service
{
    public class TestCrud
    {
        private readonly IMongoCollection<TestUser> testing;
        private TestUser user;

        public TestCrud(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            testing = database.GetCollection<TestUser>("Test");
            user = testing.Find(user => user.Id == "5f61e0170f4e42da4fdfd916").FirstOrDefault();

        }

        public TestUser Create(TestUser test)
        {
            testing.InsertOne(test);
            return test;
        }

        public TestUser AddFoodPlan(FoodPlan rDb, string id)
        {
            // TestUser t = testing.Find(user => user.Id == id).FirstOrDefault();
            user.shoppingList.Add("Make");
            var meh = testing.FindOneAndUpdate(Builders<TestUser>.Filter.Eq("Id", id), Builders<TestUser>.Update.AddToSet("Mealplan", rDb));
            return user;
        }

        public TestUser AddRecipe(RecipeDb rDb)
        {
            user = testing.FindOneAndUpdate(Builders<TestUser>.Filter.Eq("Id", user.Id), Builders<TestUser>.Update.AddToSet("Recipes", rDb));
            
            return user;
        }
        public string CheckFav(RecipeDb rDb, string id)
        {
            int favIndex = user.favorites.FindIndex(i => i.Id == id);
            if (favIndex >= 0)
            {
                user.favorites.Add(rDb);
                return $"{rDb.Name} has been added to favorites";
            }
            else
            {
                user.favorites.Remove(rDb);
                return $"{rDb.Name} has been removed from favorites";
            }
        }
        public List<RecipeDb> GetRecipes()
        {
            return user.recipeDbs;
        }
        public List<RecipeDb> GetFavorites()
        {
            return user.favorites;
        }

        public RecipeDb GetRecipe(string recId)
        {
            return user.recipeDbs.Find(rec => rec.Id == recId);
        }

        public RecipeDb Update(RecipeDb rDb, string id)
        {
            RecipeDb x = user.recipeDbs.First(i => i.Id == id);
            var index = user.recipeDbs.IndexOf(x);
            // Vend tilbage senere
            if (index != -1)
            {
                testing.FindOneAndUpdate(Builders<TestUser>.Filter.Eq("Id", user.Id), Builders<TestUser>.Update.Pull("Recipes", id));
                string a = GetRandomHexNumber(24);
                testing.FindOneAndUpdate(Builders<TestUser>.Filter.Eq("Id", user.Id), Builders<TestUser>.Update.AddToSet("Recipes", a));
            }
            int favIndex = user.favorites.FindIndex(i => i.Id == id);
            if (favIndex >= 0)
            {
                user.favorites[favIndex] = rDb;
            }
            return rDb;
        }
        private static string GetRandomHexNumber(int hex)
        {
            Random random = new Random();
            byte[] buffer = new byte[hex / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (hex % 2 == 0)
            {
                return result;
            }
            return result + random.Next(7).ToString("X");
        }
    }
}
