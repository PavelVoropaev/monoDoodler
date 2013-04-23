using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monoDoodler.Entity
{
    public class Bullet : PositibleObject
    {
        public int SpeedX { get; set; }

        public int SpeedY { get; set; }

        public void Moove()
        {
            Position.X += SpeedX;
            Position.Y -= SpeedY;
        }

        public override void Initialize(ContentManager content)
        {
            Image = content.Load<Texture2D>("Graphics\\Bullet");
            SpeedY = 25;
        }
    }
}
