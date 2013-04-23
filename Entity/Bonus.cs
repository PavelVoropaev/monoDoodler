using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monoDoodler.Entity
{
    using Properties;

    public sealed class Bonus : PositibleObject
    {
        public bool DoobleJump { get; set; }

        public bool MultFire { get; set; }

        public override void Initialize(ContentManager content)
        {
            if (Rnd.Next(2) % 2 == 0)
            {
                DoobleJump = true;
                Image = content.Load<Texture2D>("Graphics\\doubleJump");
            }
            else
            {
                MultFire = true;
                Image = content.Load<Texture2D>("Graphics\\SBonus");
            }

            Position.Y = 0;
            Position.X = Rnd.Next(10, Settings.MonitorWigth - 30);
        }
    }
}
