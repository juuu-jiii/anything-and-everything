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
		// Fields
		
		// Instance variable of the class to enforce singleton design pattern
		private static WallManager wallManagerInstance;
		
		// List variables to store Wall objects
		private List<Wall> rightWalls;
		private List<Wall> leftWalls;

		private Random generator;

		// Variables representing reference points for spawning and despawning
		private Point spawner;
		private Point despawner;

		public Vector2 ScrollVelocity { get; set; }
		public int GapSize { get; set; }

		// Variables storing x-coordinates of Wall objects lining the start "stretch"
		private int initialLeftWallXPosition;
		private int initialRightWallXPosition;

		// Public static get property for the instance variable that returns 
		//		an instance of the class creating an instance if one does not 
		//		yet exist.
		public static WallManager WallManagerInstance
		{
			get
			{
				if (wallManagerInstance == null)
				{
					wallManagerInstance = new WallManager(Game1.WindowHeight);
					return wallManagerInstance;
				}
				else
				{
					return wallManagerInstance;
				}
			}
		}

		// IEnumerables for both Wall lists - will be iterated through in game1
		//		when checking for collisions with the Player object
		public IEnumerable<Wall> LeftWalls
		{
			get
			{
				return leftWalls.AsEnumerable<Wall>();
			}
		}

		public IEnumerable<Wall> RightWalls
		{
			get
			{
				return rightWalls.AsEnumerable<Wall>();
			}
		}

		// PRIVATE constructor
		private WallManager(int windowHeight)
		{
			rightWalls = new List<Wall>();
			leftWalls = new List<Wall>();
			generator = new Random();

			// Spawner and despawner are above and below screen, respectively.
			spawner = new Point(0, -50);
			despawner = new Point(0, windowHeight + 50);

			ScrollVelocity = new Vector2(0, 10);
			GapSize = 200;

			initialLeftWallXPosition = -400;
			initialRightWallXPosition = initialLeftWallXPosition + Wall.WallWidth + 300;
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
		/// Helper method that creates a pair of left and right walls, 
		///		appending them to their respective LL's.
		/// </summary>
		/// <param name="y">
		/// The y-position of the wall being created.
		/// </param>
		private void SpawnWallPair(Texture2D wallAsset, int y)
		{
			leftWalls.Add(new Wall(wallAsset, initialLeftWallXPosition, y));
			rightWalls.Add(new Wall(wallAsset, initialRightWallXPosition, y));
		}

		// ***SpawnWallPair() overload***
		/// <summary>
		/// Creates a pair of left and right walls, appending them to their 
		///		respective LL's. Used to generate walls past the initial ones 
		///		created via SpawnWallPair().
		/// </summary>
		public void SpawnWallPair(Texture2D wallAsset)
		{
			// Regulate the frequency at which spawning occurs.
			// The vertical distance at which walls generate apart from each other is
			//		affected by how quickly the code is executed.
			// Possible fix: adjust this frequency based on ScrollVelocity.
			if (leftWalls[leftWalls.Count - 1].Position.Y >= spawner.Y + Wall.WallHeight)
			{
				// Generate a valid offset value.
				float offset = GenerateOffset(
					leftWalls[leftWalls.Count - 1].Position.X,
					rightWalls[rightWalls.Count - 1].Position.X);

				// Create a new wall pair with an offset location.
				// Adding rightWall like this creates a dependency on GapSize.
				// If rightWall is added after leftWall, use leftWalls.Count - 2.
				rightWalls.Add(new Wall(
					wallAsset,
					leftWalls[leftWalls.Count - 1].Position.X + Wall.WallWidth + GapSize + offset,
					spawner.Y));
				leftWalls.Add(new Wall(
					wallAsset,
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
				leftWall.UpdateTracker();
			}

			foreach (Wall rightWall in rightWalls)
			{
				rightWall.Position += ScrollVelocity;
				rightWall.UpdateTracker();
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
					leftWall.Tracker,
					Color.White);
			}

			foreach (Wall rightWall in rightWalls)
			{
				spriteBatch.Draw(
					wallAsset,
					rightWall.Tracker,
					Color.White);
			}
		}

		/// <summary>
		/// Clears both lists and initialises the start "stretch". Called 
		///		whenever a new game is started.
		/// </summary>
		/// <param name="wallAsset">
		/// The asset used for the Wall objects.
		/// </param>
		public void Reset(Texture2D wallAsset)
		{
			// Clear both lists
			leftWalls.Clear();
			rightWalls.Clear();
			
			// Setting up the start "stretch"
			for (int i = Game1.WindowHeight; i >= -20; i -= Wall.WallHeight)
			{
				SpawnWallPair(wallAsset, i);
			}
		}
	}
}