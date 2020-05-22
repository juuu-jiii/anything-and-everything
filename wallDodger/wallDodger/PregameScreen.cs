using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class PregameScreen : MenuScreen
	{
		// Fields

		private Color backdropColour;

		public PregameScreen(
			SpriteFont verdana12, 
			Texture2D backdrop) : base (
				backdrop,
				new Vector2(135, 270),
				Vector2.Zero,	// Unused font position
				verdana12,
				null)			// Unused font type
		{
			// Create a custom colour using only the alpha channel for 
			//		translucency, so this screen can be used as an overlay.
			backdropColour = Color.FromNonPremultiplied(0, 0, 0, 90);
		}

		/// <summary>
		/// Draws the Pregame screen and its elements.
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

			// Draw instruction text.
			spriteBatch.DrawString(
				textFont,
				"Dodge the walls!" 
					+ Environment.NewLine 
					+ "Use the arrow keys to move." 
					+ Environment.NewLine 
					+ "Press any key to start.",
				textPosition,
				Color.White);
		}
	}
}
