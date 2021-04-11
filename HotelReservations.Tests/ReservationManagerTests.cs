using HotelReservations.ReservationsManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelReservations.Tests
{
	/// <summary>
	/// Testing class for <see cref="IReservationsManager"/> implementations provided with arguments from appsettings.json.
	/// </summary>
	[TestClass]
	public class ReservationManagerTests
	{
		/// <summary>
		/// Tests input constraints for requests.
		/// </summary>
		[TestMethod]
		public void RequestsOutsidePlanningPeriodDeclined()
		{
			IReservationsManager manager = ReservationsManagerProvider.GetManager(1);

			Assert.IsFalse(manager.TryReserveRoom(-4, 2));
			Assert.IsFalse(manager.TryReserveRoom(200, 400));
			Assert.IsFalse(manager.TryReserveRoom(5, 3));
		}

		/// <summary>
		/// Tests if all reservation requests will be accepted.
		/// </summary>
		[TestMethod]
		public void AllRequestsAccepted()
		{
			IReservationsManager manager = ReservationsManagerProvider.GetManager(3);

			Assert.IsTrue(manager.TryReserveRoom(0, 5));
			Assert.IsTrue(manager.TryReserveRoom(7, 13));
			Assert.IsTrue(manager.TryReserveRoom(3, 9));
			Assert.IsTrue(manager.TryReserveRoom(5, 7));
			Assert.IsTrue(manager.TryReserveRoom(6, 6));
			Assert.IsTrue(manager.TryReserveRoom(0, 4));
		}

		/// <summary>
		/// Tests if the last reservation request will be declined.
		/// </summary>
		[TestMethod]
		public void OneRequestDeclined()
		{
			IReservationsManager manager = ReservationsManagerProvider.GetManager(3);

			Assert.IsTrue(manager.TryReserveRoom(1, 3));
			Assert.IsTrue(manager.TryReserveRoom(2, 5));
			Assert.IsTrue(manager.TryReserveRoom(1, 9));
			Assert.IsFalse(manager.TryReserveRoom(0, 15));
		}

		/// <summary>
		/// Tests if the last reservation request will be accepted after declined one.
		/// </summary>
		[TestMethod]
		public void RequestsDeclinedThenAccepted()
		{
			IReservationsManager manager = ReservationsManagerProvider.GetManager(3);

			Assert.IsTrue(manager.TryReserveRoom(1, 3));
			Assert.IsTrue(manager.TryReserveRoom(0, 15));
			Assert.IsTrue(manager.TryReserveRoom(1, 9));
			Assert.IsFalse(manager.TryReserveRoom(2, 5));
			Assert.IsTrue(manager.TryReserveRoom(4, 9));
		}

		/// <summary>
		/// Tests complex reservation requests.
		/// </summary>
		[TestMethod]
		public void ComplexRequests()
		{
			IReservationsManager manager = ReservationsManagerProvider.GetManager(2);

			Assert.IsTrue(manager.TryReserveRoom(0, 4));
			Assert.IsTrue(manager.TryReserveRoom(1, 3));
			Assert.IsFalse(manager.TryReserveRoom(2, 3));
			Assert.IsTrue(manager.TryReserveRoom(5, 5));
			Assert.IsTrue(manager.TryReserveRoom(4, 10));
			Assert.IsTrue(manager.TryReserveRoom(10, 10));
			Assert.IsTrue(manager.TryReserveRoom(6, 7));
			Assert.IsFalse(manager.TryReserveRoom(8, 10));
			Assert.IsTrue(manager.TryReserveRoom(8, 9));
		}
	}
}
