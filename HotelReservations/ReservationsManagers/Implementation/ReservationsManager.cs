using HotelReservations.ReservationHeuristics;
using System;

namespace HotelReservations.ReservationsManagers.Implementation
{
	internal class ReservationsManager : IReservationsManager
	{
		#region Constants

		internal const int NUMBER_OF_DAYS = 365;
		internal const int MAX_NUMBER_OF_ROOMS = 1000;

		#endregion Constants

		#region Fields

		private readonly int numberOfRooms;
		private readonly RoomReservations[] reservations;
		private readonly ReservationHeuristic heuristic;

		#endregion Fields

		#region Constructors

		internal ReservationsManager(int numberOfRooms, ReservationHeuristic heuristic)
		{
			if (numberOfRooms < 1 || numberOfRooms > MAX_NUMBER_OF_ROOMS)
				throw new ArgumentOutOfRangeException("numberOfRooms");

			this.numberOfRooms = numberOfRooms;
			this.heuristic = heuristic;

			reservations = new RoomReservations[numberOfRooms];
			for (int roomIndex = 0; roomIndex < numberOfRooms; roomIndex++)
				reservations[roomIndex] = new RoomReservations();
		}

		#endregion Constructors

		#region Methods

		public bool TryReserveRoom(int start, int end)
		{
			if (start < 0 || end > NUMBER_OF_DAYS || start > end)
				return false;

			for (int roomIndex = 0; roomIndex < numberOfRooms; roomIndex++)
			{
				bool reservationFits = reservations[roomIndex].TryFitReservation(start, end, out ReservationEvaluationParameter evaluationParameter);
				if (!reservationFits)
					continue;

				heuristic.EvaluateRoom(roomIndex, evaluationParameter);
			}

			int bestRoomIndex = heuristic.BestRoomIndex;
			heuristic.Reset();

			if (bestRoomIndex < 0)
			{
				// There are no available rooms from start to end date.
				return false;
			}

			reservations[bestRoomIndex].StoreReservation(start, end);

			return true;
		}

		#endregion Methods
	}
}
