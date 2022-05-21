using AppMongoRead.DAL;
using AppMongoRead.Models;
using AppMongoRead.Service;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AppMongoRead
{
	class Program
	{
		static void Main(string[] args)
		{
			string bin = Directory.GetCurrentDirectory();
			string fileName = "/connection.txt";
			string[] stringArray = new string[3];
			if (File.Exists(bin + fileName))
			{
				stringArray = File.ReadAllLines(bin + fileName);
			}
			else
			{
				using (StreamWriter sw = File.CreateText(bin + fileName))
				{
					sw.WriteLine("mongodb://172.21.102.10:27017");
					sw.WriteLine("CMDB");
					sw.WriteLine("Transaction");
				}
				stringArray = File.ReadAllLines(bin + fileName);
			}

			//By default for a local MongoDB instance connectionString = "mongodb://localhost:27017" 
			string connectionString = stringArray[0];
			string databaseName = stringArray[1];
			string collectionName = stringArray[2];

			MongoHelper database = new MongoHelper(connectionString, databaseName);


			Console.Title = "Mongo Db List Manager";

			int menuChoice;

			do
			{
				menuChoice = DialogHelper.ShowMainMenu();
				switch (menuChoice)
				{
					case 0: // Add new guest
						{
							var guest = DialogHelper.ShowAddNewGuest();

							database.InsertDocument(collectionName, guest);

							DialogHelper.ShowContinueMessage();
						}
						break;
					case 1: // Show guest list
						{
							var transactionMongo = database.LoadAllDocuments<TransactionMongo>(collectionName);
							DialogHelper.ShowGuestList(transactionMongo);

						}
						break;
					case 2: // Update guest info (by ID)
						{

							database.UpdateDocumentMany<TransactionMongo>(collectionName);
							DialogHelper.ShowContinueMessage();
						}
						break;
					case 4: // Update guest info (by ID)
						{

							Console.WriteLine("Count is : " + database.UpdateDocumentCount<TransactionMongo>(collectionName));
							DialogHelper.ShowContinueMessage();
						}
						break;
					case 3: // Delete guest (by ID)
						{
							var guestId = DialogHelper.ShowDeleteGuest();

							string guestIdGuid;

							bool isValidGuid = guestId.Length == 24 ? true : false;//Guid.TryParse(guestId, out guestIdGuid);
							if (isValidGuid)
							{
								guestIdGuid = guestId;
								database.DeleteDocument<TransactionMongo>(collectionName, guestIdGuid);
							}
							else
							{
								Console.WriteLine($"Exception: '{guestId}' is not a valid Guid!");
							}
							DialogHelper.ShowContinueMessage();
						}
						break;
					case 5:
						{
							Console.WriteLine("Sql server relation Begin ...");
							PwaHelper.CheckAndInsert();
							//PwaHelper.AddUserInMongo(finalUserAdd);
							Console.WriteLine("operation is complete.");
							Console.WriteLine("press any key to continue ... ");
							Console.ReadKey();
							DialogHelper.ShowContinueMessage();
						}
						break;
					case 6:
						{
							using (var db = new DAL.ZDemoContext())
							{
								Console.WriteLine($"Sql server records : {db.Finals.CountAsync().Result}");
								Console.WriteLine("operation is complete.");
								Console.WriteLine("press any key to continue ... ");
								Console.ReadKey();
							}
						}
						break;
					case 7:
						{
							List<string> usersUpdate = new List<string>();
							Console.WriteLine("Begin add list");
							var listUser = new ZDemoContext().Finals.ToListAsync().Result;
							for (int i = 0; i < listUser.Count; i++)
							{
								usersUpdate.Add(listUser[i].NationalCode);
							}
							Console.WriteLine("begin update ... ");
							PwaHelper.AddScoreMongo(usersUpdate);
							Console.WriteLine("press any key to continue ... ");
							Console.ReadKey();
							DialogHelper.ShowContinueMessage();
						}
						break;
					case 8: // Delete Many (by Date)
						{
							var guestId = DialogHelper.ShowDeleteGuestMany();
							if (guestId.ToLower() == "y")
							{
								Console.WriteLine("Delete Many begin ... ");
								database.DeleteDocumentMany<TransactionMongo>(collectionName, "TransactionLog");
							}
							else
							{
								return;
							}

						}
						break;
					case 9: // 
						{
							var guestId = DialogHelper.ExportUserInfoByNationalCode();
							//List<UserExport> userExportsList = new();


							ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
							ExcelPackage excel = new ExcelPackage();
							var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
							workSheet.TabColor = System.Drawing.Color.Black;

							workSheet.DefaultRowHeight = 12;
							workSheet.Row(1).Height = 20;
							workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
							workSheet.Row(1).Style.Font.Bold = true;

							workSheet.Cells[1, 1].Value = "S.No";
							workSheet.Cells[1, 2].Value = "MeliCode";
							workSheet.Cells[1, 3].Value = "Phone_Number";


							int recordIndex = 2;

							using (var db = new DAL.ZDemoContext())
							{
								var listUsers = db.UserExports;
								foreach (var item in listUsers)
								{
									var result = database.ExportUserInfoByNationalCode(item.MeliCode, "homa", "@Homa!");
									//userExportsList.Add(result);
									workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
									workSheet.Cells[recordIndex, 2].Value = item?.MeliCode;
									workSheet.Cells[recordIndex, 3].Value = result?.MobileTel;
									recordIndex++;
								}
							}


							//foreach (var article in userExportsList)
							//{
							//	workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
							//	workSheet.Cells[recordIndex, 2].Value = article.MeliCode;
							//	workSheet.Cells[recordIndex, 3].Value = article.MobileTel;
							//	recordIndex++;
							//}


							workSheet.Column(1).AutoFit();
							workSheet.Column(2).AutoFit();
							workSheet.Column(3).AutoFit();


							string p_strPath = "E:\\geeksforgeeks.xlsx";

							if (File.Exists(p_strPath))
								File.Delete(p_strPath);
							FileStream objFileStrm = File.Create(p_strPath);
							objFileStrm.Close();

							// Write content to excel file 
							File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
							//Close Excel package
							excel.Dispose();

							break;
						}
					case 10:
						{
							var users = database.FindUserByCreateDate<User>(collectionName, new DateTime(2021));

							List<string> usersUpdate = new();
							Console.WriteLine("Begin add list");
							for (int i = 0; i < users.Count; i++)
							{
								usersUpdate.Add(users[i].NationCode);
							}
							Console.WriteLine("begin update ... ");
							PwaHelper.AddScoreMongo(usersUpdate);
							Console.WriteLine("press any key to continue ... ");
							Console.ReadKey();
							DialogHelper.ShowContinueMessage();
							break;
						}
				}

			} while (menuChoice != 1000);
		}
	}
}