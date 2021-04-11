namespace HotelReservations.ReservationsManagers
{
	/// <summary>
	/// Hotel reservations manager.
	/// </summary>
	public interface IReservationsManager
	{
		/// <summary>
		/// Tries to find available room to make a reservation from start to end date.
		/// </summary>
		/// <param name="start">Start date of reservation.</param>
		/// <param name="end">End date of reservation.</param>
		/// <returns>Returns true if reservation is accepted, otherwise false.</returns>
		bool TryReserveRoom(int start, int end);
	}
}