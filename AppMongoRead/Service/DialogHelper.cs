using AppMongoRead.Models;
using System;
using System.Collections.Generic;

namespace AppMongoRead.Service
{
    public static class DialogHelper
    {
        /// <summary>
        /// Display the main menu
        /// </summary>
        /// <returns>Menu Selection</returns>
        public static int ShowMainMenu()
        {
            int choice;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(20, 0);
            Console.WriteLine("Welcome to Guest List Manager");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write(
                "Please enter your choice: \n\n" +
                "[0] Add new guest. \n" +
                "[1] Show guests list. \n" +
                "[2] Update guest info (by ID). \n" +
                "[3] Delete guest (by ID). \n" +
                "[5] Sqlserver (by ID). \n" +
                "[4] Exit. \n" +
                "[5] Check and insert. \n" +
                "[6] Count. \n" +
                "[7] UsersUpdate. \n"+
                "[8] Delete Many. \n"+
                "[9] Export National Code. \n"+
                "[10] Check date user by date and insert score ... \n"
                );
            Console.WriteLine("-------------------------------");

            var entry = Console.ReadLine();
            if (!int.TryParse(entry, out choice))
            {
                choice = 6;
            }
            return choice;

        }
        /// <summary>
        /// Show current page title
        /// </summary>
        /// <param name="title"></param>
        private static void ShowHeader(string title)
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(20, 0);
            Console.WriteLine(title);
            Console.ResetColor();
            Console.WriteLine();
        }
        /// <summary>
        /// Display continue message
        /// </summary>
        public static void ShowContinueMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n------------------------------------------\n");
            Console.ResetColor();
            Console.WriteLine("Operation completed! \n" +
                "Press return key to continue...");
            Console.Read();
        }
        /// <summary>
        /// display 'add new guest' dialog
        /// </summary>
        /// <returns></returns>
        public static TransactionMongo ShowAddNewGuest()
        {
            ShowHeader("Add new guest");

            var guest = new TransactionMongo();

            Console.Write("Enter guest full NationalId: ");
            guest.NationId = Console.ReadLine();

            Console.Write("Enter guest Score data: ");
            guest.Score = long.Parse(Console.ReadLine());

            return guest;
        }
        /// <summary>
        /// Display 'show guest list' dialog
        /// </summary>
        /// <param name="guestsList"></param>
        public static void ShowGuestList(List<TransactionMongo> guestsList)
        {
            ShowHeader("Guests list");

            var table = new ConsoleTable("Id", "Name", "Email", "Confirmed");

            foreach (var guest in guestsList)
            {
                table.AddRow(guest.Id, guest.NationId, guest.Score, guest.TransactionDateDay);
            }
            table.Print();

            ShowContinueMessage();
        }
        /// <summary>
        /// Display 'Update guest' dialog
        /// </summary>
        /// <returns></returns>
        public static string ShowUpdateGuest()
        {
            ShowHeader("Update Guest");

            Console.WriteLine("Enter guest Id: ");

            return Console.ReadLine();

        }
        /// <summary>
        /// Display 'Delete guest' dialog
        /// </summary>
        /// <returns></returns>
        public static string ShowDeleteGuest()
        {
            ShowHeader("Delete Guest");

            Console.Write("Enter guest ID: ");

            return Console.ReadLine();
        }
        public static string ShowDeleteGuestMany()
        {
            ShowHeader("Delete Guest Many");

            Console.Write("Enter guest Date for delete many: Y/N  : ");

            return Console.ReadLine();
        }
        public static string ExportUserInfoByNationalCode()
        {
            ShowHeader("Export UserInfo By NationalCode");

            Console.Write("Begin prosess ....  ");

            return Console.ReadLine();
        }
        public static void AddUserScoreByDate()
        {
            ShowHeader("Insert User Score By Date Created");
            Console.Write("Begin prosess ....  ");
        }
    }
}
