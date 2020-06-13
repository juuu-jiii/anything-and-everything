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
	class Roundabout : Obstacle
	{
		// Variable controlling deviations between wall pairs in 3rd and 6th stages of generation
		private Vector2 deviationSize;

		// Variable impacting gap size by a constant factor
		private int gapSizeReductionFactor;

		// Variable to "bookmark" the gap size at various parts throughout generation
		private int gapSizeAnchor;

		// Variable tracking the distance from previousLeftWallX to WallManager.InitialLeftWallX
		private float distanceToCentre;

		// Variable tracking the amount of distance to "nudge" the next wall pair. Used in 1st
		//		stage of terrain generation.
		private float step;

		public Roundabout()
		{
			TotalWallPairsInTerrain = 32;

			// Wall pairs will be offset 10px from their previous ones each time this is applied.
			deviationSize = new Vector2(10, 0);

			// Deviating by 10 and setting gapSizeReductionFactor = 10 will result in the left walls 
			//		just forming a straight line, because the deviation accounts for the gap size
			//		change. To make the left side symmetrical, gapSizeReductionFactor must be twice
			//		as large as deviationSize. Think about it.
			gapSizeReductionFactor = 20;
		}

		// Inherits Obstacle's GenerateNext - will be used for the actual rotary section of code

		/// <summary>
		/// Generates the Roundabout terrain, step-by-step.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// The x-coordinate of the most recently added left wall.
		/// </param>
		/// <param name="currentWallPairInTerrain">
		/// The current wall pair being generated in the terrain.
		/// </param>
		/// <param name="gapSize">
		/// The size of the gap between walls.
		/// </param>
		/// <returns>
		/// Returns a 3-tuple, consisting of:
		///		1. the x-position of the next left wall, as a float,
		///		2. the updated gap size, as an int, and
		///		3. whether to spawn an obstacle in the pathway, as a bool.
		/// </returns>
		public Tuple<float, int, bool> GenerateNext(
			float previousLeftWallX,
			int currentWallPairInTerrain,
			int gapSize)
		{
			// 1st stage of generation: centre-aligning the path
			if (currentWallPairInTerrain < 5)
			{
				distanceToCentre = WallManager.InitialLeftWallXPosition - previousLeftWallX;

				// Splitting up distanceToCentre into 5 parts, "nudging" the path toward the centre
				//		by an equal amount each time.
				// Step is recalculated each time the method is called, because distanceToCentre 
				//		decreases with each call. 
				// Each time, step is calculated by dividing distanceToCentre by the number of parts
				//		left (from the initial 5).
				step = distanceToCentre / (5 - currentWallPairInTerrain);

				// "Nudge" the next wall by step px.
				// gapSize remains constant.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					previousLeftWallX + step,
					gapSize,
					false);
			}

			// 2nd/final stage of generation: mini-straightaways
			else if (currentWallPairInTerrain < 10 || currentWallPairInTerrain > 27)
			{
				// Just copy the previous wall's position.
				// gapSize remains constant.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					previousLeftWallX, 
					gapSize,
					false);
			}

			// 3rd stage of generation: decreasing width
			else if (currentWallPairInTerrain < 15)
			{
				// Reduce gapSize and bookmark it using the anchor variable.
				gapSizeAnchor = gapSize - gapSizeReductionFactor;

				// Shift the next wall's position using deviationSize.
				// gapSize is reduced.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					previousLeftWallX + deviationSize.X,
					gapSizeAnchor,
					false);
			}

			// 4th stage of generation (part 1): increasing width (sqrt func) WITHOUT obstacle
			else if (currentWallPairInTerrain <= 16)
			{
				// Sqrt current position, and use it in subsequent calls. Eventually the graph flattens, 
				//		creating a somewhat quarter circle appearance.
				// With a sqrt func, the resulting value change starts out drastic, but quickly smoothens
				//		out, creating a curve-like path.
				// Multiply the expression within the sqrt to make the value changes more drastic, and the
				//		curve therefore more dramatic.
				float wallToAddX = (float)Math.Sqrt(90 * (previousLeftWallX + Wall.WallWidth)) - Wall.WallWidth;

				// The next wall's position uses the value calculated above.
				// gapSize is calculated by doubling the distance between the left wall's top-right corner and  
				//		the centre of the screen, to create a mirror effect with the corresponding right wall.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					wallToAddX,
					(int)(2 * (Game1.WindowWidth / 2 - (wallToAddX + Wall.WallWidth))),
					false);
			}

			// 4th stage of generation (part 2): increasing width (sqrt func) WITH obstacle
			else if (currentWallPairInTerrain <= 18)
			{
				// Sqrt current position, and use it in subsequent calls. Eventually the graph flattens, 
				//		creating a somewhat quarter circle appearance.
				// With a sqrt func, the resulting value change starts out drastic, but quickly smoothens
				//		out, creating a curve-like path.
				// Multiply the expression within the sqrt to make the value changes more drastic, and the
				//		curve therefore more dramatic.
				float wallToAddX = (float)Math.Sqrt(90 * (previousLeftWallX + Wall.WallWidth)) - Wall.WallWidth;

				// Leverage the gap size prior to this stage, marked by gapSizeAnchor, to calculate the obstacle 
				//		width needed to maintain said gap size.
				// This works because gapSizeAnchor is only updated in the third stage.
				float obstacleX = wallToAddX + Wall.WallWidth + gapSizeAnchor;
				ObstacleWidth = (int)(2 * (Game1.WindowWidth / 2 - obstacleX));

				// The next wall's position uses the value calculated above.
				// gapSize is calculated the same way as before.
				// An obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					wallToAddX,
					(int)(2 * (Game1.WindowWidth / 2 - (wallToAddX + Wall.WallWidth))),
					true);
			}

			// 4th stage of generation (part 3): decreasing width (inverse of sqrt func) WITH obstacle
			else if (currentWallPairInTerrain <= 19)
			{
				// Needed to "undo" the effects of the sqrt func to generate the remainder of the half-circle. 
				// Keeping in mind f*f^-1(x) = x, the inverse of the above function was used to achieve this.
				float wallToAddX = (float)(Math.Pow(previousLeftWallX + Wall.WallWidth, 2) / 90 - Wall.WallWidth);

				// Leverage the gap size prior to this stage, marked by gapSizeAnchor, to calculate the obstacle 
				//		width needed to maintain said gap size.
				// This works because gapSizeAnchor is only updated in the third stage.
				float obstacleX = wallToAddX + Wall.WallWidth + gapSizeAnchor;
				ObstacleWidth = (int)(2 * (Game1.WindowWidth / 2 - obstacleX));

				// The next wall's position uses the value calculated above.
				// gapSize is calculated the same way as before.
				// An obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					wallToAddX,
					(int)(2 * (Game1.WindowWidth / 2 - (wallToAddX + Wall.WallWidth))),
					true);
			}

			// 4th stage of generation (part 4): decreasing width (inverse of sqrt func) WITHOUT obstacle
			else if (currentWallPairInTerrain <= 22)
			{
				// Needed to "undo" the effects of the sqrt func to generate the remainder of the half-circle. 
				// Keeping in mind f*f^-1(x) = x, the inverse of the above function was used to achieve this.
				float wallToAddX = (float)(Math.Pow(previousLeftWallX + Wall.WallWidth, 2) / 90 - Wall.WallWidth);

				// The next wall's position uses the value calculated above.
				// gapSize is calculated the same way as before.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					wallToAddX,
					(int)(2 * (Game1.WindowWidth / 2 - (wallToAddX + Wall.WallWidth))),
					false);
			}

			// 5th stage of generation: increasing width
			// Basically else if (currentWallInTerrain > 22) - else used so all code paths return a value
			else
			{
				// No need to update gapSizeAnchor here, since further portions of code do not rely on it.

				// Shift the next wall's position using deviationSize.
				// gapSize is incremented.
				// No obstacle is spawned in the pathway.
				return new Tuple<float, int, bool>(
					previousLeftWallX - deviationSize.X,
					gapSize + gapSizeReductionFactor,
					false);
			}
		}
	}
}
