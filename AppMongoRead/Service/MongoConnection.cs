using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMongoRead.Service
{
    public class MongoConnection
    {
        public static MongoHelper GetConnectionMongo()
        {
            string bin = Directory.GetCurrentDirectory();
            string fileName = "/connection.txt";
            string[] stringArray = new string[3];
            if (File.Exists(bin + fileName))
            {
                stringArray = File.ReadAllLines(bin + fileName);
                stringArray = File.ReadAllLines(bin + fileName);

                string connectionString = stringArray[0];
                string databaseName = stringArray[1];
                string collectionName = stringArray[2];

                MongoHelper database = new MongoHelper(connectionString, databaseName);
                return database;
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

                string connectionString = stringArray[0];
                string databaseName = stringArray[1];
                string collectionName = stringArray[2];
                MongoHelper database = new MongoHelper(connectionString, databaseName);
                return database;
            }
        }
    
    }
}
