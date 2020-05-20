using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wallDodger
{
	class StartScreen
	{
		private SpriteFont verdanaBold20;
		private Texture2D backdrop;
		private Vector2 titlePosition;
		public Button StartButton { get; }
		public Button QuitButton { get; }

		public StartScreen(
			SpriteFont verdanaBold20, 
			Texture2D backdrop, 
			SpriteFont verdana12, 
			Texture2D buttonTexture,
			MouseState MState)
		{
			this.verdanaBold20 = verdanaBold20;
			this.backdrop = backdrop;
			StartButton = new Button(buttonTexture, verdana12, 50, 50, 55, 55, MState);
			QuitButton = new Button(buttonTexture, verdana12, 100, 100, 105, 105, MState);
			titlePosition = new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				Color.White);

			// Draw the buttons.
			StartButton.Draw(spriteBatch, "Start");
			QuitButton.Draw(spriteBatch, "Quit");

			// Draw the game title text.
			spriteBatch.DrawString(
				verdanaBold20,
				"Wall Dodger",
				titlePosition,
				Color.Maroon);
		}
	}
}
