using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallDodger
{
	class Obstacle : Terrain
	{
		// Fields
		public int ObstacleWidth { get; protected set; }

		public Obstacle()
		{
			TotalWallPairsInTerrain = 1;

			// Obstacles will be square in shape.
			ObstacleWidth = Wall.WallHeight;
		}

		/// <summary>
		/// Generates the x-coordinate of the obstacle object to be spawned.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// The x-coordinate of the most recently added left wall.
		/// </param>
		/// <param name="previousRightWallX">
		/// The x-coordinate of the most recently added right wall.
		/// </param>
		/// <returns>
		/// Returns the x-coordinate of the obstacle object to be spawned as a float.
		/// </returns>
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			// Obstacle object will spawn directly in the middle of the pathway.
			return ((previousLeftWallX + Wall.WallWidth + previousRightWallX) / 2 - ObstacleWidth / 2);
		}
	}
}
