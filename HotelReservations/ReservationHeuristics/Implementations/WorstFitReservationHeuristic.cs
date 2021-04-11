using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationHeuristics.Implementations
{
	internal class WorstFitReservationHeuristic : ReservationHeuristic
	{
		#region Methods

		protected override int CalculateFitness(ReservationEvaluationParameter evaluationParameter)
		{
			return -evaluationParameter.GapBeforeStart - evaluationParameter.GapAfterEnd;
		}

		#endregion Methods
	}
}