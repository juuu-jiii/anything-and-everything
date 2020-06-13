using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Deviation: the offset of one wall pair from the previous

namespace wallDodger
{

	class Zigzag : Terrain
	{
		// Fields

		// The maximum length a deviation can be, measured in walls.
		protected int maxDeviationLength;

		// Variable to track current wall in deviation
		protected int currentDeviationWall;

		// Array to store multiplicative scalars representing left and right vector movement
		protected int[] deviationDirections;

		// Variable to track current deviation direction
		protected int currentDeviationDirection;

		// The size of the aforementioned offset
		protected Vector2 deviationSize;

		// Constructor
		public Zigzag() : base()
		{
			// After at most 5 wall pairs deviation direction changes.
			maxDeviationLength = 5;

			currentDeviationWall = 0;

			// -1 represents left movement, 1 right movement
			deviationDirections = new int[] { -1, 1 };

			// Total zigzag length = 25 wall pairs
			TotalWallPairsInTerrain = 25;

			// Each wall pair is offset 40px from the previous
			deviationSize = new Vector2(40, 0); 
			
			// Random starting deviation direction
			currentDeviationDirection = deviationDirections[generator.Next(2)];
		}

		/// <summary>
		/// Helper method that changes currentDeviationDirection.
		/// </summary>
		protected void SwitchDirection()
		{
			// If currently travelling left...
			if (currentDeviationDirection == deviationDirections[0])
			{
				// ...travel right next...
				currentDeviationDirection = deviationDirections[1];
				currentDeviationWall = 0; // Reset this to avoid interfering with future GenerateNext() calls.
			}
			// ...and vice versa.
			else
			{
				currentDeviationDirection = deviationDirections[0];
				currentDeviationWall = 0;
			}
		}

		/// <summary>
		/// Helper method that checks to see if SwitchDirection() needs to be called.
		/// </summary>
		/// <param name="wallManager">
		/// The WallManager object.
		/// </param>
		/// <returns>
		/// Returns true if a direction change is needed, and false otherwise.
		/// </returns>
		protected bool DirectionSwitchNeeded(float previousLeftWallX, float previousRightWallX)
		{
			// A direction change is needed if the next wall will be spawned off-screen given the current conditions
			//		OR if the current deviation is already long enough.
			// deviationSize.X * currentDeviationDirection will be negative for leftward movement and vice versa.
			if (previousLeftWallX + Wall.WallWidth + (deviationSize.X * currentDeviationDirection) < 0
				|| previousRightWallX + (deviationSize.X * currentDeviationDirection) > Game1.WindowWidth
				|| currentDeviationWall >= maxDeviationLength)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Generates an offset for the next wall pair to be appended.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// The x-coordinate of the most recently added left wall.
		/// </param>
		/// <param name="previousRightWallX">
		/// The x-coordinate of the most recently added right wall.
		/// </param>
		/// <returns>
		/// Returns a float offset to be applied to the next wall pair.
		/// </returns>
		public override float GenerateNext(float previousLeftWallX, float previousRightWallX)
		{
			// Check if a direction change is needed to prevent walls from 
			//		spawning off-screen.
			if (DirectionSwitchNeeded(previousLeftWallX, previousRightWallX))
			{
				SwitchDirection();
			}

			// Increment current deviation wall to represent the pair that was 
			//		just added.
			currentDeviationWall++;

			// Calculate and return the offset after ensuring 
			return (deviationSize.X * (int)currentDeviationDirection);
		}

		/// <summary>
		/// Resets applicable variables to allow for future zigzag generations.
		/// </summary>
		public void Reset()
		{
			// Randomly obtain the next starting direction
			currentDeviationDirection = deviationDirections[generator.Next(2)];

			// Reset this to avoid interfering with future GenerateNext() calls.
			currentDeviationWall = 0;
		}
	}
}
