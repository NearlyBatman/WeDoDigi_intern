using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeDoDigi_intern.Models;

namespace WeDoDigi_intern.CRUD_Service
{
    public class TagCrud
    {
        private readonly IMongoCollection<TagDb> tags;
        public TagCrud(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            tags = database.GetCollection<TagDb>("TagDb");
        }

        public List<TagDb> GetAllTagsObjects()
        {
            return tags.Find(tag => true).ToList();
        }

        public List<string> GetTags()
        {
            List<TagDb> tagList = tags.Find(tags => true).ToList();
            TagHolder tagHold = new TagHolder();
            foreach(TagDb t in tagList)
            {
                tagHold.tagHolder.Add(t.tagName);
            }
            return tagHold.tagHolder;
        }
        public List<string> GetNameTags(List<string> ids)
        {
            List<string> meh = new List<string>();
            TagDb kek = new TagDb();
            foreach(string s in ids)
            {
                kek = tags.Find(tag => tag.id == s).FirstOrDefault();
                meh.Add(kek.tagName);
            }
            return meh;
        }
        public List<string> GetTags (string id)
        {
            var tagHold = tags.Find(tag => tag.id == id).FirstOrDefault();
            return tagHold.idList;
        }


        public void AddTags(List<string> tagsDb, string id)
        {
            
            foreach(string s in tagsDb)
            {
                var meh = tags.Find(searchTag => searchTag.tagName.ToLower() == s.ToLower()).FirstOrDefault();
                switch (meh)
                {
                    case null:
                        TagDb newDb = new TagDb();
                        newDb.tagName = s;
                        newDb.idList.Add(id);
                        tags.InsertOne(newDb);
                        break;

                    default:
                        if(meh.idList.Contains(id))
                        {
                            break;
                        }
                        else
                        {
                            meh.idList.Add(id);
                            tags.ReplaceOne(tag => tag.id == meh.id, meh);
                        }
                        break;
                }
            }
        }
    }
}
