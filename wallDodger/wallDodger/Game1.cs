using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wallDodger
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public const int WindowHeight = 650;
		public const int WindowWidth = 500;

		Texture2D wall;
		Texture2D playerArrow;

		WallManager wallManager;
		Player player;
		
		KeyboardState kbState;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			// Setting window size up
			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
			graphics.ApplyChanges();

			wallManager = new WallManager(WindowHeight);

			// Setting up the start "stretch"
			for (int i = WindowHeight; i >= -20; i -= Wall.WallHeight)
			{
				wallManager.SpawnWallPair(wall, i);
			}

			this.IsMouseVisible = true;

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			wall = Content.Load<Texture2D>("greySquare");
			playerArrow = Content.Load<Texture2D>("greySquare");

			player = new Player(playerArrow);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			kbState = Keyboard.GetState();

			if (kbState.IsKeyDown(Keys.Left))
			{
				player.Position -= player.StrafeVelocity;
				player.UpdateTracker();
			}
			if (kbState.IsKeyDown(Keys.Right))
			{
				player.Position += player.StrafeVelocity;
				player.UpdateTracker();
			}

			foreach (Wall wall in wallManager.LeftWalls)
			{
				if (player.Collided(wall))
				{
					System.Console.WriteLine("LEFT WALL COLLISION");
				}
			}

			foreach (Wall wall in wallManager.RightWalls)
			{
				if (player.Collided(wall))
				{
					System.Console.WriteLine("RIGHT WALL COLLISION");
				}
			}

			// Continuous scrolling
			if (kbState.IsKeyDown(Keys.Up))
			{
				wallManager.Scroll();
			}
			//wallManager.Scroll();

			// Manage memory by spawning and despawning walls as necessary.
			wallManager.SpawnWallPair(wall);
			wallManager.DespawnWallPair();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			spriteBatch.Begin();
			
			wallManager.DrawAll(spriteBatch, wall);
			player.Draw(spriteBatch, player.Position);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
