using HotelReservations;
using HotelReservations.ReservationsManagers;
using System;

namespace ConsoleApp
{
	class EntryPoint
	{
		private static int ReadIntegerInputData()
		{
			int result;
			while (!int.TryParse(Console.ReadLine().Trim(), out result))
				Console.WriteLine("Please enter a integer value.");

			return result;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Please enter number of rooms:");
			int numberOfRooms = ReadIntegerInputData();

			IReservationsManager manager = ReservationsManagerProvider.GetManager(numberOfRooms);

			string response;
			do
			{
				Console.WriteLine("Please enter the start date of your reservation:");
				int start = ReadIntegerInputData();

				Console.WriteLine("Please enter the end date of your reservation:");
				int end = ReadIntegerInputData();

				if (manager.TryReserveRoom(start, end))
					Console.WriteLine("Reservation is accepted.");
				else
					Console.WriteLine("Reservation is not accepted.");

				Console.WriteLine("Do you want to make another reservation? (y/n)");
				response = Console.ReadLine().Trim().ToLower();
			}
			while (response == "y" || response == "yes");
		}
	}
}
