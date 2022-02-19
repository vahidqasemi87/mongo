using AppMongoRead.DAL;
using AppMongoRead.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMongoRead.Service
{
    public class PwaHelper
    {
        private IMongoDatabase db;
        public PwaHelper(string connectionString, string databasename)
        {
            //ایجاد یک ارتباط با دیتابیس
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(databasename);
        }
        public static void CheckAndInsert()
        {

            int counter = 0;
            using (var db = new ZDemoContext())
            {
                var listSql = db.Finals.ToList();
                for (int index = 0; index < listSql.Count; index++)
                {
                    if (!CheckExist(listSql[index].NationalCode))
                    {
                        AddUserInMongo(listSql[index]);
                        counter++;
                        Console.WriteLine($"Insert count : {counter}");
                    }
                }
            }
        }

        public static void AddUserInMongo(Final finalsUser)
        {
            var database = MongoConnection.GetConnectionMongo();
            //for (int i = 0; i < finalsUsers.Count; i++)
            //{
            database.InsertDocument("User", new User
            {
                FirstName = finalsUser.FirstName,
                LastName = finalsUser?.LastName,
                NationCode = finalsUser?.NationalCode,
                MobileTel = finalsUser.Mobile,
            });
            //}
            Console.Clear();
            Console.WriteLine("Insert Complete");
            GC.Collect();
        }
        public static void AddUserSample()
        {
            var database = MongoConnection.GetConnectionMongo();
            var user = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                NationCode = "1234567890",
                MobileTel = "0217777777"
            };
            database.InsertDocument("User", user);
            Console.WriteLine("Insert Complete.");
        }
        public static bool CheckExist(string nationCode)
        {
            var database = MongoConnection.GetConnectionMongo();
            var result = database.CheckExistNationCode<User>("User", nationCode);
            return result;
        }
        public static void UpdatePwa()
        {
            Console.WriteLine("Begin update operation ... ");
            //var inject = db.GetCollection<T>(collectionName);
            var database = MongoConnection.GetConnectionMongo();
            database.UpdateDocumentMany<User>("user");
            Console.WriteLine("inject is complete.");

            //
            var arrayFilter = Builders<T>.Filter.Gt("Score", 301) & Builders<T>.Filter.Eq("RuleId", new ObjectId("567fbfc7ab344566c0caecf2"));
            //var arrayFilter = Builders<T>.Filter.Gt("Score", 35) /*& Builders<T>.Filter.Eq("RuleId", new ObjectId("567fbfc7ab344566c0caecf2"))*/;
            var count01 = inject.CountDocuments(arrayFilter);
            //
            //var count = db.GetCollection<T>(collectionName).CountDocumentsAsync(new BsonDocument()).Result;
            Console.WriteLine("count is complete.");
            //int skip = 0, limit = 10;

            //while (limit <= count)
            //{
            //Console.WriteLine($"Skip : {skip}\t\tLimit : {limit} \t\t from {count01}");
            Console.WriteLine($"from :  {count01}");
            var collection = db.GetCollection<T>(collectionName).Find(arrayFilter).ToListAsync();/*.Skip(skip).Limit(limit).ToListAsync()*/;
            foreach (var item in (collection.Result as List<Models.TransactionMongo>))
            {
                //if (item.Score > 35 /*&& item.RuleId == "567fbfc7ab344566c0caecf2"*/)
                //if (item.Score >= 301 && item.RuleId == "567fbfc7ab344566c0caecf2")
                //{
                long score = 0;
                //score = (item.Score / 2) + 10;
                score = (item.Score / 100) + 30;
                Console.WriteLine($"Score cal is : {item.Score} \t\t   Score new is {score}");
                //score = new Random().Next(10, 100);
                var result = inject.UpdateOne(Builders<T>.Filter.Eq("_id", new ObjectId(item.Id)), Builders<T>.Update.Set("Score", score));
                //}
            }
            //skip += 1;
            //limit += 10;
            //}

            Console.WriteLine("Done. :)");

            ////var arrayFilter = Builders<T>.Filter.Eq("Score", 3);
            //var arrayFilter = Builders<T>.Filter.Gt("Score", 300) & Builders<T>.Filter.Eq("RuleId", new ObjectId("567fbfc7ab344566c0caecf2"));
            ////var count = collection.CountDocuments(arrayFilter);
            ////return count;
            //long data = 0;
            //if (document is Models.TransactionMongo)
            //{
            //    data = (document as Models.TransactionMongo).Score / 100;
            //    data = data + 30;
            //}

            //var arrayUpdate = Builders<T>.Update.Set("Score", data);
            //var result = collection.UpdateMany(arrayFilter, arrayUpdate);

            //return result;

            //var collection = db.GetCollection<T>(collectionName);
            //foreach (var item in collection.Find("").ToList())
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}