using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations.ReservationHeuristics
{
	internal abstract class ReservationHeuristic
	{
		#region Properties

		internal int BestRoomFitness { get; private set; }
		internal int BestRoomIndex { get; private set; }

		#endregion Properties

		#region Constructors

		internal ReservationHeuristic()
		{
			Reset();
		}

		#endregion Constructors

		#region Methods
		
		protected abstract int CalculateFitness(ReservationEvaluationParameter evaluationParameter);

		internal void EvaluateRoom(int roomIndex, ReservationEvaluationParameter evaluationParameter)
		{
			int roomFitness = CalculateFitness(evaluationParameter);
			if (roomFitness < BestRoomFitness)
			{
				BestRoomFitness = roomFitness;
				BestRoomIndex = roomIndex;
			}
		}

		internal void Reset()
		{
			BestRoomIndex = -1;
			BestRoomFitness = int.MaxValue;
		}

		#endregion Methods
	}
}
