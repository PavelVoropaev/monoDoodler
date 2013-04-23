using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monoDoodler.Entity
{
    using Properties;

    public sealed class Enemy : PositibleObject
    {
        public int SpeedX { get; set; }

        public int SpeedY { get; set; }
        
        public override void Initialize(ContentManager content)
        {
            Image = content.Load<Texture2D>("Graphics\\Enemy");
            SpeedX = Rnd.Next(-3, 3);
            SpeedY = Rnd.Next(1, 3);
            Position = new Vector2(0, Rnd.Next(10, Settings.MonitorWigth - 30));
        }
    }
}
