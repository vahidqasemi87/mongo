using System.IO;

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
                    sw.WriteLine("mongodb://10.15.6.97:27017");
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
