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
	class GameOverScreen
	{
		private SpriteFont verdanaBold20;
		private SpriteFont verdana12;
		private Texture2D backdrop;
		private Vector2 gameOverTextPosition;
		private Vector2 instructionTextPosition;
		public Button returnToMainButton { get; }

		public GameOverScreen(
			SpriteFont verdanaBold20, 
			SpriteFont verdana12, 
			Texture2D backdrop, 
			Texture2D buttonTexture,
			MouseState MState)
		{
			this.verdanaBold20 = verdanaBold20;
			this.verdana12 = verdana12;
			this.backdrop = backdrop;
			gameOverTextPosition = new Vector2(100, 100);
			instructionTextPosition = new Vector2(100, 150);
			returnToMainButton = new Button(buttonTexture, verdana12, 200, 200, 205, 205, MState);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				Color.FromNonPremultiplied(0, 0, 0, 90));

			// Draw game over text.
			spriteBatch.DrawString(
				verdanaBold20,
				"Game Over",
				gameOverTextPosition,
				Color.White);

			// Draw instruction text.
			spriteBatch.DrawString(
				verdanaBold20,
				"Press ENTER to restart.",
				instructionTextPosition,
				Color.White);
		}
	}
}
