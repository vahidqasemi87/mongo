using AppMongoRead.Models;
using AppMongoRead.Service;
using System;
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
                    case 7:
                        {
                            PwaHelper.AddUserSample();
                            Console.ReadKey();
                        }
                        break;
                }

            } while (menuChoice != 1000);
        }
    }
}
