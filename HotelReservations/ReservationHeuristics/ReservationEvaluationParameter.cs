using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationHeuristics
{
	internal class ReservationEvaluationParameter
	{
		#region Properties

		internal int GapBeforeStart { get; private set; }
		internal int GapAfterEnd { get; private set; }

		#endregion Properties

		#region Constructors

		internal ReservationEvaluationParameter(int gapBeforeStart, int gapAfterEnd)
		{
			GapBeforeStart = gapBeforeStart;
			GapAfterEnd = gapAfterEnd;
		}

		#endregion Constructors
	}
}
