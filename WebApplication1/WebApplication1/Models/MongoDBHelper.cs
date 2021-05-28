using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace WebApplication1.Models
{
    public class MongoDBHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://faran_user:faran123@advanceconsulting.qrak3.mongodb.net/AdvanceConsultingDatabase?connect=replicaSet";
        public static string MongoDatabase = "AdvanceConsultingDatabase";
        public static IMongoCollection<Models.Users> users_collection { get; set; }
        public static IMongoCollection<Models.Campaigns> campaigns_collection { get; set; }
        public static IMongoCollection<Models.SmallBusinesses> smallbusinesses_collection { get; set; }
        internal static void MongoDBConnectionService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static bool MongoDBUserExists(string email, string password)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                users_collection = Models.MongoDBHelper.database.GetCollection<Models.Users>("users");

                var filter = Builders<Models.Users>.Filter.Where(s => s.email == email && s.password == password);
                var result = users_collection.Find(filter).ToList();

                if (result.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal static string MongoDBUserType(string email)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                users_collection = Models.MongoDBHelper.database.GetCollection<Models.Users>("users");

                var filter = Builders<Models.Users>.Filter.Where(s => s.email == email);
                var result = users_collection.Find(filter).Single().userType;

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal static List<Campaigns> MongoDBGetCampaignRequests()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                campaigns_collection = Models.MongoDBHelper.database.GetCollection<Models.Campaigns>("campaigns");

                var filter = Builders<Models.Campaigns>.Filter.Where(s => s.status == "Pending");
                var result = campaigns_collection.Find(filter).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static List<SmallBusinesses> MongoDBGetSmallBusinessRequests()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                smallbusinesses_collection = Models.MongoDBHelper.database.GetCollection<Models.SmallBusinesses>("smallbusinesses");

                var filter = Builders<Models.SmallBusinesses>.Filter.Where(s => s.status == "Pending");
                var result = smallbusinesses_collection.Find(filter).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static void MongoDBUpdateCampaign(object id, string status)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                campaigns_collection = Models.MongoDBHelper.database.GetCollection<Models.Campaigns>("campaigns");

                var filter = Builders<Models.Campaigns>.Filter.Eq("_id", id);
                var update = Builders<Models.Campaigns>.Update.Set("status", status);
                var result = campaigns_collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static void MongoDBUpdateSmallBusiness(object id, string status)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                smallbusinesses_collection = Models.MongoDBHelper.database.GetCollection<Models.SmallBusinesses>("smallbusinesses");

                var filter = Builders<Models.SmallBusinesses>.Filter.Eq("_id", id);
                var update = Builders<Models.SmallBusinesses>.Update.Set("status", status);
                var result = smallbusinesses_collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static ObjectId MongoDBUploadFile(Campaigns model)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                var fs = new GridFSBucket(database);

                var id = UploadFile(fs, model.ImageFile);

                return id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static ObjectId MongoDBUploadFile(SmallBusinesses model)
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
                var fs = new GridFSBucket(database);

                var id = UploadFile(fs, model.ImageFile);

                return id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static ObjectId UploadFile(GridFSBucket fs, HttpPostedFileBase imageFile)
        {
            try
            {
                var t = Task.Run<ObjectId>(() =>
                {
                    return
                    fs.UploadFromStreamAsync(imageFile.FileName, imageFile.InputStream);

                });

                return t.Result;
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}