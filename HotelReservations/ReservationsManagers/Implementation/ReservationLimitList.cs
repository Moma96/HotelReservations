using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationsManagers.Implementation
{
	internal class ReservationLimitList
	{
		#region Fields

		private readonly List<ReservationLimit> limits;

		#endregion Fields

		#region Properties

		internal int Count => limits.Count;

		#endregion Properties

		#region Constructiors

		internal ReservationLimitList()
		{
			limits = new List<ReservationLimit>();
		}

		#endregion Constructors

		#region Methods

		internal int FindNextLimitIndex(ReservationLimit limit)
		{
			int index = limits.BinarySearch(limit);
			if (index > 0)
			{
				// There is already limit with the same date.
				return -1;
			}

			int nextLimitIndex = ~index;
			return nextLimitIndex;
		}

		internal ReservationLimit GetLimit(int limitIndex)
		{
			if (limitIndex >= 0 && limitIndex < limits.Count)
				return limits[limitIndex];
			else
				return null;
		}

		internal void Insert(ReservationLimit limit)
		{
			int index = FindNextLimitIndex(limit);
			limits.Insert(index, limit);
		}

		#endregion Methods
	}
}
