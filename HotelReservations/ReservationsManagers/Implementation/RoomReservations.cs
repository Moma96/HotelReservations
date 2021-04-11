using HotelReservations.ReservationHeuristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationsManagers.Implementation
{
	internal class RoomReservations
	{
		#region Fields
		
		private readonly ReservationLimitList limits;

		#endregion Fields

		#region Constructors

		internal RoomReservations()
		{
			limits = new ReservationLimitList();
		}

		#endregion Constructors

		#region Methods

		internal bool TryFitReservation(int start, int end, out ReservationEvaluationParameter evaluationParameter)
		{
			evaluationParameter = null;

			ReservationLimit startLimit = new ReservationLimit(start, ReservationLimit.LimitType.Start);
			ReservationLimit endLimit = new ReservationLimit(end, ReservationLimit.LimitType.End);

			int nextStartIndex = limits.FindNextLimitIndex(startLimit);
			if (nextStartIndex < 0)
			{
				// This room is already reserved on the start date.
				return false;
			}

			int nextEndIndex = limits.FindNextLimitIndex(endLimit);
			if (nextEndIndex < 0)
			{
				// This room is already reserved on the end date.
				return false;
			}

			if (nextStartIndex != nextEndIndex)
			{
				// This room is reserved somewhere between start and end date.
				return false;
			}

			ReservationLimit limitAfterEnd = limits.GetLimit(nextEndIndex);
			if (limitAfterEnd?.Type == ReservationLimit.LimitType.End)
			{
				// This reservation is within the other one.
				return false;
			}

			// Since limitAfterEnd is Start limit, we know that limitBeforeStart is End limit (or StartEnd).
			ReservationLimit limitBeforeStart = limits.GetLimit(nextStartIndex - 1);

			int startAfterEnd = limitAfterEnd != null ? limitAfterEnd.Date : ReservationsManager.NUMBER_OF_DAYS;
			int endBeforeStart = limitBeforeStart != null ? limitBeforeStart.Date : -1;

			int gapBeforeStart = start - endBeforeStart - 1;
			int gapAfterEnd = startAfterEnd - end - 1;

			evaluationParameter = new ReservationEvaluationParameter(gapBeforeStart, gapAfterEnd);
			return true;
		}

		internal void StoreReservation(int start, int end)
		{
			if (start == end)
			{
				limits.Insert(new ReservationLimit(start, ReservationLimit.LimitType.StartEnd));
			}
			else
			{
				limits.Insert(new ReservationLimit(start, ReservationLimit.LimitType.Start));
				limits.Insert(new ReservationLimit(end, ReservationLimit.LimitType.End));
			}
		}

		#endregion Methods
	}
}
