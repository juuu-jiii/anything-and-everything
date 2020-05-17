using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class WallManager
	{
		Texture2D wallAsset;
		private List<Wall> rightWalls;
		private List<Wall> leftWalls;
		private Random generator;
		private Point spawner;
		private Point despawner;
		private Vector2 ScrollVelocity { get; set; }
		private int GapSize { get; set; }
		const int InitialLeftWallXPosition = -400;
		const int InitialRightWallXPosition = InitialLeftWallXPosition + Wall.WallWidth + 300;

		public WallManager(int windowHeight, Texture2D wallAsset)
		{
			rightWalls = new List<Wall>();
			leftWalls = new List<Wall>();
			generator = new Random();

			// Spawner and despawner are above and below screen, respectively.
			spawner = new Point(0, -50);
			despawner = new Point(0, windowHeight + 50);

			ScrollVelocity = new Vector2(0, 20);
			GapSize = 300;

			this.wallAsset = wallAsset;
		}

		/// <summary>
		/// Helper method that generates a random offset based on the location 
		///		of the previous Wall pair. Does not generate values that result 
		///		in the next Wall pair being drawn out of bounds.
		/// </summary>
		/// <param name="previousLeftWallX">
		/// x-coordinate of the previous left wall's top left corner.
		/// </param>
		/// <param name="previousRightWallX">
		/// x-coordinate of the previous right wall's top left corner.
		/// </param>
		/// <returns>
		/// Returns an int offset value.
		/// </returns>
		private int GenerateOffset(float previousLeftWallX, float previousRightWallX)
		{
			int offset = generator.Next(-50, 51);

			// Generate a random offset until a suitable value is obtained.
			while (previousLeftWallX + Wall.WallWidth + offset < 10
				|| previousRightWallX + offset > Game1.WindowWidth)
			{
				offset = generator.Next(-50, 51);
			}

			return offset;
		}

		/// <summary>
		/// Creates a pair of left and right walls, appending them to their 
		///		respective LL's. Used to initialise a game.
		/// </summary>
		/// <param name="y">
		/// The y-position of the wall being created.
		/// </param>
		public void SpawnWallPair(int y)
		{
			leftWalls.Add(new Wall(InitialLeftWallXPosition, y));
			rightWalls.Add(new Wall(InitialRightWallXPosition, y));
		}

		// ***SpawnWallPair() overload***
		/// <summary>
		/// Creates a pair of left and right walls, appending them to their 
		///		respective LL's. Used to generate walls past the initial ones 
		///		created via SpawnWallPair().
		/// </summary>
		public void SpawnWallPair()
		{
			// Regulate the frequency at which spawning occurs.
			if (leftWalls[leftWalls.Count - 1].Position.Y >= spawner.Y + 21)
			{
				// Generate a valid offset value.
				float offset = GenerateOffset(
					leftWalls[leftWalls.Count - 1].Position.X,
					rightWalls[rightWalls.Count - 1].Position.X);

				// Create a new wall pair with an offset location.
				// Adding rightWall like this creates a dependency on GapSize.
				// If rightWall is added after leftWall, use leftWalls.Count - 2.
				rightWalls.Add(new Wall(
					leftWalls[leftWalls.Count - 1].Position.X + Wall.WallWidth + GapSize + offset,
					spawner.Y));
				leftWalls.Add(new Wall(
					leftWalls[leftWalls.Count - 1].Position.X + offset,
					spawner.Y));
				
				// Rewriting the code to add rightWalls like this does NOT create a dependency on
				//		GapSize. This way, reducing GapSize will not be as simple as altering the
				//		variable itself.
				//rightWalls.Add(new Wall(
				//	rightWalls[rightWalls.Count - 1].Position.X + offset,
				//	spawner.Y));
			}
		}

		/// <summary>
		/// Removes the oldest Wall entries in both lists once they reach the 
		///		despawn height (y-coordinate of despawner).
		/// </summary>
		public void DespawnWallPair()
		{
			// Only need to check position of one wall in the pair, since they
			//		both have the same y-coordinate
			if (leftWalls[0].Position.Y >= despawner.Y)
			{
				// Remove the oldest wall pair.
				leftWalls.RemoveAt(0);
				rightWalls.RemoveAt(0);
			}
		}

		/// <summary>
		/// Moves all walls downward by looping through leftWalls and rightWalls,
		///		and adjusting each entry's y-position by ScrollSpeed.
		/// </summary>
		public void Scroll()
		{
			foreach (Wall leftWall in leftWalls)
			{
				leftWall.Position += ScrollVelocity;
				leftWall.UpdateRectangle();
			}

			foreach (Wall rightWall in rightWalls)
			{
				rightWall.Position += ScrollVelocity;
				rightWall.UpdateRectangle();
			}
		}

		/// <summary>
		/// Loops through both lists and draws their contents to the screen.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public void DrawAll(SpriteBatch spriteBatch, Texture2D wallAsset)
		{
			foreach (Wall leftWall in leftWalls)
			{
				spriteBatch.Draw(
					wallAsset,
					leftWall.RectangleTracker,
					Color.White);
			}

			foreach (Wall rightWall in rightWalls)
			{
				spriteBatch.Draw(
					wallAsset,
					rightWall.RectangleTracker,
					Color.White);
			}
		}
	}
}