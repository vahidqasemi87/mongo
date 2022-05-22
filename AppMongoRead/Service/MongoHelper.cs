using AppMongoRead.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

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
			try
			{
				var collection = db.GetCollection<T>(collectionName);
				collection.InsertOne(document);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message} from MongoHelper");
				Console.ReadKey();
				Environment.Exit(0);
			}

		}
		/// <summary>
		/// لود کردن همه اطلاعات در مجموعه
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <returns></returns>
		public List<T> LoadAllDocuments<T>(string collectionName)
		{
			try
			{
				var collection = db.GetCollection<T>(collectionName);
				var result = collection.Find(new BsonDocument()).ToList();
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error : ");
				Console.WriteLine($"{ex.Message}");
				Console.ReadKey();
				return new List<T>();
			}

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
		public bool CheckExistNationCode<T>(string collectionName, string nationCode)
		{
			var collection = db.GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Eq("NationCode", nationCode);
			var result = collection.Find(filter).Any();
			return result;
		}
		public bool CheckExistScore<T>(string collectionName, string nationCode, string ruleId, string planId)
		{
			var collection = db.GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Eq("NationId", nationCode) & Builders<T>.Filter.Eq("RuleId", ruleId) & Builders<T>.Filter.Eq("PlanId", planId);
			var result = collection.Find(filter).Any();
			return result;
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

		public void UpdateDocumentMany<T>(string collectionName)
		{
			Console.WriteLine("Begin ... ");
			var inject = db.GetCollection<T>(collectionName);
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

		public long UpdateDocumentCount<T>(string collectionName)
		{
			Console.WriteLine("begin ... ");
			var collection = db.GetCollection<T>(collectionName);
			var arrayFilter = Builders<T>.Filter.Gt("Score", 300) & Builders<T>.Filter.Eq("RuleId", new ObjectId("567fbfc7ab344566c0caecf2"));
			var count = collection.CountDocuments(arrayFilter);
			return count;
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
		public void DeleteDocumentMany<T>(string collectionName, string id)
		{
			var collection = db.GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Lte("Id", id);
			collection.DeleteMany(filter);

		}
		public void UpdateDocumentManyPwa<T>(string collectionName)
		{
			Console.WriteLine("Begin ... ");
			var inject = db.GetCollection<T>(collectionName);
			Console.WriteLine("inject is complete.");

			var arrayFilter = Builders<T>.Filter.Eq("RuleId", new ObjectId("567fbfc7ab344566c0caecf2"));

			var count01 = inject.CountDocuments(arrayFilter);

			Console.WriteLine("count is complete.");

			Console.WriteLine($"from :  {count01}");
			var collection = db.GetCollection<T>(collectionName).Find(arrayFilter).ToListAsync();
			foreach (var item in (collection.Result as List<Models.TransactionMongo>))
			{
				long score = 0;
				score = (item.Score / 100) + 30;
				Console.WriteLine($"Score cal is : {item.Score} \t\t   Score new is {score}");
				var result = inject.UpdateOne(Builders<T>.Filter.Eq("_id", new ObjectId(item.Id)), Builders<T>.Update.Set("Score", score));
			}

			Console.WriteLine("Done. :)");
		}
		public UserExport ExportUserInfoByNationalCode(string meliCode, string uid, string pwd)
		{

			var client = new RestClient("https://club.rb24.ir/InqueyPanel/NationalCodeExist");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/json");
			request.AddJsonBody(new { nationalCode = meliCode, username = uid, password = pwd });
			IRestResponse response = client.Execute(request);
			var result = JsonConvert.DeserializeObject<UserExport>(response.Content);
			return result;
		}
		public List<User> FindUserByCreateDate<T>(string collectionName, DateTime? date)
		{
			var beginDate = new DateTime(2015, 12, 27, 0, 0, 0, DateTimeKind.Utc);
			var ebdDate = new DateTime(2015, 12, 27, 23, 59, 01, DateTimeKind.Utc);

			Console.WriteLine("Begin Find ... ");

			var collection = db.GetCollection<User>(collectionName);
			//var filterBuilder = Builders<User>.Filter;
			//var filter = filterBuilder.Eq(c=>c.CreateDate,date);

			//var ff = Builders<User>.Filter.Lte(c => c.CreateDate, ebdDate) & Builders<User>.Filter.Gte(c => c.CreateDate, beginDate);
			//var rr = collection.Find(ff);
			//var count = rr.CountDocuments();

			var result = collection.Find(x => x.CreateDate >= beginDate && x.CreateDate <= ebdDate).ToList();
			return result;
		}
		public List<string> FindUserInTransaction<T>(string collectionName, string ruleId)
		{
			List<string> str = new();
			var collection = db.GetCollection<TransactionMongo>(collectionName);
			var result = collection.Find(x => x.RuleId == ruleId).ToList();
			foreach (var item in result)
			{
				str.Add(item.NationId);
			}
			return str;
		}
		public List<string> GetUserNotTrasaction<T>(string collectionName, IEnumerable<string> str)
		{
			var collection = db.GetCollection<User>(collectionName);
			var listallUser = collection.Find(x=>x.CreateDate >= new DateTime(2021,03,21,0,0,0)).ToList().Select(s=>s.NationCode);

			var dns = listallUser.Except(str).ToList();
			int count = dns.Count;
			return dns;
		}

	}
}