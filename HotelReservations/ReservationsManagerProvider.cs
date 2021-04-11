using HotelReservations.ReservationHeuristics;
using HotelReservations.ReservationHeuristics.Implementations;
using HotelReservations.ReservationsManagers;
using HotelReservations.ReservationsManagers.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservations
{
	/// <summary>
	/// Hotel reservations manager provider.
	/// </summary>
	public static class ReservationsManagerProvider
	{
		#region Methods

		/// <summary>
		/// Instantiates new <see cref="IReservationsManager"/> implementation based on the <see cref="IConfiguration"/> argument.
		/// </summary>
		/// <param name="numberOfRooms">Number of rooms of the </param>
		/// <returns><see cref="IReservationsManager"/> implementation.</returns>
		public static IReservationsManager GetManager(int numberOfRooms)
		{
			IHost host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddTransient<IReservationsManager>(provider =>
						new ReservationsManager(numberOfRooms,
							ResolveManagerHeuristic(provider.GetRequiredService<IConfiguration>())));
				})
				.Build();

			return host.Services.GetService<IReservationsManager>();
		}

		private static ReservationHeuristic ResolveManagerHeuristic(IConfiguration config)
		{
			string heuristic = config.GetValue<string>("ReservationsManagerHeuristic");

			switch (heuristic)
			{
				case "WorstFit":
					return new WorstFitReservationHeuristic();
				case "BestFit":
				default:
					return new BestFitReservationHeuristic();
			}
		}

		#endregion Methods
	}
}
