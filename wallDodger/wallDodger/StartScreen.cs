using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class StartScreen : MenuScreen
	{
		// Fields

		// Variables storing Button components
		public Button StartButton { get; }
		public Button QuitButton { get; }

		public StartScreen(
			SpriteFont verdanaBold20, 
			SpriteFont verdana12,
			SpriteFont verdanaSmall,
			Texture2D backdrop, 
			Texture2D buttonTexture) : base (
				backdrop,
				new Vector2(90, 200),
				new Vector2(90, 190),
				verdanaBold20,
				verdanaSmall)
		{
			StartButton = new Button(buttonTexture, verdana12, 175, 300, 227, 313);
			QuitButton = new Button(buttonTexture, verdana12, 175, 360, 230, 373);
		}

		/// <summary>
		/// Draws the Start screen and its elements.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				Color.White);

			// Draw the buttons.
			StartButton.Draw(spriteBatch, "Start");
			QuitButton.Draw(spriteBatch, "Quit");

			// Draw the game title text and version number
			spriteBatch.DrawString(
				subtextFont,
				"v1.0.0.0",
				subtextPosition,
				Color.Maroon);

			spriteBatch.DrawString(
				textFont,
				"Wall Dodger",
				textPosition,
				Color.Maroon);
		}
	}
}
