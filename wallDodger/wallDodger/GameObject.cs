using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Defines a core object in the game world

namespace wallDodger
{
	abstract class GameObject
	{
		protected Texture2D asset;
		public Vector2 Position { get; set; }

		// Tracker Rectangle to detect collisions between Player and Wall objects
		public Rectangle Tracker { get; set; }

		public GameObject(Texture2D asset)
		{
			this.asset = asset;
		}

		public abstract void UpdateTracker();
	}
}
