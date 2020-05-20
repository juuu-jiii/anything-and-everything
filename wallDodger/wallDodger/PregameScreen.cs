using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wallDodger
{
	class PregameScreen
	{
		private SpriteFont verdana12;
		private Texture2D backdrop;
		private Vector2 textPosition;

		public PregameScreen(SpriteFont verdana12, Texture2D backdrop)
		{
			this.verdana12 = verdana12;
			this.backdrop = backdrop;
			textPosition = new Vector2(100, 100);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the backdrop.
			spriteBatch.Draw(
				backdrop,
				new Rectangle(0, 0, Game1.WindowWidth, Game1.WindowHeight),
				Color.FromNonPremultiplied(0, 0, 0, 90));

			// Draw instruction text.
			spriteBatch.DrawString(
				verdana12,
				"Dodge the walls!" + Environment.NewLine + "Use the arrow keys to move. Press any key to start.",
				textPosition,
				Color.White);
		}
	}
}
