using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationsManagers.Implementation
{
	internal class ReservationLimit : IComparable<ReservationLimit>
	{
		#region Enums

		internal enum LimitType { Start, End, StartEnd };

		#endregion Enums

		#region Properties

		internal LimitType Type { get; private set; }

		internal int Date { get; private set; }

		#endregion Properties

		#region Constructors

		internal ReservationLimit(int date, LimitType type)
		{
			Date = date;
			Type = type;
		}

		#endregion Constructors

		#region Overrides

		public override bool Equals(object obj)
		{
			if (!(obj is ReservationLimit limit))
				return false;

			return Date == limit.Date;
		}

		public override int GetHashCode()
		{
			return Date.GetHashCode();
		}

		public int CompareTo(ReservationLimit other)
		{
			return Date - other.Date;
		}

		#endregion Overrides
	}
}
