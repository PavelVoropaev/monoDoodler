using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using monoDoodler.Helpers;

namespace monoDoodler.Entity
{
    public abstract class PositibleObject
    {
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Vector2 Position;

        public Random Rnd { get { return RandomHelper.Rnd; } }

        public Texture2D Image { get; set; }

        public int Width
        {
            get { return Image.Width; }
        }

        public int Height
        {
            get { return Image.Height; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public bool Active { get; set; }

        public abstract void Initialize(ContentManager content);
    }
}
