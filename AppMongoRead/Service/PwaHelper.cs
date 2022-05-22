using AppMongoRead.DAL;
using AppMongoRead.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
			try
			{
				var database = MongoConnection.GetConnectionMongo();
				database.InsertDocument("User", new User
				{
					FirstName = finalsUser.FirstName,
					LastName = finalsUser?.LastName,
					NationCode = finalsUser?.NationalCode,
					MobileTel = finalsUser.Mobile,
				});
				Console.Clear();
				Console.WriteLine("Insert Complete");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error : {ex.Message} from PwaHelper");
				Console.ReadKey();
				Environment.Exit(0);
			}

			GC.Collect();
		}
		public static void AddScoreMongo(List<string> nationId)
		{
			string rid = "567bbdc0ab344566c0caecd3";
			string pid = null;

			System.Globalization.PersianCalendar pcr = new System.Globalization.PersianCalendar();

			int year = pcr.GetYear(DateTime.Now);
			int month = pcr.GetMonth(DateTime.Now);
			int day = pcr.GetDayOfMonth(DateTime.Now);

			var database = MongoConnection.GetConnectionMongo();
			for (int i = 0; i < nationId.Count; i++)
			{
				Console.WriteLine($"Users id = {i}\t {nationId[i]}");

				database.InsertDocument("Transaction", new TransactionMongo
				{
					Score = 100,
					NationId = nationId[i],
					//حتما تغییر یابد
					RuleId = rid,
					//حتما تغییر یابد
					PlanId = pid,
					CreationOnDay = day,
					CreationOnMonth = month,
					CreationOnYear = year,
					TransactionDate = DateTime.Now,
					TransactionDateDay = day,
					TransactionDateMonth = month,
					TransactionDateYear = year,
					CreationOn = DateTime.Now,
					Description = "عضویت در باشگاه مشتریان",
				});
			}

			Console.Clear();
			Console.WriteLine("Insert Complete   :)");
			GC.Collect();
		}
		//public static void CheckScoreExist()
		//{
		//	var database = MongoConnection.GetConnectionMongo();
		//	var result = database.CheckExistNationCode<User>("User", nationCode);
		//	return result;
		//}
		//public static bool CheckExistScore<T>(string collectionName, string nationCode, string ruleId, string planId)
		//{
		//	var database = MongoConnection.GetConnectionMongo();
		//	var collection = database.CheckExistScore<TransactionMongo>(nationCode,ruleId,planId);
		//	var filter = Builders<T>.Filter.Eq("NationId", nationCode) & Builders<T>.Filter.Eq("RuleId", ruleId) & Builders<T>.Filter.Eq("PlanId", planId);
		//	var result = collection.Find(filter).Any();
		//	return result;
		//}
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
	}
}