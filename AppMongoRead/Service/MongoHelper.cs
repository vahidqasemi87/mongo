using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMongoRead.Service
{
    public class MongoHelper
    {
        private IMongoDatabase db;
        public MongoHelper(string connectionString, string databasename)
        {
            //ایجاد یک ارتباط با دیتابیس
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(databasename);
        }

        /// <summary>
        /// درج یک سند در مجموعه
        /// </summary>
        /// <typeparam name="T">Document data type</typeparam>
        /// <param name="collectionName">Collection name</param>
        /// <param name="document">Document</param>
        public void InsertDocument<T>(string collectionName, T document)
        {
            var collection = db.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }
        /// <summary>
        /// لود کردن همه اطلاعات در مجموعه
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public List<T> LoadAllDocuments<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);

            return collection.Find(new BsonDocument()).ToList();
        }
        /// <summary>
        /// لود کردن یک سند با آی دی
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T LoadDocumentById<T>(string collectionName, string id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        /// <summary>
        /// بروز کردن یکی به روش ریپلیس
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>

        public ReplaceOneResult UpdateDocumentReplaceOne<T>(string collectionName, string id, T document)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var result = collection.ReplaceOne(filter, document);
            return result;
        }
        /// <summary>
        /// بروز کردن یکی به روش آپدیت
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>

        public UpdateResult UpdateDocumentUpdateOne<T>(string collectionName, string id, T document)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<T>.Update.Set("NationId", 999999999);
            var result = collection.UpdateOne(filter, update);
            return result;
        }
        /// <summary>
        /// بروز کردن یکی به روش آپدیت منی
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>

        public void UpdateDocumentMany<T>(string collectionName/*, string id, T document*/)
        {
            Console.WriteLine("Begin ... ");
            int counter = 0;
            var inject = db.GetCollection<T>(collectionName);
            //var count = db.GetCollection<T>(collectionName).CountDocumentsAsync(new BsonDocument()).Result;
            //int flag = 0;
            // int skip = 0, limit = 100;
            //do
            //{
            //Console.WriteLine($"Skip :{skip}\tLimit : {limit}\tflag:{flag}");
            var collection = db.GetCollection<T>(collectionName).Find(new BsonDocument()).ToListAsync();/*.Skip(skip).Limit(limit).ToListAsync()*/
            foreach (var item in (collection.Result as List<Models.TransactionMongo>))
            {
                counter++;
                if (item.Score > 301 && item.RuleId == "567fbfc7ab344566c0caecf2")
                {
                long score = 0;
                score = (item.Score / 100) + 30;
                var result = inject.UpdateOne(Builders<T>.Filter.Eq("_id", new ObjectId(item.Id)), Builders<T>.Update.Set("Score", score));
                }
                //flag++;
                Console.Clear();
                Console.WriteLine($"Counter : {counter}");
                
            }
            //skip += 1;
            //limit += 100;

            //} while (count > flag);

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
        /// <summary>
        /// Insert document into collection if it does not already exist, or update it if it does
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>
        public void UpsertDocument<T>(string collectionName, string id, T document)
        {
            var collection = db.GetCollection<T>(collectionName);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                document,
                new ReplaceOptions { IsUpsert = true });
        }
        /// <summary>
        /// Delete document by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        public void DeleteDocument<T>(string collectionName, string id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);

        }

    }
}