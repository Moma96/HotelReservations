using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationHeuristics.Implementations
{
	internal class BestFitReservationHeuristic : ReservationHeuristic
	{
		#region Methods

		protected override int CalculateFitness(ReservationEvaluationParameter evaluationParameter)
		{
			return Math.Min(evaluationParameter.GapBeforeStart, evaluationParameter.GapAfterEnd);
		}

		#endregion Methods
	}
}
