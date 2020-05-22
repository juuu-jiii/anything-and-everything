﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class GameOverScreen : MenuScreen
	{
		// Fields

		private Color backdropColour;
		public Button returnToStartScreen { get; }

		public GameOverScreen(
			SpriteFont verdanaBold20, 
			SpriteFont verdana12, 
			Texture2D backdrop, 
			Texture2D buttonTexture) : base (
				backdrop,
				new Vector2(96, 200),
				new Vector2(155, 270),
				verdanaBold20,
				verdana12)
		{
			returnToStartScreen = new Button(buttonTexture, verdana12, 177, 310, 207, 323);

			// Create a custom colour using only the alpha channel for 
			//		translucency, so this screen can be used as an overlay.
			backdropColour = Color.FromNonPremultiplied(0, 0, 0, 90);
		}

		/// <summary>
		/// Draws the Game Over screen and its elements.
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
				backdropColour);

			// Draw button.
			returnToStartScreen.Draw(spriteBatch, "Main Menu");

			// Draw game over text.
			spriteBatch.DrawString(
				textFont,
				"Game Over",
				textPosition,
				Color.White);

			// Draw instruction text.
			spriteBatch.DrawString(
				subtextFont,
				"Press ENTER to restart.",
				subtextPosition,
				Color.White);
		}
	}
}
