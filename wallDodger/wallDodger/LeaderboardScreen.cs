using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class LeaderboardScreen : MenuScreen
	{
		// Fields

		// Back button
		public Button BackButton { get; }

		// Spacer for drawing scores to the screen
		private Vector2 spacer;

		// Vector2 for second subtext location
		private Vector2 subtext2Position;

		public LeaderboardScreen(
			SpriteFont verdanaBold20,
			SpriteFont verdana12,
			Texture2D backdrop,
			Texture2D buttonTexture) : base (
				backdrop,
				new Vector2(50, 50),
				new Vector2(100, 100),
				verdanaBold20,
				verdana12)
		{
			BackButton = new Button(buttonTexture, verdana12, 175, 360, 225, 373);
			spacer = new Vector2(0, 20);

			// Second subtext is offset 100 px to the right of first.
			subtext2Position = subtextPosition + new Vector2(100, 0);
		}

		/// <summary>
		/// Draws the Leaderboard screen and its elements.
		/// </summary>
		/// <param name="spriteBatch">
		/// The SpriteBatch object used to draw with.
		/// </param>
		/// <param name="hiScores">
		/// An int array containing high scores.
		/// </param>
		public void Draw(SpriteBatch spriteBatch, Tuple<string, int>[] hiScores)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				Color.White);
			
			// Draw the buttons.
			BackButton.Draw(spriteBatch, "Back");

			// Draw the screen heading.
			spriteBatch.DrawString(
				textFont, 
				"HIGH SCORES", 
				textPosition, 
				Color.Black);

			// Loop through the array and draw the high scores.
			for (int i = 0; i < hiScores.Length; i++)
			{
				// spacer works like Environment.NewLine here, to prevent the
				//		game from drawing all scores in the same location 
				//		on-screen.

				spriteBatch.DrawString(
					subtextFont,
					hiScores[i].Item1,
					subtextPosition + i * spacer,
					Color.Black);

				spriteBatch.DrawString(
					subtextFont,
					hiScores[i].Item2.ToString(),
					subtext2Position + i * spacer,
					Color.Black);
			}
		}

		// Unnecessary method; included because overriding is required.
		public override void Draw(SpriteBatch spriteBatch)
		{
			throw new NotImplementedException();
		}
	}
}
